using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enmyDeckModel : MonoBehaviour
{
    public readonly int ID;
    public readonly string characterName;
    public readonly string deckName;
    public readonly int Rarity;
    public readonly int[] card;
    public readonly int decBackImage;

    public enmyDeckModel(string[] dataList)
    {
        ID = int.Parse(dataList[0]);
        characterName = dataList[1];
        deckName = dataList[2];
        card = new[] {  int.Parse(dataList[3]), int.Parse(dataList[4]),
                        int.Parse(dataList[5]), int.Parse(dataList[6]),
                        int.Parse(dataList[7]), int.Parse(dataList[8]),
                        int.Parse(dataList[9]), int.Parse(dataList[10]),
                        int.Parse(dataList[11]), int.Parse(dataList[12]),
                        int.Parse(dataList[13]), int.Parse(dataList[14]) };
        decBackImage = int.Parse(dataList[15]);
    }
}
