using System;
using Manager;
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
        private bool _portable;

        private readonly Subject<int> _settingCard = new Subject<int>();
        public IObservable<int> CheckCardID => _settingCard;
        
        void Start()
        {
            _canvasTransform = GameObject.FindGameObjectWithTag("Stage").transform;
            _hand = FindObjectOfType<Hand>();
            _slot = gameObject.GetComponent<CardSlot>();

            if (PlayerConfig.IsOnline)
            {
                OnlineManager.Instance.CurrentState
                    .Subscribe(state => _portable = state == GameState.Select)
                    .AddTo(this);
            }
            else
            {
                CPUManager.Instance.CurrentState
                    .Subscribe(state => _portable = state == GameState.Select)
                    .AddTo(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (!_portable) return;
            
            // ドラッグ時のカード生成
            _draggingCard = Instantiate(cardPrefab, _canvasTransform);
            _draggingCard.GetComponent<CardController>().Init(_slot.MyCardID);
            _draggingCard.transform.SetAsLastSibling();
            _draggingCard.GetComponent<CanvasGroup>().blocksRaycasts = false;
            
            // Handに持っているカードの情報を渡す
            _hand.SetGrabbingCardID(_slot.MyCardID);
            _slot.MyCard.view.shadow.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (!_portable) return;
            _draggingCard.transform.position = _hand.transform.position;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (!_hand.IsHavingCardID()) return;
            //if (!_portable) return;
            
            int gotCardID = _hand.GetGrabbingCardID();
            
            _hand.SetGrabbingCardID(_slot.MyCardID);
            _slot.DeleteCard();
            _slot.CreateCard(gotCardID);
            
            _settingCard.OnNext(_slot.MyCardID);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (!_portable) return;
            
            _slot.MyCard.view.shadow.SetActive(false);
            Destroy(_draggingCard);

            int gotCardID = _hand.GetGrabbingCardID();
            _slot.CreateCard(gotCardID);
            Debug.Log(gotCardID);
            
            _settingCard.OnNext(_slot.MyCardID);
        }
    }
}
