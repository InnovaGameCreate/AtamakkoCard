using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// ボタンのON・OFFを切り替えるクラス
    /// </summary>
    public class SwitchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _myButton; // アタッチ先のボタン
        
        void Start()
        {
            _myButton = gameObject.GetComponent<Button>();
        }

        /// <summary>
        /// マウスポインタがボタン上ならボタンをONにする。
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            _myButton.interactable = true;
        }

        /// <summary>
        /// マウスポインタがボタン外に出たときにボタンをOFFにする。
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            _myButton.interactable = false;
        }
    }
}
