using System;
using System.Collections.Generic;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using Photon.Pun;
using Player;
using UniRx;
using UnityEngine;

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

    private ReactiveProperty<bool> _ready = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> MyReady
    {
        get => _ready;
        set => _ready = value;
    }
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
    private PlayerMove _move;
    private PlayerAttack _attack;
    private Deck _deck1;
    private List<int> _cardList;
    private AtamakkoStatus _playerStatus;
    private AtamakkoStatus _enemyStatus;

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

        _ready
            .Where(r => r)
            .Subscribe(_ =>
            {
                if (_otherReady)
                {
                    photonView.RPC(nameof(NextStart), RpcTarget.AllViaServer);
                    _otherReady = false;
                }
                else
                {
                    photonView.RPC(nameof(SendReady), RpcTarget.Others);
                }
            })
            .AddTo(this);
    }

    [PunRPC]
    private void SendReady()
    {
        if (_ready.Value)
        {
            photonView.RPC(nameof(NextStart), RpcTarget.AllViaServer);
            return;
        }
        _otherReady = true;
    }

    [PunRPC]
    private void NextStart()
    {
        _next.OnNext(true);
        _ready.Value = false;
    }

    private async void WaitingGame()
    {
        _ready.Value = true;
        await _next.ToUniTask(true);
        _gameState.Value = State.Init;
    }

    private async void StartGame()
    {
        await StartCoroutine(CardData.GetData());
        _deck1 = Resources.Load<Deck>("Deck1");

        _cardList = new List<int>(_deck1.cardIDList);
        _cardList = ShuffleDeck(_cardList);

        _ready.Value = true;
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
        decisionButton.Decision
            .Subscribe(_ =>
            {
                decisionButton.MyInteractable = false;

                _ready.Value = true;
            })
            .AddTo(this);
        
        await _next.ToUniTask(true);
        _gameState.Value = State.Battle;
    }
    
    private async void BattleFaze()
    {
        Debug.Log("BattleFaze");
        foreach (var slot in battleSlots)
        {
            // var token = new CancellationTokenSource();
            await Battle(slot.MyCardID);
            slot.CreateCard(-1);
        }

        _ready.Value = true;
        await _next.ToUniTask(true);
        _gameState.Value = State.Draw;
    }

    private async UniTask Battle(int cardID)
    {
        var card = new CardModel(CardData.CardDataArrayList[cardID]);
        Debug.Log("ID"+cardID);
        for (int i = 6; i > 0; i--)
        {
            await UniTask.Delay(10);
            
            await _attack.Attack(card, i);
            _ready.Value = true;
            await _next.ToUniTask(true);
            
            await _move.CanMove(card, i);
            _ready.Value = true;
            await _next.ToUniTask(true);
        }
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
