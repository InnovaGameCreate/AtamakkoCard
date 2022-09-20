using UniRx;
using UnityEngine;

namespace Player
{
    public class AtamakkoStatus : MonoBehaviour
    {
        public enum Ultimate
        {
            Normal,
            Recover,
            Attack,
            Speed
        }
        private Ultimate _ultState = Ultimate.Normal;
        public Ultimate UState
        {
            get => _ultState;
            set => _ultState = value;
        }

        private ReactiveProperty<int> _hp = new ReactiveProperty<int>(6);
        public ReactiveProperty<int> MyHp
        {
            get => _hp;
            set => _hp = value;
        }
        
        [SerializeField] private int position;

        public int MyPosition
        {
            get => position;
            set => position = value;
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
