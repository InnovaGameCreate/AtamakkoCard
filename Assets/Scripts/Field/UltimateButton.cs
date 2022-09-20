using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Field
{
    public class UltimateButton : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _interactable = new ReactiveProperty<bool>(false);

        [SerializeField] private GameObject uiUltimate;
    
        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }
    
        [SerializeField] private Button bUltimate;

        private void Start()
        {
            bUltimate.OnClickAsObservable()
                .Subscribe(_ => uiUltimate.SetActive(true))
                .AddTo(this);
            
            _interactable
                .Subscribe(b => bUltimate.interactable = b)
                .AddTo(this);
        }
    }
}
