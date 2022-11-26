using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Card;

namespace Assemble
{
    public class assembleSlot : slot
    {
        AssembleDeck assemble;
        [SerializeField]
        protected Image itemShowImage;

        [SerializeField]
        private enum slotType//装備できる装備カードのタイプ
        {
            upper,
            mid,
            lower,
            accessory
        }
        [SerializeField]
        private slotType MySlotType;
        [SerializeField]
        private GameObject CardView;//装備しているカードを表示する場所
        [SerializeField]
        private int PositionNum;//（上部：０、中央：１、下部２、アクセサリ３～５）
        [SerializeField]
        private GameObject cardPrefab;//生成するカードオブジェクトのプレファブ
        private GameObject card1;
        private GameObject card2;

        private hand Hand;
        public AssembleDeck Myassemble { get => assemble; set => assemble = value; }

        protected override void Start()
        {
            base.Start();
            Hand = FindObjectOfType<hand>();
            Myassemble = FindObjectOfType<AssembleDeck>();
            StartCoroutine(SetUp());
        }

        IEnumerator SetUp()//スプレットシートからデータを読み取る時間停止
        {
            yield return new WaitForSeconds(0.5f);

            setUpItem();
        }
        private void setUpItem()//データから初期の装備カードをセットする
        {
            Debug.Log(PositionNum + "：" +PlayerConfig.Equipmnet[PositionNum]);
            var quipmentNum = PlayerConfig.Equipmnet[PositionNum];
            var quipmentData = equipmentData.CardDataArrayList[quipmentNum];

            Item item = new Item();
            item.MyItemName = quipmentData[1];
            equipmentIcon cardIcon = Resources.Load<equipmentIcon>("EquipmentIcon");
            item.MyCardID = int.Parse(quipmentData[0]);
            item.MyItemImage = cardIcon.equipmentIconList[int.Parse(quipmentData[0])];
            item.MyCardNum1 = int.Parse(quipmentData[4]);
            item.MyCardNum1 = int.Parse(quipmentData[5]);
            setItem(item);
        }

        private void InstatiateCard(Item item)
        {
            if (card1 != null) Destroy(card1);
            if (card2 != null) Destroy(card2);

            card1 = Instantiate(cardPrefab, CardView.transform);
            card2 = Instantiate(cardPrefab, CardView.transform);
            card1.transform.localScale = new Vector3(2, 2, 2);
            card2.transform.localScale = new Vector3(2, 2, 2);
            var _cardController1 = card1.GetComponent<CardController>();
            var _cardController2 = card2.GetComponent<CardController>();
            _cardController1.Init(CardData.CardDataArrayList[item.MyCardNum1]);
            _cardController2.Init(CardData.CardDataArrayList[item.MyCardNum2]);

        }

        public override void setItem(Item item)
        {
            Debug.Log(MySlotType + "のEquipmnet[PositionNum]を" + item.MyCardID + "に変更しました");
            PlayerConfig.Equipmnet[PositionNum] = item.MyCardID;


            InstatiateCard(item);
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

            /*
            //ドロップした物が武器だった場合
            if (MySlotType == slotType.accessory)
            {
                //weapon Weapon = gotItem as weapon;
                //Weapon.IsRightWeapon = isRightWeapon;
            }
            */
            setItem(gotItem);
        }

    }
}