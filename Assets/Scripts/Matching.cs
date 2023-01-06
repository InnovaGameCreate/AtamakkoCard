using Photon.Pun;
using Photon.Realtime;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Matching : MonoBehaviourPunCallbacks
{
    private bool _inRoom;
    private bool _isMatching;

    [SerializeField] private ButtonController onlineMatching;
    [SerializeField] private ButtonController cpuBattle;

    /*
    [SerializeField] private GameObject privateUI;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button joinButton;
    */

    private void Start()
    {
        onlineMatching.Pushed
            .Subscribe(_ =>
            {
                cpuBattle.MyInteractable = false;
                MatchingButton();
            })
            .AddTo(this);

        cpuBattle.Pushed
            .Subscribe(_ =>
            {
                onlineMatching.MyInteractable = false;
                CPUButton();
            })
            .AddTo(this);
    }

    public void CPUButton()
    {
        PlayerConfig.IsOnline = false;
        SceneManager.LoadScene("BattleCPU");
    }
    
    public void MatchingButton()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Joined");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        _inRoom = true;
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    
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
        if (_isMatching)
        {
            return;
        }

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
