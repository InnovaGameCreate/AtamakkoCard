using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class DropPlace : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            CardMove card = eventData.pointerDrag.GetComponent<CardMove>();
            if (card != null)
            {
                card.cardParent = transform;
            }
        }
    }
}
