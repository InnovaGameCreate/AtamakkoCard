using UnityEngine;

namespace Card
{
    public class CardModel

    {
        public readonly int ID;
        public readonly string Name;
        public readonly int Initiative;
        public readonly int Damage;
        public readonly string Kind;
        public readonly string Additional;
        public readonly string Effect;
        public int Rarity;
        public readonly string[] Move;
        public readonly string[] Attack;
        public readonly string Explanation;
        
        public CardModel(string[] dataList) {
            Debug.Log(dataList[0]);
            ID = int.Parse(dataList[0]);
            Name = dataList[1];
            Initiative = int.Parse(dataList[2]);
            Damage = int.Parse(dataList[3]);
            Kind = dataList[4];
            Additional = dataList[5];
            Effect = dataList[6];
            Debug.Log(dataList[7]);
            //Rarity = int.Parse(dataList[7]);
            Move = new []{dataList[8], dataList[9], dataList[10], dataList[11], dataList[12], dataList[13]};
            Attack = new []{dataList[14], dataList[15], dataList[16], dataList[17], dataList[18], dataList[19]};
            Explanation = dataList[20];
        }
    }
}
