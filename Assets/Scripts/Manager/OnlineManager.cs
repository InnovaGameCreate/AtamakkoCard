using System;
using System.Collections.Generic;
using System.Effect;
using System.Linq;
using Card;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Player;
using UI;
using UniRx;
using UnityEngine;

namespace Manager
{
    public class OnlineManager : MonoBehaviourPunCallbacks
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
        [SerializeField] private GameObject playerContent;
        [SerializeField] private GameObject enemyContent;
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject enemyObject;
        [SerializeField] private GameObject stage;
        [SerializeField] private AttackButton attackButton;
        [SerializeField] private MoveButton moveButton;
        [SerializeField] private GameObject[] sSlot;
        [SerializeField] private GameObject cardPrefab;
        private PlayerCore _player;
        private EnemyCore _enemy;
        private CardController _cardController;
        
        private bool _getData;
        private List<int> _enemyDeck;
        private int _enemyDamage;
        private int _enemyPlace;

        private bool _youWin;

        public static OnlineManager Instance;

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
            // アタマッコ生成
            _player = playerObject.GetComponent<PlayerCore>();
            _enemy = enemyObject.GetComponent<EnemyCore>();

            // ターン制御の設定
            _currentState
                .Subscribe(OnStateChanged)
                .AddTo(this);
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
                    await DrawFaze();
                    break;
                case GameState.Select:
                    await SelectFaze();
                    break;
                case GameState.Battle:
                    await BattleFaze();
                    break;
            }
        }

        /*
         * ゲームをスタートする前に行う関数
         */
        private async void WaitingGame()
        {
            await TimeCounter.Instance.CountDown(3);
            _currentState.Value = GameState.Init;
        }

        /*
         * ゲーム開始時に行う関数
         */
        private async UniTask StartGame()
        {
            // カードデータを取得
            await StartCoroutine(CardData.GetData());
            
            // デッキのインスタンス生成
            var myDeck = Resources.Load<Deck>("Deck1").cardIDList;
            //var playerDeck = PlayerConfig.Deck;
            photonView.RPC(nameof(SendDeck), RpcTarget.Others, myDeck.ToArray());
            
            await UniTask.WaitUntil((() => _getData));
            _getData = false;
            await UniTask.Delay(10);

            // プレイヤーの初期設定
            _player.Initialize(myDeck);
            _enemy.Initialize(_enemyDeck);
            
            // ゲーム終了の設定
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
            
            // ドローフェーズへ
            _currentState.Value = GameState.Draw;
        }

        /*
         * ドローフェーズ関数
         */
        private async UniTask DrawFaze()
        {
            if (_player.CheckDeck()) // 自デッキにカードがないなら
            {
                _player.RefillDeck(); // デッキを補充する
                photonView.RPC(nameof(SendDeck), RpcTarget.Others, _player.GetDeck().ToArray());
                
                await UniTask.WaitUntil((() => _getData));
                _getData = false;
                await UniTask.Delay(10);
                
                _enemy.SetDeck(_enemyDeck);
            }
            /*
            if (_enemy.CheckDeck()) // 敵デッキにカードがないなら
            {
                _enemy.RefillDeck(); // デッキを補充する
            }
            */
            
            if (playerHand.transform.childCount <= 0)   // 手持ちにカードがないなら
            {
                // 自身の手札補充＆スロット生成
                for (int i = 0; i < 6; i++)
                {
                    CreateSlot(_player.DrawCard());
                }
                // エネミーの手札補充
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
        private async UniTask SelectFaze()
        {
            // デッキ情報を更新
            var playerList = _player.GetDeck();
            var enemyList = _enemy.GetDeck();
            foreach (Transform childObj in playerContent.transform)
            {
                Destroy(childObj.gameObject);
            }
            foreach (Transform childObj in enemyContent.transform)
            {
                Destroy(childObj.gameObject);
            }
            await UniTask.Delay(10);
            foreach (var cardID in playerList)
            {
                var card = Instantiate(cardPrefab, playerContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }
            foreach (var cardID in enemyList)
            {
                var card = Instantiate(cardPrefab, enemyContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }
            
            ultimateButton.MyInteractable = !_player.UsedUltimate; // 必殺技を使っているかどうかを判断
            decisionButton.Decision // 決定ボタンが押されたとき
                .Subscribe(_ =>
                {
                    TimeCounter.Instance.EndTimer(); // タイマーを0にする
                })
                .AddTo(this);

            Debug.Log("waiting");
            await TimeCounter.Instance.CountDown(120);    // カウントタイマー起動（120s）
            Debug.Log("start");
            
            ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF
            if (_player.AtamakkoData.UltimateState != UltimateState.Normal) // 必殺技を指定していたら
            {
                _player.UsedUltimate = true;   // 必殺技を使用済みに
            }
            
            // 敵の行動情報を受け取る
            //_enemy.CardSelect(); // CPUがカードを選択する
            //_enemy.UltimateSelect();

            for (int i = 0; i < battleSlots.Length; i++)
            {
                _player.SetSettingCard(i, battleSlots[i].MyCardID);
            }

            photonView.RPC(nameof(SendSelectFaze), 
                    RpcTarget.Others, 
                    _player.GetSettingCards().ToArray(), 
                    _player.AtamakkoData.UltimateState);
            
            
            await UniTask.WaitUntil(() => _getData);
            _getData = false;
            await UniTask.Delay(10);

            for (int i = 0; i < enemySlots.Length; i++)
            {
                EnemyCard(i, _enemy.GetNowCardID(i));
            }
            
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

        private async UniTask BattleFaze()
        {
            // 必殺技を選択している
            if (_player.AtamakkoData.UltimateState != UltimateState.Normal)
            {
                await AnimationManager.Instance.MyUltimateCutIn();
                switch (_player.AtamakkoData.UltimateState)
                {
                    case UltimateState.Recover:
                        break;
                    case UltimateState.Attack:
                        EffectManager.Instance.InstantiateEffect(EffectType.RedEffect, _player.transform);
                        break;
                    case UltimateState.Speed:
                        EffectManager.Instance.InstantiateEffect(EffectType.BlueEffect, _player.transform);
                        break;
                }
            }
            _player.UseUltimate();
            // 必殺技を選択している
            if (_enemy.AtamakkoData.UltimateState != UltimateState.Normal)
            {
                await AnimationManager.Instance.EnUltimateCutIn();
                switch (_enemy.AtamakkoData.UltimateState)
                {
                    case UltimateState.Recover:
                        break;
                    case UltimateState.Attack:
                        EffectManager.Instance.InstantiateEffect(EffectType.RedEffect, _enemy.transform);
                        break;
                    case UltimateState.Speed:
                        EffectManager.Instance.InstantiateEffect(EffectType.BlueEffect, _enemy.transform);
                        break;
                }
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
            
            _player.AtamakkoData.UltimateState = UltimateState.Normal;
            _enemy.AtamakkoData.UltimateState = UltimateState.Normal;
            
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
                        playerAttack = t;
                        attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * playerAttack);
                        attackArea.AttackPlace = playerAttack;

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
                    /*
                    int enemyDamage = _enemy.GetDamage(enemyCard.Damage);
                    int enemyAttack = _enemy.AttackSelect(_player.AtamakkoData.MyPosition, enemyCard);
                    */
                    photonView.RPC(nameof(SendAction), RpcTarget.Others, myDamage, playerAttack);

                    await UniTask.WaitUntil(() => _getData);
                    _getData = false;
                    await UniTask.Delay(10);

                    if (_enemy.AtamakkoData.MyPosition == playerAttack)
                    {
                        _enemy.AddDamage(myDamage);
                    }
                    if (myCard.Additional == "〇")
                    {
                        _player.Move(playerAttack);
                    }
                    
                    if (_player.AtamakkoData.MyPosition == _enemyPlace)
                    {
                        _player.AddDamage(_enemyDamage);
                    }
                    if (enemyCard.Additional == "〇")
                    {
                        _enemy.Move(_enemyPlace);
                    }
                }

                if (myCard.Kind == "移動")
                {
                    var canMove = _player.CanMove(myCard);
                    int playerMove = 0;
                    foreach (var t in canMove)
                    {
                        playerMove = t;
                        var moveArea = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[playerMove].transform);
                        moveArea.MovePlace = playerMove;

                        moveArea.Selected
                            .Subscribe(i =>
                            {
                                playerMove = i;
                                TimeCounter.Instance.EndTimer();
                            })
                            .AddTo(moveArea);
                    }
                    await TimeCounter.Instance.CountDown(30);
                    photonView.RPC(nameof(SendAction), RpcTarget.Others, 0, playerMove);

                    await UniTask.WaitUntil(() => _getData);
                    _getData = false;
                    await UniTask.Delay(10);
                    
                    /*
                    int enemyMove = _enemy.MoveSelect(_player.AtamakkoData.MyPosition, enemyCard);
                    */
                    
                    _player.Move(playerMove);
                    if (myCard.Additional == "〇" && _enemy.AtamakkoData.MyPosition == playerMove)
                    {
                        int myDamage = _player.GetDamage(myCard.Damage);
                        _enemy.AddDamage(myDamage);
                    }
                    _enemy.Move(_enemyPlace);
                    if (enemyCard.Additional == "〇" && _player.AtamakkoData.MyPosition == _enemyPlace)
                    {
                        int enemyDamage = _enemy.GetDamage(enemyCard.Damage);
                        _player.AddDamage(enemyDamage);
                    }
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

        [PunRPC]
        private void SendDeck(int[] deck)
        {
            var list = deck.ToList();
            _enemyDeck = new List<int>(list);
            _getData = true;
        }

        [PunRPC]
        private void SendSelectFaze(int[] setting, UltimateState ultimateState)
        {
            var list = setting.ToList();
            _enemy.SetSettingCards(list);
            _enemy.AtamakkoData.UltimateState = ultimateState;
            _getData = true;
        }

        [PunRPC]
        private void SendAction(int damage, int place)
        {
            _enemyDamage = (damage + 3) % 6;
            _enemyPlace = (place + 3) % 6;
            _getData = true;
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
                    attackPosition = t;
                    attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * attackPosition);
                    attackArea.AttackPlace = attackPosition;

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
                if (_enemy.AtamakkoData.MyPosition == attackPosition)
                {
                    _enemy.AddDamage(myDamage);
                }
                if (card.Additional == "〇")
                {
                    _player.Move(attackPosition);
                }
            }

            if (card.Kind == "移動")
            {
                var canMove = _player.CanMove(card);
                int movePosition = 0;
                foreach (var t in canMove)
                {
                    movePosition = t;
                    var moveArea = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[movePosition].transform);
                    moveArea.MovePlace = movePosition;

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
                if (card.Additional == "〇" && _enemy.AtamakkoData.MyPosition == movePosition)
                {
                    int myDamage = _player.GetDamage(card.Damage);
                    _enemy.AddDamage(myDamage);
                }
            }
        }

        private async UniTask EnemyTurn(CardModel card)
        {
            await UniTask.WaitUntil(() => _getData);
            _getData = false;
            await UniTask.Delay(10);
            
            if (card.Kind == "攻撃")
            {
                /*
                int enemyDamage = _enemy.GetDamage(card.Damage);
                int attackPosition = _enemy.AttackSelect(_player.AtamakkoData.MyPosition, card);
                */
                if (_player.AtamakkoData.MyPosition == _enemyPlace)
                {
                    _player.AddDamage(_enemyDamage);
                }
                if (card.Additional == "〇")
                {
                    _enemy.Move(_enemyPlace);
                }
            }

            if (card.Kind == "移動")
            {
                //int movePosition = _enemy.MoveSelect(_player.AtamakkoData.MyPosition, card);
                _enemy.Move(_enemyPlace);
                if (card.Additional == "〇" && _player.AtamakkoData.MyPosition == _enemyPlace)
                {
                    int enemyDamage = _enemy.GetDamage(card.Damage);
                    _player.AddDamage(enemyDamage);
                }
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