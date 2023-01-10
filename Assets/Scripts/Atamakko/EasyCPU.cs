using System;
using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Atamakko
{
    /// <summary>
    /// CPU（弱い）の中身
    /// </summary>
    public class EasyCPU : MonoBehaviour
    {
        /// <summary>
        /// セットするカードを選択する。ランダムで行われる。
        /// </summary>
        /// <param name="handID">手札のカードID</param>
        /// <returns>セットするカードID</returns>
        public List<int> SelectCardLogic(List<int> handID)
        {
            var cards = new List<int>();
            System.Random random = new System.Random((int) DateTime.Now.Ticks);
            for (int i = 0; i < 3; i++)
            {
                int index = (int) (random.NextDouble() * (handID.Count - 1));
                cards.Add(handID[index]);
                handID.Remove(index);
            }
            return cards;
        }

        /// <summary>
        /// 必殺技を選択する。体力が３以下なら回復の必殺技を使う。
        /// </summary>
        /// <param name="myDate">アタマッコの内部データ</param>
        /// <returns>使用する必殺技</returns>
        public UltimateState SelectUltimateLogic(AtamakkoData myDate)
        {
            if (myDate.MyHp.Value <= 3)
            {
                return UltimateState.Recover;
            }

            return UltimateState.Normal;
        }

        /// <summary>
        /// 攻撃する場所を選択する。プレイヤーが攻撃可能な位置にいる場合、攻撃位置に決定する。
        /// </summary>
        /// <param name="enemy">自身の位置</param>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>攻撃する位置</returns>
        public int SelectAttackLogic(int enemy, int player, CardModel card)
        {
            int select = -1;
            var canSelect = new List<int>();
            System.Random random = new System.Random((int) DateTime.Now.Ticks);
            for (int i = 0; i < card.Attack.Length; i++)
            {
                if (card.Attack[i] == "〇")
                {
                    if (select < 0)
                    {
                        select = player == (i + enemy) % 6 ? player : -1;
                    }
                    canSelect.Add(i);
                }
            }
            if (select < 0)
            {
                select = (int) (random.NextDouble() * (canSelect.Count - 1));
            }

            return select;
        }

        /// <summary>
        /// 移動する場所を選択する。ランダムで決定する。
        /// </summary>
        /// <param name="enemy">自分の位置</param>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>移動する位置</returns>
        public int SelectMoveLogic(int enemy, int player, CardModel card)
        {
            var canSelect = new List<int>();
            System.Random random = new System.Random((int) DateTime.Now.Ticks);
            for (int i = 0; i < card.Move.Length; i++)
            {
                if (card.Move[i] == "〇")
                {
                    canSelect.Add(i);
                }
            }
            
            return (enemy + canSelect[(int) (random.NextDouble() * (canSelect.Count - 1))]) % 6;
        }
    }
}
