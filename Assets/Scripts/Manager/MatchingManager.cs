using Arena;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    /// <summary>
    /// マッチングシーンから戦闘シーンへの遷移を管理するクラス
    /// </summary>
    public class MatchingManager : MonoBehaviourPunCallbacks
    {
        private bool _isConnecting;
        private bool _inRoom; // ルームに入っているかどうか
        private bool _isMatching; // マッチングしたかどうか

        public IReadOnlyReactiveProperty<RuleState> CurrentRule => _currentRule;
        private readonly ReactiveProperty<RuleState> _currentRule = new ReactiveProperty<RuleState>(RuleState.Casual);

        [SerializeField] private ButtonController matchingButton; // オンライン戦のボタン
        [SerializeField] private ButtonController ruleButton; // オンライン戦のボタン
        [SerializeField] private ButtonController casualButton; // CPU戦のボタン
        [SerializeField] private ButtonController rateButton; // CPU戦のボタン
        [SerializeField] private ButtonController privateButton; // CPU戦のボタン
        [SerializeField] private ButtonController cancelButton; // CPU戦のボタン
        
        [SerializeField] private GameObject rateContent;
        [SerializeField] private GameObject passwordContent;
        [SerializeField] private GameObject ruleButtons;
        private GameObject _cancelObject;

        [SerializeField] private TMP_Text ruleText;
        [SerializeField] private TMP_InputField passwordInputField;

        private RoomOptions _roomOptions;

        public static MatchingManager Instance; // インスタンス

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
            _currentRule
                .Subscribe(state =>
                {
                    casualButton.MyInteractable = true;
                    rateButton.MyInteractable = false; // 一旦、選択できないようにしておく
                    privateButton.MyInteractable = true;
                    switch (state)
                    {
                        case RuleState.Casual:
                            rateContent.SetActive(true);
                            passwordContent.SetActive(false);
                            casualButton.MyInteractable = false;
                            matchingButton.MyInteractable = true;
                            ruleText.text = "カジュアル";
                            break;
                        case RuleState.Rating:
                            rateContent.SetActive(true);
                            passwordContent.SetActive(false);
                            rateButton.MyInteractable = false;
                            matchingButton.MyInteractable = true;
                            ruleText.text = "レート";
                            break;
                        case RuleState.Private:
                            rateContent.SetActive(false);
                            passwordContent.SetActive(true);
                            privateButton.MyInteractable = false;
                            matchingButton.MyInteractable = false;
                            ruleText.text = "プライベート";
                            break;
                    }
                })
                .AddTo(this);

            // オンライン戦のボタンが押されたときの処理
            matchingButton.Pushed
                .Subscribe(_ => MatchingButton())
                .AddTo(this);
            
            // オンライン戦のボタンが押されたときの処理
            cancelButton.Pushed
                .Subscribe(_ => CancelButton())
                .AddTo(this);

            ruleButton.Pushed
                .Subscribe(_ =>ruleButtons.SetActive(true))
                .AddTo(this);

            casualButton.Pushed
                .Subscribe(_ =>
                {
                    _currentRule.Value = RuleState.Casual;
                    ruleButtons.SetActive(false);
                })
                .AddTo(this);

            rateButton.Pushed
                .Subscribe(_ =>
                {
                    _currentRule.Value = RuleState.Rating;
                    ruleButtons.SetActive(false);
                })
                .AddTo(this);

            privateButton.Pushed
                .Subscribe(_ =>
                {
                    _currentRule.Value = RuleState.Private;
                    ruleButtons.SetActive(false);
                })
                .AddTo(this);
            
            passwordInputField.onValueChanged.AddListener(OnPasswordInputFieldValueChanged);

            _cancelObject = cancelButton.gameObject;

            _roomOptions = new RoomOptions
            {
                MaxPlayers = 2
            };
        }

        private void OnPasswordInputFieldValueChanged(string value)
        {
            matchingButton.MyInteractable = (value.Length == 6);
        }

        /// <summary>
        /// オフラインモードでBattleCPUシーンへ行く。
        /// </summary>
        private void CPUButton()
        {
            PlayerConfig.IsOnline = false;
            enemyDeckData.enemyDeck = PlayerConfig.Deck;
            SceneManager.LoadScene("BattleCPU");
        }

        /// <summary>
        /// マスターサーバーへ接続する。
        /// </summary>
        private void MatchingButton()
        {
            PhotonNetwork.ConnectUsingSettings();
            matchingButton.MyInteractable = false;
            ruleButton.MyInteractable = false;
            _cancelObject.SetActive(true);
        }

        /// <summary>
        /// マスターサーバーへ接続する。
        /// </summary>
        private void CancelButton()
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            _isMatching = false;
            _inRoom = false;
            matchingButton.MyInteractable = true;
            ruleButton.MyInteractable = true;
            _cancelObject.SetActive(false);
        }

        /// <summary>
        /// マスターサーバー接続時にランダムマッチングを行う。
        /// </summary>
        public override void OnConnectedToMaster()
        {
            switch (_currentRule.Value)
            {
                case RuleState.Casual:
                    _roomOptions.IsVisible = true;
                    PhotonNetwork.JoinRandomRoom();
                    break;
                case RuleState.Rating:
                    /*
                     * 未実装　かなりややこしい
                     */
                    break;
                case RuleState.Private:
                    passwordInputField.interactable = false;
                    _roomOptions.IsVisible = false;
                    PhotonNetwork.JoinOrCreateRoom(passwordInputField.text, _roomOptions, TypedLobby.Default);
                    break;
            }
        }

        /// <summary>
        /// ルームに参加する。満員ならそのルームを閉じる。
        /// </summary>
        public override void OnJoinedRoom()
        {
            _inRoom = true;
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    
        /// <summary>
        /// ルームが無かったらルームを作成する。
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, _roomOptions);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            PhotonNetwork.Disconnect();
            ruleButton.MyInteractable = true;
            passwordInputField.text = string.Empty;
            _cancelObject.SetActive(false);
        }

        private void Update()
        {
            // ルームが2人になったらオンライン戦を始める
            if (_isMatching) return;
            if (!_inRoom) return;
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                _isMatching = true;
                _inRoom = false;
                PlayerConfig.IsOnline = true;
                SceneManager.LoadScene("BattleOnline");
            }
        }
    }
}
