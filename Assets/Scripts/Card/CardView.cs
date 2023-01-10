using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    /// <summary>
    /// カードの見た目を設定するクラス
    /// </summary>
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText, iniText, expText; // 名前，先制度，説明文
        [SerializeField] private Image cardSprite; // カードアイコン
        [SerializeField] private CardTypeIcon cardTypeIcon; // カードの種類アイコン
        [SerializeField] private GameObject place; // カードの間合い
        public GameObject backCard; // カードの裏側
        public GameObject shadow; // 影

        /// <summary>
        /// カードの見た目を設定する。
        /// </summary>
        /// <param name="cardModel">カードデータ</param>
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