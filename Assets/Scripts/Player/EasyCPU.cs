using System;
using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Player
{
    public class EasyCPU : MonoBehaviour
    {
        public List<int> SelectCard(List<int> handID)
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

        public int SelectPlace(int enemy, int player, CardModel card)
        {
            int select = -1;
            var canSelect = new List<int>();
            System.Random random = new System.Random((int) DateTime.Now.Ticks);
            if (card.Kind == "攻撃")
            {
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
            }
            else if (card.Kind == "移動")
            {
                for (int i = 0; i < card.Move.Length; i++)
                {
                    if (card.Move[i] == "〇")
                    {
                        canSelect.Add(i);
                    }
                }
            }

            if (select < 0)
            {
                select = (int) (random.NextDouble() * (canSelect.Count - 1));
            }
            
            return select;
        }
    }
}
