using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "Create CardIcon", fileName = "CardIcon")]
    public class CardIcon : ScriptableObject
    {
        public List<Sprite> cardIconList; // カードアイコンのリスト
    }
}
