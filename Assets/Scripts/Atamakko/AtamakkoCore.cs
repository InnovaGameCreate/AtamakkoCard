using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Atamakko
{
    public class AtamakkoCore : MonoBehaviour, IDamagable
    {
        public AtamakkoData AtamakkoData { get; private set; }
        protected DeckData DeckData { get; private set; }
        [SerializeField] private GameObject[] sSlot;
        
        public bool UsedUltimate { get; set; }
        //public UltimateState UltimateState { get; set; }

        public void Initialize(List<int> deck)
        {
            AtamakkoData = GetComponent<AtamakkoData>();
            DeckData = GetComponent<DeckData>();
            DeckData.MyDeck = deck;
            DeckData.DeckCards = new List<int>(DeckData.MyDeck);
            ShuffleDeck();
        }

        public void ResetCorrection()
        {
            AtamakkoData.DamageCorrection = 0;
            AtamakkoData.SpeedCorrection = 0;
        }

        private void ShuffleDeck()
        {
            System.Random random = new System.Random((int) DateTime.Now.Ticks); // ランダムのインスタンス化
            for (int i = 0; i < DeckData.DeckCards.Count; i++)
            {
                int index = i + (int) (random.NextDouble() * (DeckData.DeckCards.Count - i));
                (DeckData.DeckCards[index], DeckData.DeckCards[i]) = (DeckData.DeckCards[i], DeckData.DeckCards[index]);
            }
        }

        public void RefillDeck()
        {
            DeckData.UsedCards.Clear();
            DeckData.DeckCards = new List<int>(DeckData.MyDeck);
            ShuffleDeck();
        }

        public virtual List<int> GetDeck()
        {
            return DeckData.DeckCards;
        }

        public void SetDeck(List<int> list)
        {
            DeckData.DeckCards = new List<int>(list);
        }

        public bool CheckDeck()
        {
            return DeckData.DeckCards.Count <= 0;
        }

        public int DrawCard()
        {
            DeckData.HandCards.Add(DeckData.DeckCards[0]);
            DeckData.DeckCards.Remove(DeckData.DeckCards[0]);
            return DeckData.HandCards[^1];
        }

        public void TrashCard()
        {
            foreach (var t in DeckData.SettingCards)
            {
                DeckData.UsedCards.Add(t);
            }
            DeckData.SettingCards.Clear();
        }

        public void UseUltimate()
        {
            switch (AtamakkoData.UltimateState)
            {
                case UltimateState.Attack:
                    AtamakkoData.DamageCorrection += 1;
                    break;
                case UltimateState.Recover:
                    AtamakkoData.MyHp.Value += 3;
                    break;
                case UltimateState.Speed:
                    AtamakkoData.SpeedCorrection += 1;
                    break;
                
            }
        }

        public int GetNowCardID(int slotNum)
        {
            return DeckData.SettingCards[slotNum];
        }

        public int GetInitiative(int initiative)
        {
            return initiative + AtamakkoData.SpeedCorrection;
        }

        public int GetDamage(int damage)
        {
            return damage + AtamakkoData.DamageCorrection;
        }
        
        public void Move(int slotNum)
        {
            gameObject.transform.SetParent(sSlot[slotNum].transform);
            AtamakkoData.MyPosition = slotNum;
            Debug.Log("移動先：" + slotNum);
        }
        
        public void AddDamage(int damage)
        {
            AtamakkoData.MyHp.Value -= damage;
        }
    }
}
