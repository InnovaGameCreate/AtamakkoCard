using System.Collections.Generic;
using Manager;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
    public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public ReactiveProperty<bool> MySelect { get; set; } = new ReactiveProperty<bool>(false);

        private CardController _cardController;
        public CardController MyCard => _cardController;

        private Image _selectImage;
        private static readonly Color SelectColor = new Color(255, 0, 0);

        [SerializeField] private GameObject cardPrefab;
        private PredictionManager _predictionManager;
        private List<GameObject> _areaObjects;

        public int MyCardID { get; private set; } = -1;

        void Start()
        {
            _selectImage = gameObject.GetComponent<Image>();
            MySelect.Subscribe(b => { _selectImage.color = b ? SelectColor : Color.clear; }).AddTo(this);
            _predictionManager = FindObjectOfType<PredictionManager>();
            BattleManager.Instance.CurrentState
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (MyCardID < 0 || _cardController == null) return;
            if (BattleManager.Instance.CurrentState.Value != GameState.Select) return;
            switch (_cardController.Model.Kind)
            {
                case "攻撃":
                {
                    for (int i = 0; i < _cardController.Model.Attack.Length; i++)
                    {
                        if (_cardController.Model.Attack[i] == "〇")
                        {
                            _predictionManager.Show(i, true);
                        }
                    }

                    break;
                }
                case "移動":
                {
                    for (int i = 0; i < _cardController.Model.Move.Length; i++)
                    {
                        if (_cardController.Model.Move[i] == "〇")
                        {
                            _predictionManager.Show(i, false);
                        }
                    }

                    break;
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (MyCardID < 0 || _cardController == null) return;
            if (BattleManager.Instance.CurrentState.Value != GameState.Select) return;
            _predictionManager.Hide();
        }
    }
}
