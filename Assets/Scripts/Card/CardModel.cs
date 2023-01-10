
namespace Card
{
    /// <summary>
    /// カードの内部情報のクラス
    /// </summary>
    public class CardModel
    {
        public readonly int ID; // カードID
        public readonly string Name; // カード名
        public readonly int Initiative; // 先制度
        public readonly int Damage; // ダメージ
        public readonly string Kind; // 種類（攻撃，移動）
        public readonly string Additional; // 追加効果の有無
        public readonly string Effect; // エフェクト（斬撃，射撃）
        public int Rarity; // レアリティ
        public readonly string[] Move; // 移動の間合い
        public readonly string[] Attack; // 攻撃の間合い
        public readonly string Explanation; // 説明文
        
        /// <summary>
        /// カード情報の初期化
        /// </summary>
        /// <param name="dataList">カード情報</param>
        public CardModel(string[] dataList) {
            ID = int.Parse(dataList[0]);
            Name = dataList[1];
            Initiative = int.Parse(dataList[2]);
            Damage = int.Parse(dataList[3]);
            Kind = dataList[4];
            Additional = dataList[5];
            Effect = dataList[6];
            Rarity = int.Parse(dataList[7]);
            Move = new []{dataList[8], dataList[9], dataList[10], dataList[11], dataList[12], dataList[13]};
            Attack = new []{dataList[14], dataList[15], dataList[16], dataList[17], dataList[18], dataList[19]};
            Explanation = dataList[20];
        }
    }
}
