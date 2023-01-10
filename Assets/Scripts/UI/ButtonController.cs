using System;
using System.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 汎用的なボタンクラス
    /// </summary>
    public class ButtonController : MonoBehaviour
    {
        // ボタンのON・OFFを切り替えるイベント
        private readonly ReactiveProperty<bool> _interactable = new ReactiveProperty<bool>(true);
        // ボタンを押した時のイベント
        private readonly Subject<bool> _pushed = new Subject<bool>();
        public IObservable<bool> Pushed => _pushed;
        private Button _button; // アタッチ先のボタン
        [SerializeField] private SeType se; // SE

        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }

        private void Start()
        {
            _button = gameObject.GetComponent<Button>();
            
            // ボタンを押した時にイベント発行
            _button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _pushed.OnNext(true);
                    SeManager.Instance.ShotSe(se);
                })
                .AddTo(this);
            
            // ボタンのON・OFF切り替え
            _interactable
                .Subscribe(b => _button.interactable = b)
                .AddTo(this);
        }
    }
}
