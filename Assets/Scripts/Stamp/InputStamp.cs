using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Stamp
{
    public class InputStamp : MonoBehaviour
    {
        public enum StampID
        {
            Defeat,
            Good
        }
        
        [SerializeField] private Button button;
        private readonly Subject<byte> _stampClick = new Subject<byte>();

        public IObservable<byte> OnClickStamp => _stampClick;

        void Start()
        {
            button.OnClickAsObservable()
                .Subscribe(_ => _stampClick.OnNext(CheckImageID()))
                .AddTo(this);
        }

        private byte CheckImageID()
        {
            var sValue = (StampID)Enum.Parse(typeof(StampID), button.name);
            return (byte)sValue;
        }
    }
}

