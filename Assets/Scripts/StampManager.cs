using Photon.Pun;
using UnityEngine;

public class StampManager : MonoBehaviourPunCallbacks
{
    private GameObject mStamp;
    private GameObject obj;
    private Vector3 MyStamp = new Vector3(0, -200);
    private Vector3 EnemyStamp = new Vector3(0, 200);
    
    // Start is called before the first frame update
    void Start()
    {
        mStamp = (GameObject) Resources.Load("IStamp");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SendStamp(MyStamp);
            photonView.RPC(nameof(SendStamp), RpcTarget.Others, EnemyStamp);
        }
    }

    [PunRPC]
    private void SendStamp(Vector3 vector3)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
        obj = Instantiate(mStamp, vector3, Quaternion.identity);
        obj.transform.SetParent(this.transform, false);
    }
}
