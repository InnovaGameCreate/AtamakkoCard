using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _interactable = new ReactiveProperty<bool>(true);
        private readonly Subject<bool> _pushed = new Subject<bool>();
        public IObservable<bool> Pushed => _pushed;

        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }

        private Button _button;

        private void Start()
        {
            _button = gameObject.GetComponent<Button>();
            
            _button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _pushed.OnNext(true);
                })
                .AddTo(this);
            
            _interactable
                .Subscribe(b => _button.interactable = b)
                .AddTo(this);
        }
    }
}
