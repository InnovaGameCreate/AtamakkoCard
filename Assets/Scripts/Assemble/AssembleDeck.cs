using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assemble
{
    public class AssembleDeck : MonoBehaviour
    {
        private List<Item> item = new List<Item>();


        private void Start()
        {
            StartCoroutine("finishAssemble");
        }

        /*
        public int MyTAtk
        {
            get
            {
                int itemAtk = 0;

                foreach (Item item in MyItems)
                {
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
                }

                return TAtk + itemAtk;
            }
        }
        */
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
            saveData();
        }

        private void saveData()
        {
            //カードのデッキをセーブする
        }
    }
}