using UniRx;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// ルール表示を管理するクラス
    /// </summary>
    public class RuleSlide : MonoBehaviour
    {
        [SerializeField] private ButtonController leftSlide; // 左へスライドするボタン
        [SerializeField] private ButtonController rightSlide; // 右へスライドするボタン

        [SerializeField] private GameObject[] slideRule; // ルールスライド
        private readonly ReactiveProperty<int> _numSlide = new ReactiveProperty<int>(0); // スライド番号

        [SerializeField] private ButtonController ruleButton; // ルールボタン
        [SerializeField] private ButtonController closeButton; // 閉じるボタン

        private void Start()
        {
            // スライド番号に合わせてスライドを表示＆ボタンのON・OFFを切り替える
            _numSlide
                .Subscribe(num =>
                {
                    foreach (var obj in slideRule)
                    {
                        obj.SetActive(false);
                    }
                    slideRule[num].SetActive(true);
                    if (num <= 0)
                    {
                        leftSlide.MyInteractable = false;
                        rightSlide.MyInteractable = true;
                    }
                    else if (num >= slideRule.Length - 1)
                    {
                        leftSlide.MyInteractable = true;
                        rightSlide.MyInteractable = false;
                    }
                    else
                    {
                        leftSlide.MyInteractable = true; 
                        rightSlide.MyInteractable = true;
                    }
                })
                .AddTo(this);
            
            // ルールボタンを押したときにルールスライドを表示する
            ruleButton.Pushed
                .Subscribe(_ =>
                {
                    var t = gameObject.transform;
                    for (int i = 0; i < t.childCount; i++)
                    {
                        t.GetChild(i).gameObject.SetActive(true);
                    }
                    _numSlide.Value = 0;
                })
                .AddTo(this);
            
            // 閉じるボタンを押したときにルールスライドを非表示にする
            closeButton.Pushed
                .Subscribe(_ =>
                {
                    var t = gameObject.transform;
                    for (int i = 0; i < t.childCount; i++)
                    {
                        t.GetChild(i).gameObject.SetActive(false);
                    }
                    _numSlide.Value = 0;
                })
                .AddTo(this);
            
            // 左ボタンを押したときにスライドを左に送る
            leftSlide.Pushed
                .Subscribe(_ => _numSlide.Value--)
                .AddTo(this);

            // 右ボタンを押したときにスライドを右に送る
            rightSlide.Pushed
                .Subscribe(_ => _numSlide.Value++)
                .AddTo(this);
        }
    }
}
