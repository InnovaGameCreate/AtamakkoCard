using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assemble
{
    public class AssembleDeck : MonoBehaviour
    {
        [SerializeField]
        private List<int> DeckDataList;
        public static List<int> Deck = new List<int>();

        private List<Item> item = new List<Item>();


        private void Start()
        {
            StartCoroutine("finishAssemble");
        }


        public void createDeck()
        {
            if(DeckDataList != null) DeckDataList.RemoveRange(0, DeckDataList.Count);//DeckDatListがnullでない場合は初期化する
            foreach (Item item in MyItems)
            {
                DeckDataList.Add(item.MyCardNum1);
                DeckDataList.Add(item.MyCardNum2);
                /*
                weapon Weapon = item as weapon;

                if (Weapon != null) itemAtk += Weapon.MyTAtk;
                if (Weapon.IsRightWeapon)
                {
                    RTAtk = itemAtk;
                    RReloadTime = Weapon.MyReloadTime;
                }

                else
                {
                    LTAtk = itemAtk;
                    LReloadTime = Weapon.MyReloadTime;
                }
                */
            }
        } 
        public List<Item> MyItems { get => item; set => item = value; }

        public void RemoveItem(Item item)
        {
            if (item != null) MyItems.Remove(item);
        }

        public void SetItem(Item item)
        {
            if (item != null) MyItems.Add(item);
            Debug.Log("アイテムを装備");
        }

        IEnumerator finishAssemble()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
            createDeck();//デッキを作る
            saveData();

            yield return new WaitForSeconds(0.1f);
            StartCoroutine("finishAssemble");
        }

        private void saveData()
        {
            //カードのデッキをセーブする
            if (DeckDataList.Count != 12)
            {
                Debug.Log("カードの枚数が12枚ではないです");
                return;
            }
            for (int i = 0; i < 11; i++)
            {
                if(Deck != null) Deck.RemoveRange(0, Deck.Count);//デッキにデータがあれば消す
                Deck.Add(DeckDataList[i]);
            }
            if (DeckDataList.Count == 12)
            {
                Debug.Log("カードを保存することができました");
                PlayerPrefsUtility.SaveList<int>("MyDeck", Deck);
                return;
            }
        }

    }
}