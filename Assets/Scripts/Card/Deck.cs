using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "Creat Deck", fileName = "Deck")]
    public class Deck : ScriptableObject
    {
        public List<int> cardIDList; // デッキに入っているカードID
    }
}
