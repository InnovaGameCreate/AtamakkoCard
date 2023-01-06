using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Player
{
    public class DeckData : MonoBehaviour
    {
        public List<int> MyDeck { get; set; }

        public List<int> DeckCards { get; set; } = new List<int>();
        public List<int> HandCards { get; set; } = new List<int>();
        public List<int> SettingCards { get; set; } = new List<int>();
        public List<int> UsedCards { get; set; } = new List<int>();
    }
}
