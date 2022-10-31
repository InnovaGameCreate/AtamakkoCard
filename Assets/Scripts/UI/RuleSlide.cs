using UniRx;
using UnityEngine;

namespace UI
{
    public class RuleSlide : MonoBehaviour
    {
        [SerializeField] private ButtonController leftSlide;
        [SerializeField] private ButtonController rightSlide;

        [SerializeField] private GameObject[] slideRule;
        private readonly ReactiveProperty<int> _numSlide = new ReactiveProperty<int>(0);

        [SerializeField] private ButtonController ruleButton;
        [SerializeField] private ButtonController closeButton;

        private void Start()
        {
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
            
            leftSlide.Pushed
                .Subscribe(_ => _numSlide.Value--)
                .AddTo(this);

            rightSlide.Pushed
                .Subscribe(_ => _numSlide.Value++)
                .AddTo(this);

        }
    }
}
