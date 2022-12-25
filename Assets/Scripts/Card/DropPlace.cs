using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class DropPlace : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Hand hand;
        [SerializeField] private CardSlot slotPrefab;
        public void OnDrop(PointerEventData eventData)
        {
            if (hand.GetGrabbingCardID() >= 0)
            {
                var cardSlot = Instantiate(slotPrefab);
                cardSlot.CreateCard(hand.GetGrabbingCardID());
            }
        }
    }
}
