using System;
using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Player
{
    public class EasyCPU : MonoBehaviour
    {

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

        public UltimateState SelectUltimateLogic(AtamakkoData myDate)
        {
            if (myDate.MyHp.Value <= 3)
            {
                return UltimateState.Recover;
            }

            return UltimateState.Normal;
        }

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
