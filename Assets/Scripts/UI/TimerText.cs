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

            timeCounter.Timer
                .Subscribe(time =>
                {
                    Debug.Log(time);
                    _text.text = $"{time}";
                }, () =>
                {
                    _text.text = string.Empty;
                    _text.color = Color.white;
                })
                .AddTo(this);

            timeCounter.Timer
                .Where(timer => timer <= 5)
                .Subscribe(_ => _text.color = Color.red)
                .AddTo(this);
        }
    }
}
