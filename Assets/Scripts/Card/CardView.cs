using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText, iniText, expText;
        [SerializeField] private Image cardSprite;
        [SerializeField] private CardTypeIcon cardTypeIcon;
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
            cardTypeIcon.setImage(cardModel.Kind);

            if (cardModel.Kind == "攻撃")
            {
                int i = 0;
                foreach (var str in cardModel.Attack)
                {
                    if (str == "〇")
                    {
                        var attackPlace = place.transform.GetChild(i).gameObject;
                        attackPlace.SetActive(true);
                        attackPlace.GetComponent<Image>().color = Color.red;
                    }

                    i++;
                }
            }

            if (cardModel.Kind == "移動")
            {
                int j = 0;
                foreach (var str in cardModel.Move)
                {
                    if (str == "〇")
                    {
                        var movePlace = place.transform.GetChild(j).gameObject;
                        movePlace.SetActive(true);
                        movePlace.GetComponent<Image>().color = new Color(0f, 1f, 0f);
                    }

                    j++;
                }
            }
        }
    }
}