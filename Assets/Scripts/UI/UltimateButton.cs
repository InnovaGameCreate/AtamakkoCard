using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UltimateButton : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _interactable = new ReactiveProperty<bool>(false);

        [SerializeField] private GameObject uiUltimate;
        private Button _bUltimate;

        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }
    

        private void Start()
        {
            _bUltimate = gameObject.GetComponent<Button>();
            
            _bUltimate.OnClickAsObservable()
                .Subscribe(_ => uiUltimate.SetActive(true))
                .AddTo(this);
            
            _interactable
                .Subscribe(b => _bUltimate.interactable = b)
                .AddTo(this);
        }
    }
}
