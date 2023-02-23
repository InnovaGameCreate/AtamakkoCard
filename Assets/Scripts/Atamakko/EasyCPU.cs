using System;
using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Atamakko
{
    /// <summary>
    /// CPU（弱い）の中身
    /// </summary>
    public class EasyCPU : EnemyLogic
    {
        /// <summary>
        /// セットするカードを選択する。ランダムで行われる。
        /// </summary>
        /// <param name="handID">手札のカードID</param>
        /// <returns>セットするカードID</returns>
        public override List<int> SelectCardLogic(List<int> handID)
        {
            var cards = new List<int>();
            var random = new System.Random((int) DateTime.Now.Ticks);
            for (var i = 0; i < 3; i++)
            {
                var index = (int) (random.NextDouble() * (handID.Count - 1));
                cards.Add(handID[index]);
                handID.Remove(handID[index]);
            }
            return cards;
        }

        /// <summary>
        /// 必殺技を選択する。体力が３以下なら回復の必殺技を使う。
        /// </summary>
        /// <param name="myDate">アタマッコの内部データ</param>
        /// <returns>使用する必殺技</returns>
        public override UltimateState SelectUltimateLogic(AtamakkoData myDate)
        {
            var random = new System.Random((int) DateTime.Now.Ticks);
            var dRan = random.NextDouble();
            switch (dRan)
            {
                case <= 0.2:
                    return UltimateState.Attack;
                case <= 0.3:
                    return UltimateState.Speed;
                case <= 0.6 when myDate.MyHp.Value == 4:
                case <= 0.8 when myDate.MyHp.Value <= 3:
                    return UltimateState.Recover;
                default:
                    return UltimateState.Normal;
            }
        }

        /// <summary>
        /// 攻撃する場所を選択する。プレイヤーが攻撃可能な位置にいる場合、攻撃位置に決定する。
        /// </summary>
        /// <param name="enemy">自身の位置</param>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>攻撃する位置</returns>
        public override int SelectAttackLogic(int enemy, int player, CardModel card)
        {
            var select = -1;
            var canSelect = new List<int>();
            var random = new System.Random((int) DateTime.Now.Ticks);
            for (var i = 0; i < card.Attack.Length; i++)
            {
                if (card.Attack[i] == "〇")
                {
                    var pos = (i + enemy) % 6;
                    if (select < 0)
                    {
                        select = player == pos ? player : -1;
                    }
                    canSelect.Add(pos);
                }
            }
            if (select < 0)
            {
                select = canSelect[(int) (random.NextDouble() * canSelect.Count)];
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
        public override int SelectMoveLogic(int enemy, int player, CardModel card)
        {
            var canSelect = new List<int>();
            var random = new System.Random((int) DateTime.Now.Ticks);
            for (var i = 0; i < card.Move.Length; i++)
            {
                if (card.Move[i] == "〇")
                {
                    canSelect.Add(i);
                }
            }
            
            return (enemy + canSelect[(int) (random.NextDouble() * canSelect.Count)]) % 6;
        }
    }
}
