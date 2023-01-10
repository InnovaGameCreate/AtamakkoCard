using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Stamp
{
    /// <summary>
    /// スタンプを押すクラス
    /// </summary>
    public class InputStamp : MonoBehaviour
    {
        [SerializeField] private byte buttonID; // ボタンID
        [SerializeField] private Button button; // ボタン
        
        // 押されたときのイベント
        private readonly Subject<byte> _stampClick = new Subject<byte>();
        public IObservable<byte> OnClickStamp => _stampClick;

        void Start()
        {
            // 押されたときボタンIDを送る
            button.OnClickAsObservable()
                .Subscribe(_ => _stampClick.OnNext(buttonID))
                .AddTo(this);
        }
    }
}

