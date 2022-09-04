using Photon.Pun;
using Photon.Realtime;

public class TestScene : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";
        
        // PhotonServiceSettingsの設定内容を使ってマスターサーバへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        var roomOptions = new RoomOptions
        {
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
}
