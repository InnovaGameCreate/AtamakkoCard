using UniRx;
using UnityEngine;

namespace Field
{
    public class HpStatus : MonoBehaviour
    {
        private ReactiveProperty<int> _hp = new ReactiveProperty<int>(6);

        public ReactiveProperty<int> MyHP
        {
            get => _hp;
            set => value = _hp;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            _hp
                .Where(hp => hp > 6)
                .Subscribe(_ => _hp.Value = 6)
                .AddTo(this);

            _hp
                .Where(hp => hp < 0)
                .Subscribe(_ => _hp.Value = 0)
                .AddTo(this);
        }
    }
}
