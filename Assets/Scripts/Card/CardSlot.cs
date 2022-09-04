using System;
using Field;
using UniRx;
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

        private readonly Subject<int> _settingCard = new Subject<int>();
        public IObservable<int> CheckCardID => _settingCard;

        void Start()
        {
            _canvasTransform = GameObject.FindGameObjectWithTag("Stage").transform;

            _hand = FindObjectOfType<Hand>();

            BattleManager.Instance.MyGameState
                .Where(s => s == BattleManager.State.Battle)
                .Subscribe(_ =>
                {
                    if (MyCardID < 0)
                    {
                        Destroy(gameObject);
                    }
                })
                .AddTo(this);
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
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_cardID == -1) return;
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            _draggingCard = Instantiate(cardPrefab, _canvasTransform);
            _draggingCard.GetComponent<CardController>().Init(CardData.CardDataArrayList[_cardID]);
            _draggingCard.transform.SetAsLastSibling();
            _hand.SetGrabbingCardID(_cardID);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_cardID == -1) return;
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            _draggingCard.transform.position = _hand.transform.position;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (!_hand.IsHavaintCardID()) return;
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            
            int gotCardID = _hand.GetGrabbingCardID();
            
            _hand.SetGrabbingCardID(_cardID);
            CreateCard(gotCardID);
            
            _settingCard.OnNext(_cardID);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            Destroy(_draggingCard);

            int gotCardID = _hand.GetGrabbingCardID();
            CreateCard(gotCardID);
            Debug.Log(gotCardID);
            
            _settingCard.OnNext(_cardID);
        }
    }
}
