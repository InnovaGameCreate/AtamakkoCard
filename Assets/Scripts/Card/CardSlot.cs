using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardSlot : MonoBehaviour
    {

        public ReactiveProperty<bool> MySelect { get; set; } = new ReactiveProperty<bool>(false);

        private CardController _cardController;
        public CardController MyCard => _cardController;

        private Image _selectImage;
        private static readonly Color SelectColor = new Color(255, 0, 0);

        [SerializeField] private GameObject cardPrefab;

        public int MyCardID { get; private set; } = -1;

        void Start()
        {
            _selectImage = gameObject.GetComponent<Image>();
            MySelect.Subscribe(b => { _selectImage.color = b ? SelectColor : Color.clear; }).AddTo(this);
            if (PlayerConfig.IsOnline)
            {
                OnlineManager.Instance.CurrentState
                    .Where(s => s == GameState.Battle)
                    .Subscribe(_ =>
                    {
                        if (MyCardID < 0)
                        {
                            Destroy(gameObject);
                        }
                    })
                    .AddTo(this);
            }
            else
            {
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
            }
            
        }

        public void CreateCard(int cardID)
        {
            MyCardID = cardID;
            if (cardID >= 0)
            {
                var card = Instantiate(cardPrefab, transform);
                _cardController = card.GetComponent<CardController>();
                _cardController.Init(MyCardID);
            }
            else
            {
                DeleteCard();
            }
        }

        public void DeleteCard()
        {
            MyCardID = -1;
            foreach (Transform childObj in transform)
            {
                Destroy(childObj.gameObject);
            }
        }

        public void FlipOver()
        {
            _cardController.view.backCard.SetActive(!_cardController.view.backCard.activeSelf);
        }

        public bool IsVisible(string[] cardData)
        {
            return true;
        }
    }
}
