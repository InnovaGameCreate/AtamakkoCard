using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Atamakko
{
    public class TutorialCPU : EnemyLogic
    {
        private int _attackTimes, _moveTimes;
        
        /// <summary>
        /// セットするカードを選択する。
        /// </summary>
        /// <param name="handID">手札のカードID</param>
        /// <returns>セットするカードID</returns>
        public override List<int> SelectCardLogic(List<int> handID)
        {
            var cards = new List<int>();
            for (var i = 0; i < 3; i++)
            {
                cards.Add(handID[0]);
                handID.Remove(handID[0]);
            }
            return cards;
        }

        /// <summary>
        /// 必殺技を選択する。
        /// </summary>
        /// <param name="myDate">アタマッコの内部データ</param>
        /// <returns>使用する必殺技</returns>
        public override UltimateState SelectUltimateLogic(AtamakkoData myDate)
        {
            return UltimateState.Normal;
        }

        /// <summary>
        /// 攻撃する場所を選択する。
        /// </summary>
        /// <param name="enemy">自身の位置</param>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>攻撃する位置</returns>
        public override int SelectAttackLogic(int enemy, int player, CardModel card)
        {
            _attackTimes++;
            return _attackTimes switch
            {
                1 => 0,
                2 => 4,
                3 => 3,
                4 => 4,
                _ => -1
            };
        }

        /// <summary>
        /// 移動する場所を選択する。
        /// </summary>
        /// <param name="enemy">自分の位置</param>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>移動する位置</returns>
        public override int SelectMoveLogic(int enemy, int player, CardModel card)
        {
            _moveTimes++;
            return _moveTimes switch
            {
                1 => 4,
                2 => 0,
                _ => -1
            };
        }
    }
}
