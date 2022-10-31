using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assemble
{
    public class assembleSlot : slot
    {
        AssembleDeck assemble;
        [SerializeField]
        protected Image itemShowImage;

        [SerializeField]
        private enum slotType
        {
            upper,
            mid,
            lower,
            accessory
        }
        [SerializeField]
        private slotType MySlotType;

        private hand Hand;
        public AssembleDeck Myassemble { get => assemble; set => assemble = value; }

        protected override void Start()
        {
            base.Start();
            Hand = FindObjectOfType<hand>();
            Myassemble = FindObjectOfType<AssembleDeck>();
        }

        public override void setItem(Item item)
        {

            Myassemble.RemoveItem(MyItem);
            Myassemble.SetItem(item);

            MyItem = item;

            if (item != null)
            {
                itemImage.color = new Color(1, 1, 1, 1);
                itemImage.sprite = item.MyItemImage;
                itemShowImage.color = new Color(1, 1, 1, 1);
                itemShowImage.sprite = item.MyItemImage;
            }
            else
            {
                itemImage.color = new Color(0, 0, 0, 0);
                itemShowImage.color = new Color(0, 0, 0, 0);
            }
        }
        public override void OnDrop(PointerEventData eventData)
        {
            if (!Hand.IsHavingItem()) return;
            Item gotItem = Hand.GetGrabbingItemNormal();
            if (MySlotType.ToString() != gotItem.MyPartsType.ToString()) return;
            gotItem = Hand.GetGrabbingItem();
            Hand.SetGtabbingItem(MyItem);

            //ドロップした物が武器だった場合
            if (MySlotType == slotType.accessory)
            {
                //weapon Weapon = gotItem as weapon;
                //Weapon.IsRightWeapon = isRightWeapon;
            }
            setItem(gotItem);
        }

    }
}