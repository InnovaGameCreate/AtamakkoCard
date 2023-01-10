using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// クリックされたときに非表示にするクラス
    /// </summary>
    public class CancelSelect : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject uiUltimate; // 非表示にするUI

        /// <summary>
        /// クリックされたときに特定のUIを非表示にする
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            uiUltimate.SetActive(false);
        }
    }
}
