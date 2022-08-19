using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Field
{
    public class DecisionButton : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _interactable = new ReactiveProperty<bool>(false);

        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }

        [SerializeField] private Button bDecision;

        private void Start()
        {
            bDecision.OnClickAsObservable()
                .Subscribe(_ => Debug.Log("Clicked"))
                .AddTo(this);
            
            _interactable
                .Subscribe(b => bDecision.interactable = b)
                .AddTo(this);
        }
    }
}
