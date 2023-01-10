using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// タイマーの表示クラス
    /// </summary>
    public class TimerText : MonoBehaviour
    {
        [SerializeField] private TimeCounter timeCounter; // タイマーカウンター
        private TextMeshProUGUI _text; // 時間を表示するテキスト

        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = String.Empty;

            timeCounter.Timer.Subscribe(ShowTimer).AddTo(this); // 時間を表示する

            // タイマーが終了したらテキストを空にする
            timeCounter.CountNow
                .Where(b => !b)
                .Subscribe(_ =>
                {
                    _text.text = string.Empty;
                })
                .AddTo(this);
        }

        /// <summary>
        /// 時間を表示する。
        /// </summary>
        /// <param name="time">時間</param>
        private void ShowTimer(int time)
        {
            _text.text = $"{time}";
            _text.color = time > 5 ? Color.white : Color.red;
        }
    }
}