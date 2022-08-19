using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class CardSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        private int _cardID = -1;
        
        [SerializeField] private GameObject cardPrefab;

        public int MyCardID
        {
            get => _cardID;
            set => _cardID = value;
        }

        private Hand _hand;
        private GameObject _draggingCard;
        private Transform _canvasTransform;
        
        void Start()
        {
            _canvasTransform = FindObjectOfType<Canvas>().transform;

            _hand = FindObjectOfType<Hand>();
        }

        public void CreateCard(int cardID)
        {
            MyCardID = cardID;
            if (cardID >= 0)
            {
                var card = Instantiate(cardPrefab, transform);
                card.GetComponent<CardController>().Init(CardData.CardDataArrayList[_cardID]);
            }
            else
            {
                foreach (Transform childObj in transform)
                {
                    Destroy(childObj.gameObject);
                }
            }
            Debug.Log(gameObject.name + "," +_cardID);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_cardID == -1) return;
            _draggingCard = Instantiate(cardPrefab, _canvasTransform);
            _draggingCard.GetComponent<CardController>().Init(CardData.CardDataArrayList[_cardID]);
            _draggingCard.transform.SetAsLastSibling();
            Debug.Log(_cardID);
            _hand.SetGrabbingCardID(_cardID);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_cardID == -1) return;
            _draggingCard.transform.position = _hand.transform.position;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop" + gameObject.name);
            if (!_hand.IsHavaintCardID()) return;
            
            int gotCardID = _hand.GetGrabbingCardID();
            
            _hand.SetGrabbingCardID(_cardID);
            CreateCard(gotCardID);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(_draggingCard);

            int gotCardID = _hand.GetGrabbingCardID();
            CreateCard(gotCardID);
        }
    }
}
