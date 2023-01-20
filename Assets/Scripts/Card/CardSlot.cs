using Manager;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
    /// <summary>
    /// カードスロットクラス
    /// </summary>
    public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // 使用するスロットの処理
        public ReactiveProperty<bool> MySelect { get; set; } = new ReactiveProperty<bool>(false);
        private Image _selectImage;
        private static readonly Color SelectColor = new Color(255, 0, 0);

        [SerializeField] private GameObject cardPrefab; // カードのプレハブ
        public CardController MyCard { get; private set; } // スロット内のカード
        public int MyCardID { get; private set; } = -1; // 持っているカードID
        private Prediction _prediction; // 予測間合い
        
        void Start()
        {
            _selectImage = gameObject.GetComponent<Image>();
            _prediction = FindObjectOfType<Prediction>();
            // 使用するスロットの色を変える
            MySelect.Subscribe(b => { _selectImage.color = b ? SelectColor : Color.clear; }).AddTo(this);
            // 戦闘フェイズ時に空のスロットを消す
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

        /// <summary>
        /// カードを生成する。
        /// </summary>
        /// <param name="cardID">カードID</param>
        public void CreateCard(int cardID)
        {
            MyCardID = cardID;
            if (cardID >= 0)
            {
                var card = Instantiate(cardPrefab, transform);
                MyCard = card.GetComponent<CardController>();
                MyCard.Init(MyCardID);
            }
            else
            {
                DeleteCard();
            }
        }

        /// <summary>
        /// カードを消す。
        /// </summary>
        public void DeleteCard()
        {
            MyCardID = -1;
            foreach (Transform childObj in transform)
            {
                Destroy(childObj.gameObject);
            }
        }
        
        /// <summary>
        /// カードを裏返す。
        /// </summary>
        public void FlipOver()
        {
            MyCard.view.backCard.SetActive(!MyCard.view.backCard.activeSelf);
        }

        public bool IsVisible(string[] cardData)
        {
            return true;
        }

        /// <summary>
        /// カードをかざした時に間合いを表示する
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (MyCardID < 0 || MyCard == null) return;
            if (BattleManager.Instance.CurrentState.Value != GameState.Select) return;
            switch (MyCard.Model.Kind)
            {
                case "攻撃":
                {
                    for (int i = 0; i < MyCard.Model.Attack.Length; i++)
                    {
                        if (MyCard.Model.Attack[i] == "〇")
                        {
                            _prediction.Show(i, true);
                        }
                    }

                    break;
                }
                case "移動":
                {
                    for (int i = 0; i < MyCard.Model.Move.Length; i++)
                    {
                        if (MyCard.Model.Move[i] == "〇")
                        {
                            _prediction.Show(i, false);
                        }
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// かざしていないとき表示した間合いを非表示にする。
        /// </summary>
        /// <param name="eventData">マウスポインタ</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (MyCardID < 0 || MyCard == null) return;
            if (BattleManager.Instance.CurrentState.Value != GameState.Select) return;
            _prediction.Hide();
        }
    }
}
