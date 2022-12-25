using System.Collections.Generic;
using Card;
using UniRx;
using UnityEngine;

namespace UI
{
    public class UiDeck : MonoBehaviour
    {
        private ReactiveProperty<List<int>> _indicate = new ReactiveProperty<List<int>>(null);

        [SerializeField] private GameObject cardPrefab;
        private void Start()
        {
            _indicate
                .Subscribe(list =>
                {
                    foreach (Transform childObj in transform)
                    {
                        Destroy(childObj.gameObject);
                    }

                    foreach (var cardID in list)
                    {
                        var card = Instantiate(cardPrefab, transform);
                        card.GetComponent<CardController>().Init(cardID);
                    }
                })
                .AddTo(this);
        }

        public void SetIndicate(List<int> list)
        {
            var sortList = new List<int>(list);
            sortList.Sort();
            _indicate = new ReactiveProperty<List<int>>(sortList);
        }
    }
}
