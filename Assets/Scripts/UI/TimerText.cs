using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public class TimerText : MonoBehaviour
    {
        [SerializeField] private TimeCounter timeCounter;
        private TextMeshProUGUI _text;

        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = String.Empty;

            timeCounter.Timer.Subscribe(ShowTimer).AddTo(this);

            timeCounter.CountNow
                .Where(b => !b)
                .Subscribe(_ =>
                {
                    _text.text = string.Empty;
                })
                .AddTo(this);
        }

        private void ShowTimer(int time)
        {
            _text.text = $"{time}";
            _text.color = time > 5 ? Color.white : Color.red;
        }
    }
}