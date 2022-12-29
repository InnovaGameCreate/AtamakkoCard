using UnityEngine;

namespace storyMode
{
    public class DisplayPanel : MonoBehaviour
    {
        /// <summary>
        /// 非表示のオブジェクトを特定の条件で表示する
        /// </summary>
        [SerializeField] private GameObject TargetObject;

        public void OnClick(bool _bool)
        {
            TargetObject.SetActive(_bool);
        }
    }
}