using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class CardController : MonoBehaviour, IPointerClickHandler
    {
        public int CardID { get; set; }
        
        public CardView view;
        private CardModel _model;

        private CardMovement _cardMovement;

        private void Awake()
        {
            view = GetComponent<CardView>();
            _cardMovement = GetComponent<CardMovement>();
        }

        public void Init(int id)
        {
            CardID = id;
            _model = new CardModel(CardData.CardDataArrayList[CardID]);
            view.Show(_model);
        }

        public void FlipOver()
        {
            view.backCard.SetActive(!view.backCard.activeSelf);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("クリックされました" + CardID);
        }
    }
}
