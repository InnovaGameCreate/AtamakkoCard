using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assemble
{
    public class slotGrid : MonoBehaviour
    {
        [SerializeField]
        private GameObject slotPrefab;

        [SerializeField]
        private Item[] allItems;

        private int slotNumber;
        void Start()
        {
            slotNumber = allItems.Length;
            for (int i = 0; i < slotNumber; i++)
            {
                GameObject slotObj = Instantiate(slotPrefab, this.transform);

                slot slot = slotObj.GetComponent<slot>();

                if (i < allItems.Length)
                {
                    slot.setItem(allItems[i]);
                }
                else
                {
                    slot.setItem(null);
                }

            }
        }

    }
}
