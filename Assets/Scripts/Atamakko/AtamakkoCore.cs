using System;
using System.Audio;
using System.Collections.Generic;
using UnityEngine;

namespace Atamakko
{
    /// <summary>
    /// アタマッコの原型　継承元
    /// </summary>
    public class AtamakkoCore : MonoBehaviour
    {
        public AtamakkoData AtamakkoData { get; private set; } // アタマッコの内部情報
        protected DeckData DeckData { get; private set; } // デッキ情報
        [SerializeField] private GameObject[] sSlot; // スロット　移動に使う
        
        public bool UsedUltimate { get; protected set; } // 必殺技が使用済みかどうか

        /// <summary>
        /// アタマッコの初期化。デッキ情報を格納する。
        /// </summary>
        /// <param name="deck">デッキに入っているカードID</param>
        public void Initialize(List<int> deck)
        {
            AtamakkoData = GetComponent<AtamakkoData>();
            DeckData = GetComponent<DeckData>();
            DeckData.MyDeck = deck;
            DeckData.DeckCards = new List<int>(DeckData.MyDeck);
        }

        public void DebugDeck()
        {
            foreach (var cardID in DeckData.DeckCards)
            {
                Debug.Log("今のデッキ："+cardID);
            }
        }
        
        public void DebugHand()
        {
            foreach (var cardID in DeckData.HandCards)
            {
                Debug.Log("今のハンド："+cardID);
            }
        }

        /// <summary>
        /// 必殺技等で得た補正を0にする。
        /// </summary>
        public void ResetCorrection()
        {
            AtamakkoData.DamageCorrection = 0;
            AtamakkoData.SpeedCorrection = 0;
        }

        /// <summary>
        /// デッキをシャッフルする。
        /// </summary>
        public void ShuffleDeck()
        {
            System.Random random = new System.Random((int) DateTime.Now.Ticks);
            for (int i = 0; i < DeckData.DeckCards.Count; i++)
            {
                int index = i + (int) (random.NextDouble() * (DeckData.DeckCards.Count - i));
                (DeckData.DeckCards[index], DeckData.DeckCards[i]) = (DeckData.DeckCards[i], DeckData.DeckCards[index]);
            }
        }

        /// <summary>
        /// デッキを補充する。
        /// </summary>
        public void RefillDeck()
        {
            DeckData.UsedCards.Clear();
            DeckData.DeckCards = new List<int>(DeckData.MyDeck);
            ShuffleDeck();
        }

        /// <summary>
        /// デッキの山札情報を取得する。
        /// </summary>
        /// <returns>デッキの山札のカードIDリスト</returns>
        public virtual List<int> GetDeck()
        {
            return DeckData.DeckCards;
        }

        /// <summary>
        /// デッキの山札情報を初期化する。
        /// </summary>
        /// <param name="list"></param>
        public void SetDeck(List<int> list)
        {
            DeckData.DeckCards = new List<int>(list);
        }
        
        /// <summary>
        /// デッキがあるかどうかを調べる。
        /// </summary>
        /// <returns>デッキがあるかどうかのbool値</returns>
        public bool CheckDeck()
        {
            return DeckData.DeckCards.Count <= 0;
        }

        /// <summary>
        /// カードをドローする。
        /// </summary>
        /// <returns>ドローしたカードID</returns>
        public int DrawCard()
        {
            DeckData.HandCards.Add(DeckData.DeckCards[0]);
            DeckData.DeckCards.Remove(DeckData.DeckCards[0]);
            return DeckData.HandCards[^1];
        }

        /// <summary>
        /// 使ったカードを使用済みカードにする。
        /// </summary>
        public void TrashCard()
        {
            foreach (var t in DeckData.SettingCards)
            {
                DeckData.UsedCards.Add(t);
            }
            DeckData.SettingCards.Clear();
        }

        /// <summary>
        /// 必殺技を使用する。
        /// </summary>
        public void UseUltimate()
        {
            switch (AtamakkoData.UltimateState)
            {
                case UltimateState.Attack:
                    AtamakkoData.DamageCorrection += 1;
                    UsedUltimate = true;
                    break;
                case UltimateState.Recover:
                    AtamakkoData.MyHp.Value += 3;
                    UsedUltimate = true;
                    break;
                case UltimateState.Speed:
                    AtamakkoData.SpeedCorrection += 1;
                    UsedUltimate = true;
                    break;
            }
        }

        /// <summary>
        /// セットしたカードIDを取得する。
        /// </summary>
        /// <param name="slotNum">スロット番号</param>
        /// <returns>セットしたカードID</returns>
        public int GetNowCardID(int slotNum)
        {
            return DeckData.SettingCards[slotNum];
        }

        /// <summary>
        /// アタマッコの先制度を取得する。
        /// </summary>
        /// <param name="initiative">カードの先制度</param>
        /// <returns>アタマッコの先制度</returns>
        public int GetInitiative(int initiative)
        {
            return initiative + AtamakkoData.SpeedCorrection;
        }

        /// <summary>
        /// 与えるダメージを取得する。
        /// </summary>
        /// <param name="damage">補正無しのダメージ</param>
        /// <returns>補正後のダメージ</returns>
        public int GetDamage(int damage)
        {
            return damage + AtamakkoData.DamageCorrection;
        }
        
        /// <summary>
        /// アタマッコを移動させる。
        /// </summary>
        /// <param name="slotNum">移動先のスロット番号</param>
        public void Move(int slotNum)
        {
            gameObject.transform.SetParent(sSlot[slotNum].transform);
            SeManager.Instance.ShotSe(SeType.MoveCard);
            AtamakkoData.MyPosition = slotNum;
        }
        
        /// <summary>
        /// ダメージを受ける。
        /// </summary>
        /// <param name="damage">受けるダメージ</param>
        public void AddDamage(int damage)
        {
            AtamakkoData.MyHp.Value -= damage;
        }
    }
}
