using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Atamakko
{
    /// <summary>
    /// プレイヤーのアタマッコ
    /// </summary>
    public class PlayerCore : AtamakkoCore
    {
        /// <summary>
        /// 山札のカードIDを取得する。
        /// </summary>
        /// <returns>山札のカードID</returns>
        public override List<int> GetDeck()
        {
            var list = new List<int>(DeckData.DeckCards);
            list.Sort();
            return list;
        }

        public void RemoveHand(int cardID)
        {
            DeckData.HandCards.Remove(cardID);
        }

        /// <summary>
        /// セットしたカードIDを取得する。
        /// </summary>
        /// <returns>セットしたカードID</returns>
        public List<int> GetSettingCards()
        {
            return DeckData.SettingCards;
        }

        /// <summary>
        /// カードIDをセットする。
        /// </summary>
        /// <param name="cardID">セットするカードID</param>
        public void SetSettingCard(int cardID)
        {
            DeckData.SettingCards.Add(cardID);
            int num = DeckData.HandCards.IndexOf(cardID);
            if (num >= 0)
            {
                DeckData.HandCards.Remove(num);
            }
            else
            {
                Debug.Log("手持ちからカードが見つかりません");
            }
        }

        /// <summary>
        /// 攻撃可能な位置を取得する。
        /// </summary>
        /// <param name="card">使用するカード</param>
        /// <returns>攻撃可能な位置リスト</returns>
        public List<int> CanAttack(CardModel card)
        {
            var canAttack = new List<int>();
            for(int i = 0; i < card.Attack.Length; i++)
            {
                if (card.Attack[i] == "〇")
                {
                    var toPosition = (i + AtamakkoData.MyPosition) % 6;
                    canAttack.Add(toPosition);
                }
            }

            return canAttack;
        }

        /// <summary>
        /// 移動可能な位置を取得する
        /// </summary>
        /// <param name="card">使用するカード</param>
        /// <returns>移動可能な位置リスト</returns>
        public List<int> CanMove(CardModel card)
        {
            var canMove = new List<int>();
            for (int i = 0; i < card.Move.Length; i++)
            {
                if (card.Move[i] == "〇")
                {
                    var toPosition = (i + AtamakkoData.MyPosition) % 6;
                    canMove.Add(toPosition);
                }
            }
            
            return canMove;
        }
    }
}
