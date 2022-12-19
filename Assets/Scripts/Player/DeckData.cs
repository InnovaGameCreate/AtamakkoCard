using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Player
{
    public class DeckData : MonoBehaviour
    {
        public Deck MyDeck { get; set; }

        public List<int> DeckCards { get; set; }
        public List<int> HandCards { get; set; }
        public List<int> SettingCards { get; set; }
        public List<int> UsedCards { get; set; }
    }
}
