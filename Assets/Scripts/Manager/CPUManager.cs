using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Card;
using Cysharp.Threading.Tasks;
using Player;
using UI;
using UniRx;
using UnityEngine;

namespace Manager
{
    public class CPUManager : MonoBehaviour
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

        private readonly Subject<bool> _next = new Subject<bool>();
        public IObserver<bool> Next => _next;

        [SerializeField] private CardSlot slotPrefab;
        [SerializeField] private Transform cardManager;
        [SerializeField] private DecisionButton decisionButton;
        [SerializeField] private CardSlot[] battleSlots;
        [SerializeField] private CardSlot[] enemySlots;
        [SerializeField] private GameObject playerHand;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject enemy;
        [SerializeField] private UltimateButton ultimateButton;
        [SerializeField] private TimeCounter timeCounter;
        private PlayerMove _move;
        private PlayerAttack _attack;

        private Deck _myDeck;
        public Deck MyDeck => _myDeck;
        private List<int> _myCardList;
        private List<int> _myUsedCard;
        
        private Deck _cpuDeck;
        public Deck CPUDeck => _cpuDeck;
        private List<int> _cpuCardList;
        private List<int> _cpuUsedCard;
        
        private AtamakkoStatus _playerStatus;
        private AtamakkoStatus _enemyStatus;
        private bool _usedUltimate;
        private bool _youWin;

        public static CPUManager Instance;

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
                .Subscribe(OnStateChanged)
                .AddTo(this);

            _playerStatus.MyHp
                .Where(hp => hp == 0)
                .Subscribe(_ =>
                {
                    _gameState.Value = State.End;
                    AnimationManager.Instance.ResultFadeIn(false);
                })
                .AddTo(this);
        
            _enemyStatus.MyHp
                .Where(hp => hp == 0)
                .Subscribe(_ =>
                {
                    _gameState.Value = State.End;
                    AnimationManager.Instance.ResultFadeIn(true);
                })
                .AddTo(this);
        }

        private void OnStateChanged(State nextState)
        {
            switch (nextState)
            {
                case State.Waiting:
                    WaitingGame();
                    break;
                case State.Init:
                    StartGame();
                    break;
                case State.Draw:
                    DrawFaze();
                    break;
                case State.Select:
                    SelectFaze();
                    break;
                case State.Battle:
                    BattleFaze();
                    break;
            }
        }

        private void NextStart()
        {
            _next.OnNext(true);
        }

        private async void WaitingGame()
        {
            Debug.Log("waiting");
            await timeCounter.CountDown(7);
            Debug.Log("start");
            _gameState.Value = State.Init;
        }

        private async void StartGame()
        {
            await StartCoroutine(CardData.GetData());   // カードデータを取得
            _myDeck = Resources.Load<Deck>("Deck1");    // 自デッキのインスタンス生成
            _cpuDeck = Resources.Load<Deck>("Deck1");   // 敵デッキのインスタンス生成

            _myCardList = new List<int>(_myDeck.cardIDList);    // 自デッキのリスト生成
            _myCardList = ShuffleDeck(_myCardList); // 自デッキのシャッフル
            
            _cpuCardList = new List<int>(_cpuDeck.cardIDList);    // 敵デッキのリスト生成
            _cpuCardList = ShuffleDeck(_cpuCardList); // 敵デッキのシャッフル
            
            _gameState.Value = State.Draw;  // ドローフェーズへ
        }

        private void DrawFaze()
        {
            if (_myCardList.Count <= 0) // 自デッキにカードがないなら
            {
                _myCardList = new List<int>(_myDeck.cardIDList);    // 自デッキを補充
                _myCardList = ShuffleDeck(_myCardList); // 自デッキをシャッフル
            }
            if (_cpuCardList.Count <= 0) // 敵デッキにカードがないなら
            {
                _cpuCardList = new List<int>(_cpuDeck.cardIDList);    // 敵デッキを補充
                _cpuCardList = ShuffleDeck(_cpuCardList); // 敵デッキをシャッフル
            }

            if (playerHand.transform.childCount <= 0)   // 手持ちにカードがないなら
            {
                DrawCard(); // カードを引く
            }

            _gameState.Value = State.Select;
        }

        private async void SelectFaze()
        {
            ultimateButton.MyInteractable = !_usedUltimate; // 必殺技を使っているかどうかを判断
            decisionButton.Decision // 決定ボタンが押されたとき
                .Subscribe(_ =>
                {
                    timeCounter.EndTimer(); // タイマーを0にする
                })
                .AddTo(this);

            await timeCounter.CountDown(120);    // カウントタイマー起動（120s）
            ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF
            if (_playerStatus.UState != AtamakkoStatus.Ultimate.Normal) // 必殺技を指定していたら
            {
                _usedUltimate = true;   // 必殺技を使用済みに
            }

            await ReceiveEnemyCard();   // 敵の行動情報を受け取る

            _gameState.Value = State.Battle;
        }

        private async UniTask ReceiveEnemyCard()
        {
            throw new NotImplementedException();
        }

        private async void BattleFaze()
        {
            // 必殺技を選択している
            if (_playerStatus.UState != AtamakkoStatus.Ultimate.Normal)
            {
                await AnimationManager.Instance.MyUltimateCutIn();
            }
            
            if (_playerStatus.UState == AtamakkoStatus.Ultimate.Recover)
            {
                _playerStatus.MyHp.Value += 3;
            }
            for (int i = 0; i < battleSlots.Length; i++)
            {
                enemySlots[i].FlipOver();
                battleSlots[i].MySelect.Value = true;
                enemySlots[i].MySelect.Value = true;
                await Battle(i);
                battleSlots[i].MySelect.Value = false;
                enemySlots[i].MySelect.Value = false;
                battleSlots[i].DeleteCard();
                enemySlots[i].DeleteCard();
            }
            
            _playerStatus.UState = AtamakkoStatus.Ultimate.Normal;
            _gameState.Value = State.Draw;
        }

        private async UniTask Battle(int slotID)
        {
            var myCard = new CardModel(CardData.CardDataArrayList[battleSlots[slotID].MyCardID]);
            var enemyCard = new CardModel(CardData.CardDataArrayList[enemySlots[slotID].MyCardID]);
            int myInitiative = myCard.Initiative;
            int enemyInitiative = enemyCard.Initiative;
            if (_playerStatus.UState == AtamakkoStatus.Ultimate.Speed)
            {
                myInitiative += 1;
            }
            if (_enemyStatus.UState == AtamakkoStatus.Ultimate.Speed)
            {
                enemyInitiative += 1;
            }
            await UniTask.Delay(10);

            if (myInitiative > enemyInitiative)
            {
                if(myCard.Kind == "攻撃") await _attack.AttackSelect(myCard, myInitiative);
                if(myCard.Kind == "移動") await _move.CanMove(myCard, myInitiative);
                await timeCounter.CountDown(100);
                
                
            }
            
            /* 
            for (int i = 6; i > 0; i--)
            {
                int initiative = i;
                if (_playerStatus.UState == AtamakkoStatus.Ultimate.Speed)
                {
                    initiative -= 1;
                }
                await UniTask.Delay(10);
            
                await _attack.AttackSelect(card, initiative);
                await UniTask.Delay(10);
                await _next.ToUniTask(true);
                _attack.AttackDamage();

                await _move.CanMove(card, initiative);
                await UniTask.Delay(10);
                await _next.ToUniTask(true);
                _move.MovePart();
            }
             */
        
            await UniTask.Delay(10);
        }
        
        //private async UniTask Enemy

        private void EnemyCard(int sID, int cID)
        {
            enemySlots[sID].CreateCard(cID);
            enemySlots[sID].FlipOver();
        }

        private async UniTask EnemyUltimateCutIn()
        {
            await AnimationManager.Instance.EnUltimateCutIn();
        }

        private List<int> ShuffleDeck(List<int> idList)
        {
            System.Random random = new System.Random((int) DateTime.Now.Ticks); // ランダムのインスタンス化
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
                CreateSlot(_myCardList[0]);
                _myCardList.Remove(_myCardList[0]);
            }
        }
    
        void CreateSlot(int cData)
        {
            var slot = Instantiate(slotPrefab, cardManager);
            slot.CreateCard(cData);
        }
    }
}
