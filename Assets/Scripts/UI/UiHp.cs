using Atamakko;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiHp : MonoBehaviour
    {
        [SerializeField] private AtamakkoData atamakko;
        //private Slider _hpGauge;
        private Image _hpGauge;
        
        void Start()
        {
            //_hpGauge = gameObject.GetComponent<Slider>();
            _hpGauge = gameObject.GetComponent<Image>();

            atamakko.MyHp
                .Subscribe(hp => _hpGauge.fillAmount = (float)hp / 6)
                .AddTo(this);
        }
    }
}
