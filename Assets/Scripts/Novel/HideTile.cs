using UnityEngine;

namespace storyMode
{
    public class HideTile : MonoBehaviour
    {
        /// <summary>
        /// 非表示のオブジェクトを特定の条件で表示する
        /// </summary>
        [SerializeField] private GameObject[] hideTile;

        public void OnClick(bool _bool)
        {
            var PlayerObject = GameObject.FindGameObjectWithTag("Player");
            if (PlayerCanMoveDistance(PlayerObject.transform.position))
            {
                foreach (var item in hideTile)
                {
                    item.SetActive(_bool);
                }
            }
        }
        //プレイヤーが移動可能な距離の場合はtrueの値を返す
        private bool PlayerCanMoveDistance(Vector3 moveTargetPosition)
        {
            var distance = Vector3.Distance(gameObject.transform.position, moveTargetPosition);
            if (distance < 180) return true;
            else return false;
        }
    }
}