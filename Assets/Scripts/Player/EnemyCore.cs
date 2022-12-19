using Card;

namespace Player
{
    public class EnemyCore : AtamakkoCore
    {
        public EasyCPU enemy;

        private void Start()
        {
            enemy = gameObject.AddComponent<EasyCPU>();
        }

        public void CardSelect()
        {
            DeckData.SettingCards = enemy.SelectCardLogic(DeckData.HandCards);
        }

        public void UltimateSelect()
        {
            if (!UsedUltimate)
            {
                AtamakkoData.UltimateState = enemy.SelectUltimateLogic(AtamakkoData);
            }
        }

        public int AttackSelect(int player, CardModel card)
        {
            return enemy.SelectAttackLogic(AtamakkoData.MyPosition, player, card);
        }

        public int MoveSelect(int player, CardModel card)
        {
            return enemy.SelectMoveLogic(AtamakkoData.MyPosition, player, card);
        }
    }
}
