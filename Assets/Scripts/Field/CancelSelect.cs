using UnityEngine;
using UnityEngine.EventSystems;

namespace Field
{
    public class CancelSelect : MonoBehaviour, IEndDragHandler
    {
        [SerializeField] private GameObject uiUltimate;

        public void OnEndDrag(PointerEventData eventData)
        {
            uiUltimate.SetActive(false);
        }
    }
}
