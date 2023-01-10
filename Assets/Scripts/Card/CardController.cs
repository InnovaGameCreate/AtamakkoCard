using UnityEngine;

namespace Card
{
    /// <summary>
    /// カードの実体
    /// </summary>
    public class CardController : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
    {
        private int CardID { get; set; } // カードID
        public CardView view; // カードの見た目
        public CardModel Model { get; private set; } // カードの内部情報

        //private UIExplanation _explanation; 

        private void Awake()
        {
            view = GetComponent<CardView>();
        }

        /// <summary>
        /// カードの初期化。
        /// </summary>
        /// <param name="id">カードID</param>
        public void Init(int id)
        {
            CardID = id;
            Model = new CardModel(CardData.CardDataArrayList[CardID]);
            view.Show(Model);
            //_explanation = GameObject.FindGameObjectWithTag("Explanation").GetComponent<UIExplanation>();
        }

        /*
        public void OnPointerEnter(PointerEventData eventData)
        {
            _explanation.SetUIActive(true);
            _explanation.CardID.Value = CardID;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _explanation.CardID.Value = -1;
            _explanation.SetUIActive(false);
        }
        */
    }
}
