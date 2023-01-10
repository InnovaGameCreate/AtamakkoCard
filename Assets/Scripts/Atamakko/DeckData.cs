using System.Collections.Generic;
using UnityEngine;

namespace Atamakko
{
    /// <summary>
    /// デッキ情報
    /// </summary>
    public class DeckData : MonoBehaviour
    {
        public List<int> MyDeck { get; set; } // デッキのカードID
        public List<int> DeckCards { get; set; } = new List<int>(); // 山札のカードID
        public List<int> HandCards { get; } = new List<int>(); // 手札のカードID
        public List<int> SettingCards { get; set; } = new List<int>(); // セットしたカードID
        public List<int> UsedCards { get; } = new List<int>(); // 使ったカードID
    }
}
