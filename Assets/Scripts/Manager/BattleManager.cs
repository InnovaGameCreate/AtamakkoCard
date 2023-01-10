using Atamakko;
using Card;
using Photon.Pun;
using UI;
using UniRx;
using UnityEngine;

namespace Manager
{
    public class BattleManager : MonoBehaviourPunCallbacks
    {
        public IReadOnlyReactiveProperty<GameState> CurrentState => _CurrentState;
        protected readonly ReactiveProperty<GameState> _CurrentState = new ReactiveProperty<GameState>(GameState.Waiting);

        [SerializeField] protected CardSlot slotPrefab;
        [SerializeField] protected Transform cardManager;
        [SerializeField] protected ButtonController decisionButton;
        [SerializeField] protected UltimateButton ultimateButton;
        [SerializeField] protected CardSlot[] battleSlots;
        [SerializeField] protected CardSlot[] enemySlots;
        [SerializeField] protected GameObject playerHand;
        [SerializeField] protected GameObject playerContent;
        [SerializeField] protected GameObject enemyContent;
        [SerializeField] protected GameObject playerObject;
        [SerializeField] protected GameObject enemyObject;
        [SerializeField] protected GameObject stage;
        [SerializeField] protected AttackButton attackButton;
        [SerializeField] protected MoveButton moveButton;
        [SerializeField] protected GameObject[] sSlot;
        [SerializeField] protected GameObject cardPrefab;
        [SerializeField] protected UltimateInformText informText;
        protected PlayerCore Player;
        protected EnemyCore Enemy;
        public bool CardMobile { get; protected set; }

        protected bool YouWin;

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
            // アタマッコ生成
            Player = playerObject.GetComponent<PlayerCore>();
            Enemy = enemyObject.GetComponent<EnemyCore>();

            // ターン制御の設定
            _CurrentState
                .Subscribe(OnStateChanged)
                .AddTo(this);
        }

        /*
         * フェーズを切り替える関数
         */
        private void OnStateChanged(GameState nextState)
        {
            switch (nextState)
            {
                case GameState.Waiting:
                    WaitingGame();
                    break;
                case GameState.Init:
                    StartGame();
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
        
        protected virtual void WaitingGame()
        {
        }

        protected virtual void StartGame()
        {
        }

        protected virtual void DrawFaze()
        {
        }

        protected virtual void SelectFaze()
        {
        }
        
        protected virtual void BattleFaze()
        {
        }
    }
}
