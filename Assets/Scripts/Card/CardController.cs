using UI;
using UnityEngine;

namespace Card
{
    public class CardController : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
    {
        public int CardID { get; set; }
        
        public CardView view;
        public CardModel Model { get; private set; }

        private UIExplanation _explanation;

        private void Awake()
        {
            view = GetComponent<CardView>();
        }

        public void Init(int id)
        {
            CardID = id;
            Model = new CardModel(CardData.CardDataArrayList[CardID]);
            view.Show(Model);
            //_explanation = GameObject.FindGameObjectWithTag("Explanation").GetComponent<UIExplanation>();
        }

        public void FlipOver()
        {
            view.backCard.SetActive(!view.backCard.activeSelf);
        }

        /*
        public void OnPointerEnter(PointerEventData eventData)
        {
            _explanation.SetUIActive(true);
            _explanation.CardID.Value = CardID;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _explanation.CardID.Value = -1;
            _explanation.SetUIActive(false);
        }
        */
    }
}
