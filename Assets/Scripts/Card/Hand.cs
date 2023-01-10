using UnityEngine;

namespace Card
{
    /// <summary>
    /// ドラッグ＆ドロップ時にカードIDを格納するクラス
    /// </summary>
    public class Hand : MonoBehaviour
    {
        private int _grabbingCardID = -1; // 持っているカードID

        void Update()
        {
            // 座標をカメラに映るようにする
            if (Camera.main != null)
            {
                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = 0f;
                transform.position = targetPos;
            }
        }
        
        /// <summary>
        /// 持っているカードIDを取得する。
        /// </summary>
        /// <returns>持っているカードID</returns>
        public int GetGrabbingCardID()
        {
            int oldCardID = _grabbingCardID;
            _grabbingCardID = -1;
            return oldCardID;
        }

        /// <summary>
        /// 持っているカードIDを格納する。
        /// </summary>
        /// <param name="cardID">持っているカードID</param>
        public void SetGrabbingCardID(int cardID)
        {
            _grabbingCardID = cardID;
        }

        /// <summary>
        /// カードを持っているかどうかを判断する。
        /// </summary>
        /// <returns>カードを持っているかどうか</returns>
        public bool IsHavingCardID()
        {
            return _grabbingCardID != -1;
        }
    }
}
