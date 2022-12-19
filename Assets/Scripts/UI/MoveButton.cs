using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MoveButton : MonoBehaviour
    {
        public int MovePlace { set; get; }
        private readonly Subject<int> _selected = new Subject<int>();
        public IObservable<int> Selected => _selected;

        private Button _myButton;

        void Start()
        {
            _myButton = gameObject.GetComponent<Button>();

            _myButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _selected.OnNext(MovePlace);
                })
                .AddTo(this);

            TimeCounter.Instance.CountNow
                .Where(b => !b)
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
