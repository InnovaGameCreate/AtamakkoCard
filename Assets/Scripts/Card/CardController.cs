using UnityEngine;

namespace Card
{
    public class CardController : MonoBehaviour
    {
        public CardView view;
        public CardModel Model;

        private void Awake()
        {
            view = GetComponent<CardView>();
        }

        // Update is called once per frame
        public void Init(string[] datalist)
        {
            Model = new CardModel(datalist);
            view.Show(Model);
        }
    }
}
