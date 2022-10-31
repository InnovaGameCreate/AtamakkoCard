using Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiHp : MonoBehaviour
    {
        [SerializeField] private AtamakkoStatus atamakko;
        private Slider _hpGauge;
        
        void Start()
        {
            _hpGauge = gameObject.GetComponent<Slider>();

            atamakko.MyHp
                .Subscribe(hp => _hpGauge.value = (float)hp / 6)
                .AddTo(this);
        }
    }
}
