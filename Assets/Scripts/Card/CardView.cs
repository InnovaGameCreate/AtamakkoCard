using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText, iniText, expText;
        [SerializeField] private Image cardSprite;
        [SerializeField] private GameObject place;
        public GameObject backCard;
        public GameObject shadow;

        public void Show(CardModel cardModel)
        {
            nameText.text = cardModel.Name;
            iniText.text = cardModel.Initiative.ToString();
            expText.text = cardModel.Explanation;
            CardIcon cardIcon = Resources.Load<CardIcon>("CardIcon");
            cardSprite.sprite = cardIcon.cardIconList[cardModel.ID];
            
            int i = 0;
            foreach (var str in cardModel.Attack)
            {
                if(str == "〇") place.transform.GetChild(i).gameObject.SetActive(true);
                i++;
            }

            int j = 0;
            foreach (var str in cardModel.Move)
            {
                if(str == "〇") place.transform.GetChild(j).gameObject.SetActive(true);
                j++;
            }
        }
    }
}
