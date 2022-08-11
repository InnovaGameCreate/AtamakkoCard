using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class CardSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        [SerializeField] private GameObject cardPrefab;
        public int MyCardID { get; set; } = -1;

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
                card.GetComponent<CardController>().Init(CardData.CardDataArrayList[MyCardID]);
            }
            Debug.Log(gameObject.name + "," +MyCardID);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (MyCardID == -1) return;
            _draggingCard = Instantiate(cardPrefab, _canvasTransform);
            _draggingCard.GetComponent<CardController>().Init(CardData.CardDataArrayList[MyCardID]);
            //_draggingCard.GetComponent<CardSlot>().MyCardID = MyCardID;
            _draggingCard.transform.SetAsLastSibling();
            Debug.Log(MyCardID);
            _hand.SetGrabbingCardID(MyCardID);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (MyCardID == -1) return;
            _draggingCard.transform.position = _hand.transform.position;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop" + gameObject.name);
            if (!_hand.IsHavaintCardID()) return;
            
            int gotCardID = _hand.GetGrabbingCardID();
            
            _hand.SetGrabbingCardID(MyCardID);
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
