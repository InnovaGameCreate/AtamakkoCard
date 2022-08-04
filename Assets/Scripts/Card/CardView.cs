using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText, iniText, expText;
        [SerializeField] private Image cardImage;
        [SerializeField] private GameObject place;

        public void Show(CardModel cardModel)
        {
            nameText.text = cardModel.Name;
            iniText.text = cardModel.Initiative.ToString();
            expText.text = cardModel.Explanation;
            
            int i = 0;
            foreach (var str in cardModel.Attack)
            {
                if(str == "〇") place.transform.GetChild(i).gameObject.SetActive(true);
                i++;
            }
        }
    }
}
