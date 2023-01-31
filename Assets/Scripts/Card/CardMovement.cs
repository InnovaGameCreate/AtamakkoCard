using System;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    /// <summary>
    /// カードを移動させるクラス
    /// </summary>
    public class CardMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        private Hand _hand; // ドラッグ時のマウス
        private Transform _canvasTransform; // 表示する際の親
        private GameObject _draggingCard; // ドラッグ時のカードの複製
        [SerializeField] private GameObject cardPrefab; // カードのプレハブ
        private CardSlot _slot; // アタッチするカードのスロット
        private bool _portable; // 移動可能かどうか
        
        // カードをセットしたときにイベント発行
        private readonly Subject<int> _settingCard = new Subject<int>();
        public IObservable<int> CheckCardID => _settingCard;
        
        void Start()
        {
            _canvasTransform = GameObject.FindGameObjectWithTag("Stage").transform;
            _hand = FindObjectOfType<Hand>();
            _slot = gameObject.GetComponent<CardSlot>();
        }

        /// <summary>
        /// ドラッグ開始時にカードを複製＆HandにカードIDを格納させる
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (!BattleManager.Instance.CardMobile.Value) return;

            _draggingCard = Instantiate(cardPrefab, _canvasTransform);
            _draggingCard.GetComponent<CardController>().Init(_slot.MyCardID);
            _draggingCard.transform.SetAsLastSibling();
            _draggingCard.GetComponent<CanvasGroup>().blocksRaycasts = false;
            
            _hand.SetGrabbingCardID(_slot.MyCardID);
            _slot.MyCard.view.shadow.SetActive(true);
        }

        /// <summary>
        /// ドラッグ中にカードの位置をHandの位置にする。
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (!BattleManager.Instance.CardMobile.Value) return;
            _draggingCard.transform.position = _hand.transform.position;
        }
        
        /// <summary>
        /// ドロップしたときにカードを入れ替える。
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnDrop(PointerEventData eventData)
        {
            if (!_hand.IsHavingCardID()) return;
            if (!BattleManager.Instance.CardMobile.Value) return;
            
            int gotCardID = _hand.GetGrabbingCardID();
            
            _hand.SetGrabbingCardID(_slot.MyCardID);
            _slot.DeleteCard();
            _slot.CreateCard(gotCardID);
            
            _settingCard.OnNext(_slot.MyCardID);
        }

        /// <summary>
        /// ドラッグ終了時に複製したカードを消す。
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (_slot.MyCardID == -1) return;
            if (!BattleManager.Instance.CardMobile.Value) return;
            
            _slot.MyCard.view.shadow.SetActive(false);
            Destroy(_draggingCard);

            int gotCardID = _hand.GetGrabbingCardID();
            _slot.CreateCard(gotCardID);
            
            _settingCard.OnNext(_slot.MyCardID);
        }
    }
}
