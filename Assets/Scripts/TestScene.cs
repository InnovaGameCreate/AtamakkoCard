using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestScene : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        // PhotonServiceSettingsの設定内容を使ってマスターサーバへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を作成する
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
    }
}
