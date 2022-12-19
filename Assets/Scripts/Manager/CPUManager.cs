using System;
using Card;
using Cysharp.Threading.Tasks;
using Player;
using UI;
using UniRx;
using UnityEngine;

namespace Manager
{
    [RequireComponent(typeof(CPUManager))]
    public class CPUManager : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<GameState> CurrentState => _currentState;
        private readonly ReactiveProperty<GameState> _currentState = new ReactiveProperty<GameState>(GameState.Waiting);

        [SerializeField] private CardSlot slotPrefab;
        [SerializeField] private Transform cardManager;
        [SerializeField] private DecisionButton decisionButton;
        [SerializeField] private UltimateButton ultimateButton;
        [SerializeField] private CardSlot[] battleSlots;
        [SerializeField] private CardSlot[] enemySlots;
        [SerializeField] private GameObject playerHand;
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject enemyObject;
        [SerializeField] private GameObject stage;
        [SerializeField] private AttackButton attackButton;
        [SerializeField] private MoveButton moveButton;
        private PlayerCore _player;
        private EnemyCore _enemy;

        private bool _youWin;

        public static CPUManager Instance;

        private readonly Subject<int> _selected = new Subject<int>();
        public IObservable<int> Selected => _selected;

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
            _player = playerObject.GetComponent<PlayerCore>();
            _enemy = enemyObject.GetComponent<EnemyCore>();

            _currentState
                .Subscribe(OnStateChanged)
                .AddTo(this);

            

            _currentState.AddTo(this);
        }

        /*
         * フェーズを切り替える関数
         */
        private async void OnStateChanged(GameState nextState)
        {
            switch (nextState)
            {
                case GameState.Waiting:
                    WaitingGame();
                    break;
                case GameState.Init:
                    await StartGame();
                    break;
                case GameState.Draw:
                    DrawFaze();
                    break;
                case GameState.Select:
                    SelectFaze();
                    break;
                case GameState.Battle:
                    BattleFaze();
                    break;
            }
        }

        /*
         * ゲームをスタートする前に行う関数
         */
        private async void WaitingGame()
        {
            Debug.Log("waiting");
            await TimeCounter.Instance.CountDown(3);
            Debug.Log("start");
            _currentState.Value = GameState.Init;
        }

        /*
         * ゲーム開始時に行う関数
         */
        private async UniTask StartGame()
        {
            await StartCoroutine(CardData.GetData());   // カードデータを取得
            
            var deck = Resources.Load<Deck>("Deck1");    // デッキのインスタンス生成

            await UniTask.Delay(10);
            
            _player.Initialize(deck);
            _enemy.Initialize(deck);
            
            _player.AtamakkoData.MyHp
                .Where(hp => hp == 0)
                .Subscribe(_ =>
                {
                    _currentState.Value = GameState.End;
                    AnimationManager.Instance.ResultFadeIn(false);
                })
                .AddTo(this);
        
            _enemy.AtamakkoData.MyHp
                .Where(hp => hp == 0)
                .Subscribe(_ =>
                {
                    _currentState.Value = GameState.End;
                    AnimationManager.Instance.ResultFadeIn(true);
                })
                .AddTo(this);
            
            _currentState.Value = GameState.Draw;  // ドローフェーズへ
        }

        /*
         * ドローフェーズ関数
         */
        private void DrawFaze()
        {
            if (_player.CheckDeck()) // 自デッキにカードがないなら
            {
                _player.RefillDeck();
            }
            if (_enemy.CheckDeck()) // 敵デッキにカードがないなら
            {
                _enemy.RefillDeck();
            }

            if (playerHand.transform.childCount <= 0)   // 手持ちにカードがないなら
            {
                for (int i = 0; i < 6; i++)
                {
                    CreateSlot(_player.DrawCard());
                }
                for (int i = 0; i < 6; i++)
                {
                    _enemy.DrawCard();
                }
            }

            _currentState.Value = GameState.Select;
        }

        /*
         * セレクトフェーズ関数
         */
        private async void SelectFaze()
        {
            ultimateButton.MyInteractable = !_player.UsedUltimate; // 必殺技を使っているかどうかを判断
            decisionButton.Decision // 決定ボタンが押されたとき
                .Subscribe(_ =>
                {
                    TimeCounter.Instance.EndTimer(); // タイマーを0にする
                })
                .AddTo(this);

            await TimeCounter.Instance.CountDown(120);    // カウントタイマー起動（120s）
            ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF
            if (_player.UltimateState != UltimateState.Normal) // 必殺技を指定していたら
            {
                _player.UsedUltimate = true;   // 必殺技を使用済みに
            }

            for (int i = 0; i < battleSlots.Length; i++)
            {
                _player.SetSettingCard(i, battleSlots[i].MyCardID);
            }
            
            // 敵の行動情報を受け取る
            _enemy.CardSelect(); // CPUがカードを選択する
            _enemy.UltimateSelect();
            
            _currentState.Value = GameState.Battle;
        }

        /*
         * CPUがカード選択するフェーズ
        
        private void ReceiveEnemyCard()
        {
            _cpuSettingCard = _cpu.SelectCard(_cpuHandCard);
            _enemyData.UltimateState = _cpu.SelectUltimate(_enemyData);
        }
         */

        private async void BattleFaze()
        {
            // 必殺技を選択している
            if (_player.UltimateState != UltimateState.Normal)
            {
                await AnimationManager.Instance.MyUltimateCutIn();
            }
            _player.UseUltimate();
            // 必殺技を選択している
            if (_enemy.UltimateState != UltimateState.Normal)
            {
                await AnimationManager.Instance.EnUltimateCutIn();
            }
            _enemy.UseUltimate();

            for (int i = 0; i < battleSlots.Length; i++)
            {
                enemySlots[i].FlipOver();   // 相手カードを見えるように
                
                battleSlots[i].MySelect.Value = true;
                enemySlots[i].MySelect.Value = true;
                
                await Battle(i);
                
                battleSlots[i].MySelect.Value = false;
                enemySlots[i].MySelect.Value = false;

                battleSlots[i].DeleteCard();
                enemySlots[i].DeleteCard();
            }
            
            _player.TrashCard();
            _enemy.TrashCard();
            
            _player.UltimateState = UltimateState.Normal;
            _enemy.UltimateState = UltimateState.Normal;
            
            _currentState.Value = GameState.Draw;
        }

        private async UniTask Battle(int slotNum)
        {
            var myCard = new CardModel(CardData.CardDataArrayList[_player.GetNowCardID(slotNum)]);
            var enemyCard = new CardModel(CardData.CardDataArrayList[_enemy.GetNowCardID(slotNum)]);
            int myInitiative = _player.GetInitiative(myCard.Initiative);
            int enemyInitiative = _enemy.GetInitiative(enemyCard.Initiative);
            await UniTask.Delay(10);

            if (myInitiative == enemyInitiative && myCard.Kind == enemyCard.Kind)
            {
                if (myCard.Kind == "攻撃")
                {
                    var canAttack = _player.CanAttack(myCard);
                    int playerAttack = 0;
                    foreach (var t in canAttack)
                    {
                        var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                        attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * t);
                        attackArea.AttackPlace = t;
                        playerAttack = t;

                        attackArea.Selected
                            .Subscribe(i =>
                            {
                                playerAttack = i;
                                TimeCounter.Instance.EndTimer();
                            })
                            .AddTo(attackArea);
                    }

                    await TimeCounter.Instance.CountDown(30);
                    
                    int myDamage = _player.GetDamage(myCard.Damage);
                    int enemyDamage = _enemy.GetDamage(enemyCard.Damage);
                    int enemyAttack = _enemy.AttackSelect(_player.AtamakkoData.MyPosition, enemyCard);
                    
                    _player.AddDamage(enemyDamage, playerAttack);
                    _enemy.AddDamage(myDamage, enemyAttack);
                }

                if (myCard.Kind == "移動")
                {
                    var canMove = _player.CanMove(myCard);
                    int playerMove = 0;
                    foreach (var t in canMove)
                    {
                        var moveArea = Instantiate(moveButton, stage.transform.position, Quaternion.identity, stage.transform);
                        moveArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * t);
                        moveArea.MovePlace = t;
                        playerMove = t;

                        moveArea.Selected
                            .Subscribe(i =>
                            {
                                playerMove = i;
                                TimeCounter.Instance.EndTimer();
                            })
                            .AddTo(moveArea);
                    }

                    await TimeCounter.Instance.CountDown(30);
                    int enemyMove = _enemy.MoveSelect(_player.AtamakkoData.MyPosition, enemyCard);
                    
                    _player.Move(playerMove);
                    _enemy.Move(enemyMove);
                }
            }
            else if (myInitiative > enemyInitiative || (myInitiative == enemyInitiative && myCard.Kind == "攻撃"))
            {
                await PlayerTurn(myCard);
                await EnemyTurn(enemyCard);
            }
            else if (myInitiative < enemyInitiative || (myInitiative == enemyInitiative && myCard.Kind == "移動"))
            {
                await EnemyTurn(enemyCard);
                await PlayerTurn(myCard);
            }
        
            await UniTask.Delay(10);
        }

        private async UniTask PlayerTurn(CardModel card)
        {
            if (card.Kind == "攻撃")
            {
                var canAttack = _player.CanAttack(card);
                int attackPosition = 0;
                foreach (var t in canAttack)
                {
                    var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                    attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * t);
                    attackArea.AttackPlace = t;
                    attackPosition = t;

                    attackArea.Selected
                        .Subscribe(i =>
                        {
                            attackPosition = i;
                            TimeCounter.Instance.EndTimer();
                        })
                        .AddTo(attackArea);
                }

                await TimeCounter.Instance.CountDown(30);
                int myDamage = _player.GetDamage(card.Damage);
                _enemy.AddDamage(myDamage, attackPosition);
            }

            if (card.Kind == "移動")
            {
                var canMove = _player.CanMove(card);
                int movePosition = 0;
                foreach (var t in canMove)
                {
                    var moveArea = Instantiate(moveButton, stage.transform.position, Quaternion.identity, stage.transform);
                    moveArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * t);
                    moveArea.MovePlace = t;
                    movePosition = t;

                    moveArea.Selected
                        .Subscribe(i =>
                        {
                            movePosition = i;
                            TimeCounter.Instance.EndTimer();
                        })
                        .AddTo(moveArea);
                }

                await TimeCounter.Instance.CountDown(30);
                _player.Move(movePosition);
            }
        }

        private async UniTask EnemyTurn(CardModel card)
        {
            if (card.Kind == "攻撃")
            {
                int enemyDamage = _enemy.GetDamage(card.Damage);
                int attackPosition = _enemy.AttackSelect(_player.AtamakkoData.MyPosition, card);
                await UniTask.Delay(10);
                _player.AddDamage(enemyDamage, attackPosition);
            }

            if (card.Kind == "移動")
            {
                int movePosition = _enemy.MoveSelect(_player.AtamakkoData.MyPosition, card);
                await UniTask.Delay(10);
                _player.Move(movePosition);
            }
        }

        private void EnemyCard(int sID, int cID)
        {
            enemySlots[sID].CreateCard(cID);
            enemySlots[sID].FlipOver();
        }

        void CreateSlot(int cData)
        {
            var slot = Instantiate(slotPrefab, cardManager);
            slot.CreateCard(cData);
        }
    }
}
