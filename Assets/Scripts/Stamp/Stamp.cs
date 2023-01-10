using UnityEngine;

namespace Stamp
{
    /// <summary>
    /// スタンプクラス
    /// </summary>
    public class Stamp : MonoBehaviour
    {
        // 表示して３秒後に消す
        void Start()
        {
            Destroy(this.gameObject, 3f);
        }
    
    }
}

