using System.Collections.Generic;
using Card;
using UnityEngine;

[CreateAssetMenu(menuName = "Creat Deck", fileName = "Deck")]
public class Deck : ScriptableObject
{
    public List<int> cardIDList;
}
