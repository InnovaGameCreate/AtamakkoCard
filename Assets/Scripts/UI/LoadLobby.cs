using Photon.Pun;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// ロビーに移動するクラス
    /// </summary>
    public class LoadLobby : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// ネットワーク上のルームから抜ける。
        /// </summary>
        public void LeaveButton()
        {
            PhotonNetwork.LeaveRoom();
        }

        /// <summary>
        /// Matchingシーンへ移動する。
        /// </summary>
        public override void OnLeftRoom()
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("Matching");
        }
    }
}
