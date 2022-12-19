using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Player
{
    public class PlayerCore : AtamakkoCore
    {
        public void SetSettingCard(int slotNum, int cardID)
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

        public List<int> CanMove(CardModel card)
        {
            var canMove = new List<int>();
            for (int i = 0; i < card.Move.Length; i++)
            {
                if (card.Move[i] == "〇")
                {
                    var toPosition = (i + AtamakkoData.MyPosition) % 6;
                    canMove.Add(toPosition);
                    Debug.Log("CanMove：" + i);
                }
            }
            
            return canMove;
        }
    }
}
