using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class SwitchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _myButton;
        
        void Start()
        {
            _myButton = gameObject.GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _myButton.interactable = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _myButton.interactable = false;
        }
    }
}
