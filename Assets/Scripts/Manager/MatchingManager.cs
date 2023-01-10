using Photon.Pun;
using Photon.Realtime;
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
        private bool _inRoom; // ルームに入っているかどうか
        private bool _isMatching; // マッチングしたかどうか

        [SerializeField] private ButtonController onlineMatching; // オンライン戦のボタン
        [SerializeField] private ButtonController cpuBattle; // CPU戦のボタン

        private void Start()
        {
            // オンライン戦のボタンが押されたときの処理
            onlineMatching.Pushed
                .Subscribe(_ =>
                {
                    cpuBattle.MyInteractable = false;
                    MatchingButton();
                })
                .AddTo(this);

            // CPU戦のボタンが押されたときの処理
            cpuBattle.Pushed
                .Subscribe(_ =>
                {
                    onlineMatching.MyInteractable = false;
                    CPUButton();
                })
                .AddTo(this);
        }

        /// <summary>
        /// オフラインモードでBattleCPUシーンへ行く。
        /// </summary>
        private void CPUButton()
        {
            PlayerConfig.IsOnline = false;
            SceneManager.LoadScene("BattleCPU");
        }

        /// <summary>
        /// マスターサーバーへ接続する。
        /// </summary>
        private void MatchingButton()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        /// <summary>
        /// マスターサーバー接続時にランダムマッチングを行う。
        /// </summary>
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomRoom();
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
            var roomOptions = new RoomOptions
            {
                MaxPlayers = 2,
                //IsVisible = false
            };

            PhotonNetwork.CreateRoom(null, roomOptions);
        }

        private void Update()
        {
            // ルームが2人になったらオンライン戦を始める
            if (_isMatching) return;
            if (_inRoom)
            {
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
}
