using Atamakko;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// HPを表示するUIクラス
    /// </summary>
    public class UiHp : MonoBehaviour
    {
        [SerializeField] private AtamakkoData atamakko; // 表示するアタマッコのステータス
        private Image _hpGauge; // 表示するImage
        
        void Start()
        {
            _hpGauge = gameObject.GetComponent<Image>();

            // HPに応じて表示する
            atamakko.MyHp
                .Subscribe(hp => _hpGauge.fillAmount = (float)hp / 6)
                .AddTo(this);
        }
    }
}
