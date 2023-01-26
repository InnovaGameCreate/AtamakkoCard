using System;
using System.Audio;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 攻撃ボタンクラス
    /// </summary>
    public class AttackButton : MonoBehaviour
    {
        public int AttackPlace { set; get; } // 表示する位置
        // 押されたときのイベント
        private readonly Subject<int> _selected = new Subject<int>();
        public IObservable<int> Selected => _selected;
        private Button _myButton; // アタッチ先のボタン
        [SerializeField] private SeType se; // SE

        void Start()
        {
            _myButton = transform.GetChild(0).gameObject.GetComponent<Button>();

            // クリックされたときにイベント発行
            _myButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _selected.OnNext(AttackPlace);
                    SeManager.Instance.ShotSe(se);
                })
                .AddTo(this);

            // タイマーが0になったら消す
            BattleManager.Instance.Next
                .Where(b => b)
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
