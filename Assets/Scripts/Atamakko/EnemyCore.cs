using System.Collections.Generic;
using Card;

namespace Atamakko
{
    /// <summary>
    /// 敵のアタマッコ
    /// </summary>
    public class EnemyCore : AtamakkoCore
    {
        private EasyCPU _enemy; // 敵の中身

        private void Start()
        {
            _enemy = gameObject.AddComponent<EasyCPU>();
        }

        /// <summary>
        /// 山札と手札のカードIDを取得する。
        /// </summary>
        /// <returns>山札と手札のカードID</returns>
        public override List<int> GetDeck()
        {
            var list = new List<int>(DeckData.DeckCards);
            list.AddRange(DeckData.HandCards);
            list.Sort();
            return list;
        }

        /// <summary>
        /// カードをセットする。
        /// </summary>
        /// <param name="list">セットしたカードID</param>
        public void SetSettingCards(List<int> list)
        {
            DeckData.SettingCards = new List<int>(list);
            foreach (var settingCard in DeckData.SettingCards)
            {
                DeckData.HandCards.Remove(settingCard);
            }
        }

        /// <summary>
        /// カードを選択する。（CPU）
        /// </summary>
        public void CardSelect()
        {
            DeckData.SettingCards = _enemy.SelectCardLogic(DeckData.HandCards);
        }

        /// <summary>
        /// 必殺技を選択する。
        /// </summary>
        public void UltimateSelect()
        {
            if (!UsedUltimate) AtamakkoData.UltimateState = _enemy.SelectUltimateLogic(AtamakkoData);
            if (AtamakkoData.UltimateState != UltimateState.Normal) UsedUltimate = true;
        }

        /// <summary>
        /// 攻撃する位置を選択する。
        /// </summary>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>攻撃する位置</returns>
        public int AttackSelect(int player, CardModel card)
        {
            return _enemy.SelectAttackLogic(AtamakkoData.MyPosition, player, card);
        }

        /// <summary>
        /// 移動する位置を選択する。
        /// </summary>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>移動する位置</returns>
        public int MoveSelect(int player, CardModel card)
        {
            return _enemy.SelectMoveLogic(AtamakkoData.MyPosition, player, card);
        }
    }
}
