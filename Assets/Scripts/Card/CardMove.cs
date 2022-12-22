using System;
using Field;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class CardMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Transform cardParent;
        private Hand _hand;
        public bool Portable { get; set; } = true;

        public void Start()
        {
            _hand = FindObjectOfType<Hand>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!Portable) return;
            cardParent = transform.parent;
            transform.SetParent(cardParent.parent, false);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if(!Portable) return;
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(!Portable) return;
            transform.SetParent(cardParent, false);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
