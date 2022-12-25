using Card;
using UniRx;
using UnityEngine;

namespace UI
{
    public class UIExplanation : MonoBehaviour
    {
        public ReactiveProperty<int> CardID { get; set; } = new ReactiveProperty<int>(-1);

        private CardView _cardView;

        private void Start()
        {
            _cardView = GetComponent<CardView>();

            CardID
                .Subscribe(i =>
                {
                    if (i >= 0)
                    {
                        var model = new CardModel(CardData.CardDataArrayList[i]);
                        _cardView.Show(model);
                    }
                })
                .AddTo(this);
        }

        public void SetUIActive(bool b)
        {
            gameObject.SetActive(b);
        }
    }
}
