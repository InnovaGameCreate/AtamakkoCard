namespace Card
{
    public class CardModel

    {
        public int ID;
        public string Name;
        public int Initiative;
        public int Damage;
        public string Kind;
        public int Rarity;
        public string[] Move;
        public string[] Attack;
        public string Explanation;
        
        public CardModel(string[] dataList) {
            ID = int.Parse(dataList[0]);
            Name = dataList[1];
            Initiative = int.Parse(dataList[2]);
            Damage = int.Parse(dataList[3]);
            Kind = dataList[4];
            Rarity = int.Parse(dataList[5]);
            Move = new []{dataList[6], dataList[7], dataList[8], dataList[9], dataList[10], dataList[11]};
            Attack = new []{dataList[12], dataList[13], dataList[14], dataList[15], dataList[16], dataList[17]};
            Explanation = dataList[18];
        }
    }
}
