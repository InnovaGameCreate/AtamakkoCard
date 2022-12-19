using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace UI
{
    public class TimeCounter : MonoBehaviour
    {
        private int _countTime;
        private int _nowTime;
        
        private readonly Subject<bool> _countNow = new Subject<bool>();
        public IObservable<bool> CountNow => _countNow;

        private readonly Subject<int> _timer = new Subject<int>();
        public IObservable<int> Timer => _timer;

        public static TimeCounter Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void SetTimer(int countTime)
        {
            _countTime = countTime;
            //StartCoroutine(CountDown());
        }

        public void EndTimer()
        {
            _nowTime = _countTime;
        }

        public IEnumerator CountDown(int countTime)
        {
            _nowTime = 0;
            _countTime = countTime;
            _countNow.OnNext(true);
            while (_countTime > _nowTime)
            {
                var time = _countTime - _nowTime;
                _nowTime++;
                _timer.OnNext(time);

                yield return new WaitForSecondsRealtime(1.0f);
            }
            //_timer.OnCompleted();
            _countNow.OnNext(false);
        }
    }
}
