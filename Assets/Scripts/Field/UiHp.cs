using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Field
{
    public class UiHp : MonoBehaviour
    {
        [SerializeField] private HpStatus atamakko;
        private Slider _hpGauge;
        
        void Start()
        {
            _hpGauge = gameObject.GetComponent<Slider>();

            atamakko.MyHP
                .Subscribe(hp => _hpGauge.value = (float)hp / 6)
                .AddTo(this);
        }
    }
}
