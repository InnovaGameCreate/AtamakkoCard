using Atamakko;
using Card;
using Photon.Pun;
using UI;
using UniRx;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// 戦闘システムを管理するクラス
    /// </summary>
    public class BattleManager : MonoBehaviourPunCallbacks
    {
        // 現在のフェイズ
        public IReadOnlyReactiveProperty<GameState> CurrentState => _CurrentState;
        protected readonly ReactiveProperty<GameState> _CurrentState = new ReactiveProperty<GameState>(GameState.Waiting);

        public IReadOnlyReactiveProperty<bool> Next => _next;
        protected readonly ReactiveProperty<bool> _next = new ReactiveProperty<bool>(false);

        [SerializeField] protected CardSlot slotPrefab; // スロットのプレハブ
        [SerializeField] protected Transform cardManager; // スロットの親
        [SerializeField] protected ButtonController decisionButton; // 決定ボタン
        [SerializeField] protected ButtonController ultimateButton; // 必殺技ボタン
        [SerializeField] protected CardSlot[] battleSlots; // セット先のスロット
        [SerializeField] protected CardSlot[] enemySlots; // 敵のスロット
        [SerializeField] protected GameObject playerHand; // プレイヤーの手札
        [SerializeField] protected GameObject playerContent; // プレイヤーの山札
        [SerializeField] protected GameObject enemyContent; // 敵の山札＋手札
        [SerializeField] protected GameObject playerObject; // プレイヤーのアタマッコ
        [SerializeField] protected GameObject enemyObject; // 敵のアタマッコ
        [SerializeField] protected GameObject stage; // ステージ
        [SerializeField] protected AttackButton attackButton; // 攻撃ボタンのプレハブ
        [SerializeField] protected MoveButton moveButton; // 移動ボタンのプレハブ
        [SerializeField] protected GameObject[] sSlot; // フィールドのスロット
        [SerializeField] protected GameObject cardPrefab; // カードのプレハブ
        [SerializeField] protected UltimateInformText informText; // 必殺技のテキスト
        [SerializeField] protected GameObject settingPlace; // セットするエフェクト
        // アタマッコ
        protected PlayerCore Player;
        protected EnemyCore Enemy;
        public ReactiveProperty<bool> CardMobile { get; protected set; } = new ReactiveProperty<bool>(false); // カードを移動可能かどうか

        protected bool YouWin; // 勝敗判定

        public static BattleManager Instance; // インスタンス

        // シングルトン化
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

        /// <summary>
        /// フェーズを切り替える。
        /// </summary>
        /// <param name="nextState">次のフェイズ</param>
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
        
        /// <summary>
        /// 待ちフェイズ
        /// </summary>
        protected virtual void WaitingGame()
        {
        }

        /// <summary>
        /// スタートフェイズ
        /// </summary>
        protected virtual void StartGame()
        {
        }

        /// <summary>
        /// ドローフェイズ
        /// </summary>
        protected virtual void DrawFaze()
        {
        }

        /// <summary>
        /// 選択フェイズ
        /// </summary>
        protected virtual void SelectFaze()
        {
        }
        
        /// <summary>
        /// 戦闘フェイズ
        /// </summary>
        protected virtual void BattleFaze()
        {
        }
    }
}
