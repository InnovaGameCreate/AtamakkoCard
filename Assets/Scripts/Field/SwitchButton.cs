using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Field
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
