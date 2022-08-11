using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UnityEngine.UI;

namespace Stamp
{
    public class StampManager : MonoBehaviourPunCallbacks
    {
        // [SerializeField] private List<GameObject> buttons;
        
        [SerializeField] private List<InputStamp> _listStamp;
        [SerializeField] private List<Sprite> _listSprites;

        private GameObject _mStamp;
        private GameObject _obj;
        private readonly Vector3 _myStamp = new Vector3(0, -80);
        private readonly Vector3 _enemyStamp = new Vector3(0, 80);
        
        // Start is called before the first frame update
        private void Start()
        {
            /* 
            foreach (var stamps in buttons)
            {
                _listStamp.Add(stamps.GetComponent<InputStamp>());
                _listSprites.Add(stamps.GetComponent<Image>().sprite);
            }
             */
            
            _mStamp = (GameObject) Resources.Load("IStamp");
            foreach (var inputStamp in _listStamp)
                inputStamp.OnClickStamp
                    .Subscribe(stampID =>
                    {
                        SendStamp(_myStamp, stampID);
                        photonView.RPC(nameof(SendStamp), RpcTarget.Others, _enemyStamp, stampID);
                    })
                    .AddTo(this);
        }

        [PunRPC]
        private void SendStamp(Vector3 vector3, byte stampID)
        {
            if (_obj != null)
            {
                Destroy(_obj);
            }
            _obj = Instantiate(_mStamp, vector3, Quaternion.identity);
            _obj.GetComponent<Image>().sprite = _listSprites[stampID];
            _obj.transform.SetParent(this.transform, false);
        }
    }

}
