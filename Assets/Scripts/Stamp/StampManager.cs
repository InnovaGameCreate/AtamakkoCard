using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UnityEngine.UI;

namespace Stamp
{
    /// <summary>
    /// スタンプ機能を管理するクラス
    /// </summary>
    public class StampManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private List<InputStamp> listStamp; // 
        [SerializeField] private List<Sprite> listSprites;
        [SerializeField] private GameObject mStamp;

        private GameObject _obj; // スタンプのオブジェクト
        private readonly Vector3 _myStamp = new(0, -80); // 自身のスタンプの位置
        private readonly Vector3 _enemyStamp = new(0, 80); // 相手のスタンプの位置
        
        private void Start()
        {
            foreach (var inputStamp in listStamp)
                // スタンプが押されたときの処理
                inputStamp.OnClickStamp
                    .Subscribe(stampID =>
                    {
                        SendStamp(true, stampID);
                        photonView.RPC(nameof(SendStamp), RpcTarget.Others, false, stampID);
                    })
                    .AddTo(this);
        }

        /// <summary>
        /// スタンプを表示する。
        /// </summary>
        /// <param name="isMine">自分のスタンプかどうか</param>
        /// <param name="stampID">スタンプID</param>
        [PunRPC]
        private void SendStamp(bool isMine, byte stampID)
        {
            if (_obj != null)
            {
                Destroy(_obj);
            }
            _obj = isMine ? Instantiate(mStamp, _myStamp, Quaternion.identity) : Instantiate(mStamp, _enemyStamp, Quaternion.identity);
            _obj.GetComponent<Image>().sprite = listSprites[stampID];
            _obj.transform.SetParent(transform, false);
        }
    }

}
