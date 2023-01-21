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
        [SerializeField] private GameObject firstPlace; // カードの第一間合い
        [SerializeField] private GameObject secondPlace; // カードの第二間合い
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

            switch (cardModel.Kind)
            {
                case "攻撃":
                {
                    for (var i = 0; i < cardModel.Attack.Length; i++)
                    {
                        if (cardModel.Attack[i] != "〇") continue;
                        var attackPlace = firstPlace.transform.GetChild(i).gameObject;
                        attackPlace.SetActive(true);
                        attackPlace.GetComponent<Image>().color = Color.red;
                    }

                    if (cardModel.Additional == "◎")
                    {
                        for (var i = 1; i < cardModel.Move.Length; i++)
                        {
                            if (cardModel.Move[i] != "〇") continue;
                            var movePlace = secondPlace.transform.GetChild(i).gameObject;
                            movePlace.SetActive(true);
                            movePlace.GetComponent<Image>().color = Color.green;
                        }
                    }
                    break;
                }
                case "移動":
                {
                    for (var i = 0; i < cardModel.Move.Length; i++)
                    {
                        if (cardModel.Move[i] != "〇") continue;
                        var movePlace = firstPlace.transform.GetChild(i).gameObject;
                        movePlace.SetActive(true);
                        movePlace.GetComponent<Image>().color = Color.green;
                    }

                    if (cardModel.Additional == "◎")
                    {
                        for (var i = 1; i < cardModel.Attack.Length; i++)
                        {
                            if (cardModel.Attack[i] != "〇") continue;
                            var attackPlace = secondPlace.transform.GetChild(i).gameObject;
                            attackPlace.SetActive(true);
                            attackPlace.GetComponent<Image>().color = Color.red;
                        }
                    }
                    break;
                }
            }
        }
    }
}