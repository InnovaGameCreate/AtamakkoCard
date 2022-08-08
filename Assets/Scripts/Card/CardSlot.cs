using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class CardSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        private int _cardID;
        [SerializeField] private CardController cardPrefab;
        public int MyCardID
        {
            get => _cardID;
            private set => _cardID = value;
        }
        private Hand _hand;
        private GameObject _draggingCard;
        private Transform _canvasTransform;
        
        // Start is called before the first frame update
        void Start()
        {
            _canvasTransform = FindObjectOfType<Canvas>().transform;

            _hand = FindObjectOfType<Hand>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void SetCard(int cardID)
        {
            if (cardID > 0)
            {
                var card = Instantiate(cardPrefab, transform);
                card.Init(CardData.CardDataArrayList[cardID]);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Drag Start");

            GameObject draggedCard = this.gameObject;
            _draggingCard = Instantiate(draggedCard, _canvasTransform);
            _draggingCard.transform.SetAsFirstSibling();
            _hand.SetGrabbingCardID(MyCardID);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Drag Now");
            _draggingCard.transform.position = _hand.transform.position;
        }


        public void OnDrop(PointerEventData eventData)
        {
            if (!_hand.IsHavaintCardID()) return;
            
            int gotCardID = _hand.GetGrabbingCardID();
            
            _hand.SetGrabbingCardID(MyCardID);
            SetCard(gotCardID);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(_draggingCard);

            int gotCardID = _hand.GetGrabbingCardID();
            SetCard(gotCardID);
        }
    }
}
