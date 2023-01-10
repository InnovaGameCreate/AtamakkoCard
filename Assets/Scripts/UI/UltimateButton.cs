using System.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 必殺技を選択するボタンクラス
    /// </summary>
    public class UltimateButton : MonoBehaviour
    {
        // ボタンのON・OFFを切り替えるイベント
        private readonly ReactiveProperty<bool> _interactable = new ReactiveProperty<bool>(false);
        
        [SerializeField] private GameObject uiUltimate; // 必殺技を選択するUI
        private Button _bUltimate; // アタッチ先のボタン
        [SerializeField] private SeType se; // SE

        public bool MyInteractable
        {
            set => _interactable.Value = value;
        }
    

        private void Start()
        {
            _bUltimate = gameObject.GetComponent<Button>();
            
            // ボタンを押した時にイベント発行
            _bUltimate.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    uiUltimate.SetActive(true);
                    SeManager.Instance.ShotSe(se);
                })
                .AddTo(this);
            
            // ボタンのON・OFF切り替え
            _interactable
                .Subscribe(b => _bUltimate.interactable = b)
                .AddTo(this);
        }
    }
}
