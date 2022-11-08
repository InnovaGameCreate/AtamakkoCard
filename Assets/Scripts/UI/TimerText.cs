using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimerText : MonoBehaviour
    {
        [SerializeField] private TimeCounter timeCounter;
        private Text _text;
        void Start()
        {
            _text = GetComponent<Text>();
            _text.text = String.Empty;

            timeCounter
                .CountDownObservable
                .Subscribe(time =>
                {
                    _text.text = $"{time}";
                }, () =>
                {
                    _text.text = string.Empty;
                });

            timeCounter
                .CountDownObservable
                .First(timer => timer <= 5)
                .Subscribe(_ => _text.color = Color.red);
        }
    }
}
