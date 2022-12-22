using UnityEngine;

namespace Assemble
{
    public class equipmentModel : MonoBehaviour
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string Position;
        public readonly int Rarity;
        public readonly int[] card;

        public equipmentModel(string[] dataList)
        {
            ID = int.Parse(dataList[0]);
            Name = dataList[1];
            Position = dataList[2];
            Rarity = int.Parse(dataList[3]);
            Debug.Log(dataList[4] +"ÅF"+ dataList[5]);
            card = new[] { int.Parse(dataList[4]), int.Parse(dataList[5]) };
        }
    }
}