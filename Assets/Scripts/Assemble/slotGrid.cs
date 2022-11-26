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
        private GameObject[] parentObjects = new GameObject[4];

        private int slotNumber;
        void Start()
        {
            StartCoroutine(SetUp());
        }

        IEnumerator SetUp()
        {
            
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < Resources.Load<equipmentIcon>("EquipmentIcon").equipmentIconList.Count; i++)
            {
 
                Init(equipmentData.CardDataArrayList[i]);
            }
        }

        public void Init(string[] datalist)
        {
            if (!PlayerConfig.unLockEquipment[int.Parse(datalist[0])])
            {
                return;
            }
            GameObject slotObj = null;
            if (datalist[2] == "上部")
            {
                slotObj = Instantiate(slotPrefab, parentObjects[0].transform);
            }
            else if (datalist[2] == "中央")
            {
                slotObj = Instantiate(slotPrefab, parentObjects[1].transform);
            }
            else if (datalist[2] == "下部")
            {
                slotObj = Instantiate(slotPrefab, parentObjects[2].transform);
            }
            else if (datalist[2] == "アクセサリ")
            {
                slotObj = Instantiate(slotPrefab, parentObjects[3].transform);
            }
            slot slot = slotObj.GetComponent<slot>();
            Item item = new Item();
            item.MyItemName = datalist[1];
            equipmentIcon cardIcon = Resources.Load<equipmentIcon>("EquipmentIcon");
            item.MyCardID = int.Parse(datalist[0]);
            item.MyItemImage = cardIcon.equipmentIconList[int.Parse(datalist[0])];
            item.MyCardNum1 = int.Parse(datalist[4]);
            item.MyCardNum2 = int.Parse(datalist[5]);

            if (datalist[2] == "上部")
            {
                item.MyPartsType = Item.slotType.upper;
            }
            else if (datalist[2] == "中央")
            {
                item.MyPartsType = Item.slotType.mid;
            }
            else if (datalist[2] == "下部")
            {
                item.MyPartsType = Item.slotType.lower;
            }
            else if (datalist[2] == "アクセサリ")
            {
                item.MyPartsType = Item.slotType.accessory;
            }

                slot.setItem(item);
        }

    }
}
