using UnityEngine;

namespace Assemble
{
    public class hand : MonoBehaviour
    {
        private Item grabbingItem;
        public Item GetGrabbingItem()
        {
            Item oldItem = grabbingItem;
            grabbingItem = null;
            return oldItem;
        }

        public Item GetGrabbingItemNormal()
        {
            return grabbingItem;
        }

        public void SetGtabbingItem(Item item)
        {
            grabbingItem = item;
        }

        public bool IsHavingItem()
        {
            return grabbingItem != null;
        }
    }
}