using UnityEngine;
using UnityEngine.EventSystems;

namespace Field
{
    public class CancelSelect : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject uiUltimate;

        public void OnPointerClick(PointerEventData eventData)
        {
            uiUltimate.SetActive(false);
        }
    }
}
