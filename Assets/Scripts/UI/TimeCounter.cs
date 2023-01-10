using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// 時間をカウントするクラス
    /// </summary>
    public class TimeCounter : MonoBehaviour
    {
        private int _countTime; // カウントする時間
        private int _nowTime; // 今の時間
        
        // カウントするのイベント
        private readonly Subject<bool> _countNow = new Subject<bool>();
        public IObservable<bool> CountNow => _countNow;

        // 時間を送るイベント
        private readonly Subject<int> _timer = new Subject<int>();
        public IObservable<int> Timer => _timer;

        public static TimeCounter Instance; // インスタンス化

        private void Awake()
        {
            // シングルトン化
            if (Instance == null)
            {
                Instance = this;
                _timer.RepeatUntilDestroy(gameObject)
                    .Subscribe()
                    .AddTo(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// タイマーを０にする。
        /// </summary>
        public void EndTimer()
        {
            _nowTime = _countTime;
        }
        
        /// <summary>
        /// カウントダウンを始める。
        /// </summary>
        /// <param name="countTime">制限時間</param>
        /// <returns>コルーチン</returns>
        public IEnumerator CountDown(int countTime)
        {
            _nowTime = 0;
            _countTime = countTime;
            _countNow.OnNext(true);
            while (_countTime >= _nowTime)
            {
                var time = _countTime - _nowTime;
                _nowTime++;
                _timer.OnNext(time);

                yield return new WaitForSecondsRealtime(1.0f);
            }
            _countNow.OnNext(false);
        }
    }
}
