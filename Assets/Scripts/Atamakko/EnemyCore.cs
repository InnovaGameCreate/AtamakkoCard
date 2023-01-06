using System;
using System.Collections.Generic;
using Atamakko;
using Card;

namespace Player
{
    public class EnemyCore : AtamakkoCore
    {
        private EasyCPU _enemy;

        private void Start()
        {
            _enemy = gameObject.AddComponent<EasyCPU>();
        }

        public override List<int> GetDeck()
        {
            var list = new List<int>(DeckData.DeckCards);
            if (list == null) throw new ArgumentNullException(nameof(list));
            list.AddRange(DeckData.HandCards);
            list.Sort();
            return list;
        }

        public void SetSettingCards(List<int> list)
        {
            DeckData.SettingCards = new List<int>(list);
            foreach (var settingCard in DeckData.SettingCards)
            {
                DeckData.HandCards.Remove(settingCard);
            }
        }

        public void CardSelect()
        {
            DeckData.SettingCards = _enemy.SelectCardLogic(DeckData.HandCards);
        }

        public void UltimateSelect()
        {
            if (!UsedUltimate)
            {
                AtamakkoData.UltimateState = _enemy.SelectUltimateLogic(AtamakkoData);
            }
        }

        public int AttackSelect(int player, CardModel card)
        {
            return _enemy.SelectAttackLogic(AtamakkoData.MyPosition, player, card);
        }

        public int MoveSelect(int player, CardModel card)
        {
            return _enemy.SelectMoveLogic(AtamakkoData.MyPosition, player, card);
        }
    }
}
