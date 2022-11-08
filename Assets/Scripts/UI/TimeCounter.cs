using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public class TimeCounter : MonoBehaviour
    {
        public IObservable<int> CountDownObservable => _countDownObservable.AsObservable();

        private IConnectableObservable<int> _countDownObservable;

        private int _countTime;

        public void SetTimer(int countTime)
        {
            _countTime = countTime;
            _countDownObservable = CreateCountDownObservable(_countTime).Publish();
            _countDownObservable.Connect();
        }

        public void EndTimer()
        {
            _countTime = 0;
        }

        private IObservable<int> CreateCountDownObservable(int countTime)
        {
            return Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(x => (int) (countTime - x))
                .TakeWhile(x => x > 0);
        }
    }
}
