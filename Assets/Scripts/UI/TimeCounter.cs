using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public class TimeCounter : MonoBehaviour
    {
        public IObservable<int> CountDownObservable => _countDownObservable.AsObservable();

        private IConnectableObservable<int> _countDownObservable;

        public void SetTimer(int countTime)
        {
            _countDownObservable = CreateCountDownObservable(countTime).Publish();
            _countDownObservable.Connect();
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
