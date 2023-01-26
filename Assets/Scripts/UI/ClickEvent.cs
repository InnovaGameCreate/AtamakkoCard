using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ClickEvent : MonoBehaviour, IPointerClickHandler
    {
        public IObservable<bool> Clicked => _clicked;
        private readonly Subject<bool> _clicked = new Subject<bool>();

        /// <summary>
        /// クリックされたときに特定のUIを非表示にする
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnPointerClick(PointerEventData eventData)
        {
           _clicked.OnNext(true);
        }
    }
}
