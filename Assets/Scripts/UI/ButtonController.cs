using System;
using System.Audio;
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
        private Button _button;
        [SerializeField] private SeType se;

        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }


        private void Start()
        {
            _button = gameObject.GetComponent<Button>();
            
            _button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _pushed.OnNext(true);
                    SeManager.Instance.ShotSe(se);
                })
                .AddTo(this);
            
            _interactable
                .Subscribe(b => _button.interactable = b)
                .AddTo(this);
        }
    }
}
