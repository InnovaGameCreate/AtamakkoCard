using System.Collections.Generic;
using Assemble;
using UnityEngine;
using UnityEngine.UI;

namespace Atamakko
{
    public class PlayerCharacterVisual : MonoBehaviour
    {
        [SerializeField]
        private Image top, mid, under, accessory1, accessory2, accessory3;

        public void SetImage(int[] equipments)
        {
            Debug.Log("SetImageOrder");
            equipmentIcon cardIcon = Resources.Load<equipmentIcon>("EquipmentIcon");
            top.sprite = cardIcon.equipmentIconList[equipments[0]];
            mid.sprite = cardIcon.equipmentIconList[equipments[1]];
            under.sprite = cardIcon.equipmentIconList[equipments[2]];
            accessory1.sprite = cardIcon.equipmentIconList[equipments[3]];
            accessory2.sprite = cardIcon.equipmentIconList[equipments[4]];
            accessory3.sprite = cardIcon.equipmentIconList[equipments[5]];

            var equipment1 = equipmentData.CardDataArrayList[equipments[3]];
            var equipment2 = equipmentData.CardDataArrayList[equipments[4]];
            var equipment3 = equipmentData.CardDataArrayList[equipments[5]];
            if (equipment1[8] == "front") accessory1.gameObject.transform.SetAsLastSibling();
            else accessory1.gameObject.transform.SetAsFirstSibling();

            Debug.Log("SetImageType1:" + equipment1[8]);
            if (equipment2[8] == "front") accessory2.gameObject.transform.SetAsLastSibling();
            else accessory2.gameObject.transform.SetAsFirstSibling();
            Debug.Log("SetImageType2:" + equipment2[8]);
            if (equipment3[8] == "front") accessory3.gameObject.transform.SetAsLastSibling();
            else accessory3.gameObject.transform.SetAsFirstSibling();
            Debug.Log("SetImageType3:" + equipment3[8]);

        }
    }
}
