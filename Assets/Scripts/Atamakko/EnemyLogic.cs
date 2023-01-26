using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Atamakko
{
    public class EnemyLogic : MonoBehaviour
    {
        /// <summary>
        /// セットするカードを選択する。
        /// </summary>
        /// <param name="handID">手札のカードID</param>
        /// <returns>セットするカードID</returns>
        public virtual List<int> SelectCardLogic(List<int> handID)
        {
            return handID;
        }

        /// <summary>
        /// 必殺技を選択する。
        /// </summary>
        /// <param name="myDate">アタマッコの内部データ</param>
        /// <returns>使用する必殺技</returns>
        public virtual UltimateState SelectUltimateLogic(AtamakkoData myDate)
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
        public virtual int SelectAttackLogic(int enemy, int player, CardModel card)
        {
            return -1;
        }

        /// <summary>
        /// 移動する場所を選択する。
        /// </summary>
        /// <param name="enemy">自分の位置</param>
        /// <param name="player">プレイヤーの位置</param>
        /// <param name="card">使用するカード</param>
        /// <returns>移動する位置</returns>
        public virtual int SelectMoveLogic(int enemy, int player, CardModel card)
        {
            return -1;
        }
    }
}
