using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardSlot : MonoBehaviour
    {
        private ReactiveProperty<bool> _select = new ReactiveProperty<bool>(false);

        public ReactiveProperty<bool> MySelect
        {
            get => _select;
            set => _select = value;
        }

        private int _cardID = -1;
        private CardController _cardController;
        public CardController MyCard => _cardController;

        private Image _selectImage;
        private static readonly Color SelectColor = new Color(255, 0, 0);

        [SerializeField] private GameObject cardPrefab;

        public int MyCardID
        {
            get => _cardID;
            private set => _cardID = value;
        }

        void Start()
        {
            _selectImage = gameObject.GetComponent<Image>();
            
            CPUManager.Instance.CurrentState
                .Where(s => s == GameState.Battle)
                .Subscribe(_ =>
                {
                    if (MyCardID < 0)
                    {
                        Destroy(gameObject);
                    }
                })
                .AddTo(this);

            _select
                .Where(b => b)
                .Subscribe(_ => _selectImage.color = SelectColor)
                .AddTo(this);

            _select
                .Where((b => b == false))
                .Subscribe(_ => _selectImage.color = Color.clear)
                .AddTo(this);
        }

        public void CreateCard(int cardID)
        {
            MyCardID = cardID;
            if (cardID >= 0)
            {
                var card = Instantiate(cardPrefab, transform);
                _cardController = card.GetComponent<CardController>();
                _cardController.Init(CardData.CardDataArrayList[_cardID]);
            }
            else
            {
                DeleteCard();
            }
        }

        public void DeleteCard()
        {
            _cardID = -1;
            foreach (Transform childObj in transform)
            {
                Destroy(childObj.gameObject);
            }
        }

        public void FlipOver()
        {
            _cardController.view.backCard.SetActive(!_cardController.view.backCard.activeSelf);
        }

        public bool IsVisualable(string[] cardData)
        {
            return true;
        }
    }
}
