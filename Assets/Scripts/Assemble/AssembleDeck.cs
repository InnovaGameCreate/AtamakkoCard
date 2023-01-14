using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assemble
{
    public class AssembleDeck : MonoBehaviour
    {
        [SerializeField]
        private List<int> DeckDataList;

        private List<Item> item = new List<Item>();


        private void createDeck()
        {
            if(DeckDataList != null) DeckDataList.Clear();//DeckDatListがnullでない場合は初期化する
            foreach (Item item in MyItems)
            {
                DeckDataList.Add(item.MyCardNum1);
                DeckDataList.Add(item.MyCardNum2);
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
        }

        IEnumerator finishAssemble()
        {
            createDeck();//デッキを作る
            saveData();

            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene("Title");//タイトルに戻る
        }
        public void finish()
        {
            StartCoroutine(finishAssemble());
        }

        private void saveData()
        {
            //カードのデッキをセーブする
            if (DeckDataList.Count != 12)
            {
                Debug.Log("カードの枚数が12枚ではないです");
                return;
            }
            if (DeckDataList.Count == 12)
            {
                PlayerConfig.Deck.Clear();//デッキにデータがあれば消す
                PlayerConfig.Deck = DeckDataList;
                Debug.Log("カードを保存することができました");
                PlayerConfig.SetData();
                return;
            }
        }

    }
}