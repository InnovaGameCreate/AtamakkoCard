using System;
using System.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DecisionButton : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _interactable = new ReactiveProperty<bool>(false);
        private readonly Subject<bool> _decision = new Subject<bool>();
        public IObservable<bool> Decision => _decision;

        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }

        [SerializeField] private Button bDecision;

        private void Start()
        {
            bDecision.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    SeManager.Instance.ShotSe(SeType.ButtonClick);
                    _decision.OnNext(true);
                })
                .AddTo(this);
            
            _interactable
                .Subscribe(b => bDecision.interactable = b)
                .AddTo(this);
        }
    }
}