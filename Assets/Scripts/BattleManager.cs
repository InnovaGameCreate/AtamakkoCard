using System;
using System.Collections.Generic;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using Photon.Pun;
using Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviourPunCallbacks
{
    public enum State
    {
        Waiting,
        Init,
        Draw,
        Select,
        Battle,
        End
    }
    private ReactiveProperty<State> _gameState = new ReactiveProperty<State>(State.Waiting);
    public ReactiveProperty<State> MyGameState
    {
        get => _gameState;
        set => _gameState = value;
    }

    private bool _ready;
    private bool _otherReady;

    private readonly Subject<bool> _next = new Subject<bool>();
    public IObserver<bool> Next => _next;

    [SerializeField] private CardSlot slotPrefab;
    [SerializeField] private Transform cardManager;
    [SerializeField] private DecisionButton decisionButton;
    [SerializeField] private CardSlot[] battleSlots;
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Button bSpecial;
    private PlayerMove _move;
    private PlayerAttack _attack;
    private Deck _deck1;
    private List<int> _cardList;
    private AtamakkoStatus _playerStatus;
    private AtamakkoStatus _enemyStatus;
    private bool _usedUltimate;

    public static BattleManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _move = player.GetComponent<PlayerMove>();
        _attack = player.GetComponent<PlayerAttack>();
        _playerStatus = player.GetComponent<AtamakkoStatus>();

        _enemyStatus = enemy.GetComponent<AtamakkoStatus>();
        
        _gameState
            .Where(x => x == State.Waiting)
            .Subscribe(_ => WaitingGame())
            .AddTo(this);
        
        _gameState
            .Where(x => x == State.Init)
            .Subscribe(_ => StartGame())
            .AddTo(this);

        _gameState
            .Where(x => x == State.Draw)
            .Subscribe(_ => DrawFaze())
            .AddTo(this);

        _gameState
            .Where(x => x == State.Select)
            .Subscribe(_ => SelectFaze())
            .AddTo(this);

        _gameState
            .Where(x => x == State.Battle)
            .Subscribe(_ => BattleFaze())
            .AddTo(this);

        _playerStatus.MyHp
            .Where(hp => hp == 0)
            .Subscribe(_ =>
            {
                _gameState.Value = State.End;
                Debug.Log("You Lose!");
            })
            .AddTo(this);
        
        _enemyStatus.MyHp
            .Where(hp => hp == 0)
            .Subscribe(_ =>
            {
                _gameState.Value = State.End;
                Debug.Log("You Win!");
            })
            .AddTo(this);
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_ready && _otherReady)
            {
                photonView.RPC(nameof(NextStart), RpcTarget.All);
            }
        }
    }

    private void Ready()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _ready = true;
        }
        else
        {
            photonView.RPC(nameof(SendReady), RpcTarget.Others);
        }
    }

    [PunRPC]
    private void SendReady()
    {
        _otherReady = true;
    }

    [PunRPC]
    private void NextStart()
    {
        _next.OnNext(true);
        _otherReady = false;
        _ready = false;
    }

    private async void WaitingGame()
    {
        Ready();
        await _next.ToUniTask(true);
        _gameState.Value = State.Init;
    }

    private async void StartGame()
    {
        await StartCoroutine(CardData.GetData());
        _deck1 = Resources.Load<Deck>("Deck1");

        _cardList = new List<int>(_deck1.cardIDList);
        _cardList = ShuffleDeck(_cardList);

        Ready();
        await _next.ToUniTask(true);
        _gameState.Value = State.Draw;
    }

    private void DrawFaze()
    {
        if (_cardList.Count <= 0)
        {
            _cardList = new List<int>(_deck1.cardIDList);
            _cardList = ShuffleDeck(_cardList);
        }

        if (playerHand.transform.childCount <= 0)
        {
            DrawCard();
        }

        _gameState.Value = State.Select;
    }

    private async void SelectFaze()
    {
        Debug.Log("SelectFaze");
        bSpecial.interactable = !_usedUltimate;
        decisionButton.Decision
            .Subscribe(_ =>
            {
                decisionButton.MyInteractable = false;

                Ready();
            })
            .AddTo(this);
        
        await _next.ToUniTask(true);
        if (_playerStatus.UState != AtamakkoStatus.Ultimate.Normal)
        {
            _usedUltimate = true;
        }

        bSpecial.interactable = false;
        _gameState.Value = State.Battle;
    }
    
    private async void BattleFaze()
    {
        Debug.Log("BattleFaze");
        if (_playerStatus.UState == AtamakkoStatus.Ultimate.Recover)
        {
            _playerStatus.MyHp.Value += 3;
        }
        foreach (var slot in battleSlots)
        {
            // var token = new CancellationTokenSource();
            await Battle(slot.MyCardID);
            slot.CreateCard(-1);
        }

        Ready();
        await _next.ToUniTask(true);
        Debug.Log("NextRound");
        _playerStatus.UState = AtamakkoStatus.Ultimate.Normal;
        _gameState.Value = State.Draw;
    }

    private async UniTask Battle(int cardID)
    {
        await UniTask.Delay(10);
        var card = new CardModel(CardData.CardDataArrayList[cardID]);
        
        for (int i = 6; i > 0; i--)
        {
            int initiative = i;
            if (_playerStatus.UState == AtamakkoStatus.Ultimate.Speed)
            {
                initiative -= 1;
            }
            await UniTask.Delay(10);
            
            await _attack.Attack(card, initiative);
            Debug.Log("Attack" + initiative);
            await UniTask.Delay(10);
            Ready();
            await _next.ToUniTask(true);
            _attack.Attack();

            await _move.CanMove(card, initiative);
            Debug.Log("Move" + initiative);
            await UniTask.Delay(10);
            Ready();
            await _next.ToUniTask(true);
            _move.MovePart();
        }
        
        await UniTask.Delay(10);
    }

    private List<int> ShuffleDeck(List<int> idList)
    {
        System.Random random = new System.Random((int) DateTime.Now.Ticks);
        for (int i = 0; i < idList.Count; i++)
        {
            int index = i + (int) (random.NextDouble() * (idList.Count - i));
            (idList[index], idList[i]) = (idList[i], idList[index]);
        }
        
        return idList;
    }

    void DrawCard()
    {
        for (int i = 0; i < 6; i++)
        {
            CreateSlot(_cardList[0]);
            _cardList.Remove(_cardList[0]);
        }
    }
    
    void CreateSlot(int cData)
    {
        var slot = Instantiate(slotPrefab, cardManager);
        slot.CreateCard(cData);
    }
}
