using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Field
{
    public class UiHp : MonoBehaviour
    {
        [SerializeField] private HpStatus atamakko;
        private Slider _hpGauge;
        
        // Start is called before the first frame update
        void Start()
        {
            _hpGauge = gameObject.GetComponent<Slider>();

            atamakko.MyHP
                // ReSharper disable once PossibleLossOfFraction
                .Subscribe(hp => _hpGauge.value = hp / 6)
                .AddTo(this);
        }
    }
}
