using System;
using Field;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class CardMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        private Hand _hand;
        private Transform _canvasTransform;
        private GameObject _draggingCard;
        [SerializeField] private GameObject cardPrefab;
        private CardSlot _slot;

        private readonly Subject<int> _settingCard = new Subject<int>();
        public IObservable<int> CheckCardID => _settingCard;
        
        void Start()
        {
            _canvasTransform = GameObject.FindGameObjectWithTag("Stage").transform;
            _hand = FindObjectOfType<Hand>();
            _slot = gameObject.GetComponent<CardSlot>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            _draggingCard = Instantiate(cardPrefab, _canvasTransform);
            _draggingCard.GetComponent<CardController>().Init(CardData.CardDataArrayList[_slot.MyCardID]);
            _draggingCard.transform.SetAsLastSibling();
            _hand.SetGrabbingCardID(_slot.MyCardID);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            _draggingCard.transform.position = _hand.transform.position;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (!_hand.IsHavaintCardID()) return;
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            
            int gotCardID = _hand.GetGrabbingCardID();
            
            _hand.SetGrabbingCardID(_slot.MyCardID);
            _slot.CreateCard(gotCardID);
            
            _settingCard.OnNext(_slot.MyCardID);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (BattleManager.Instance.MyGameState.Value != BattleManager.State.Select) return;
            Destroy(_draggingCard);

            int gotCardID = _hand.GetGrabbingCardID();
            _slot.CreateCard(gotCardID);
            Debug.Log(gotCardID);
            
            _settingCard.OnNext(_slot.MyCardID);
        }
    }
}
