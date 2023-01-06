using Photon.Pun;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadLobby : MonoBehaviourPunCallbacks
    {
        public void LeaveButton()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MatchingManager");
        }
    }
}
