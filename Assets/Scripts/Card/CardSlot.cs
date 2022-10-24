using UniRx;
using UnityEngine;

namespace Card
{
    public class CardSlot : MonoBehaviour
    {
        private int _cardID = -1;
        private CardController _cardController;
        
        [SerializeField] private GameObject cardPrefab;

        public int MyCardID
        {
            get => _cardID;
            set => _cardID = value;
        }

        void Start()
        {
            BattleManager.Instance.MyGameState
                .Where(s => s == BattleManager.State.Battle)
                .Subscribe(_ =>
                {
                    if (MyCardID < 0)
                    {
                        Destroy(gameObject);
                    }
                })
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
    }
}
