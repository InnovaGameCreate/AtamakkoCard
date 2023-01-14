using Assemble;
using UnityEngine;
using UnityEngine.UI;

namespace Atamakko
{
    public class PlayerCharacterVisual : MonoBehaviour
    {
        [SerializeField]
        private Image top, mid, under, accessory1, accessory2, accessory3;
        void Start()
        {
            setImage();
        }

        public void setImage()
        {
            Debug.Log("SetImageOrder");
            equipmentIcon cardIcon = Resources.Load<equipmentIcon>("EquipmentIcon");
            top.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[0]];
            mid.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[1]];
            under.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[2]];
            accessory1.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[3]];
            accessory2.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[4]];
            accessory3.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[5]];

            var Equpment1 = equipmentData.CardDataArrayList[PlayerConfig.Equipmnet[3]];
            var Equpment2 = equipmentData.CardDataArrayList[PlayerConfig.Equipmnet[4]];
            var Equpment3 = equipmentData.CardDataArrayList[PlayerConfig.Equipmnet[5]];
            if (Equpment1[8] == "front") accessory1.gameObject.transform.SetAsLastSibling();
            else accessory1.gameObject.transform.SetAsFirstSibling();

            Debug.Log("SetImageType1:" + Equpment1[8]);
            if (Equpment2[8] == "front") accessory2.gameObject.transform.SetAsLastSibling();
            else accessory2.gameObject.transform.SetAsFirstSibling();
            Debug.Log("SetImageType2:" + Equpment2[8]);
            if (Equpment3[8] == "front") accessory3.gameObject.transform.SetAsLastSibling();
            else accessory3.gameObject.transform.SetAsFirstSibling();
            Debug.Log("SetImageType3:" + Equpment3[8]);

        }
    }
}
