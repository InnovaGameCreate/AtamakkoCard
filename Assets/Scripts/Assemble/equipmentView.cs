using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assemble
{
    public class equipmentView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image cardSprite;
        public GameObject backCard; // �J�[�h�̗���

        public void Show(equipmentModel cardModel)
        {
            nameText.text = cardModel.Name;
            equipmentIcon cardIcon = Resources.Load<equipmentIcon>("EquipmentIcon");
            cardSprite.sprite = cardIcon.equipmentIconList[cardModel.ID];
        }
    }
}