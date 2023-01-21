using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Card;
using Atamakko;

namespace Assemble
{
    public class assembleSlot : slot
    {
        AssembleDeck assemble;
        [SerializeField] protected Image itemShowImage;
        [SerializeField] PlayerCharacterVisual Visual;

        [SerializeField]
        private enum slotType//装備できる装備カードのタイプ
        {
            upper,
            mid,
            lower,
            accessory
        }
        [SerializeField] private slotType MySlotType;
        [SerializeField] private GameObject CardView;//装備しているカードを表示する場所
        [SerializeField] private int PositionNum;//（上部：０、中央：１、下部２、アクセサリ３～５）
        [SerializeField] private GameObject cardPrefab;//生成するカードオブジェクトのプレファブ
        private GameObject card1;
        private GameObject card2;

        private hand Hand;
        public AssembleDeck Myassemble { get => assemble; set => assemble = value; }

        protected override void Start()
        {
            base.Start();
            Hand = FindObjectOfType<hand>();
            Myassemble = FindObjectOfType<AssembleDeck>();
            SetUp();
        }

        private void SetUp()//スプレットシートからデータを読み取る時間停止
        {
            //yield return new WaitForSeconds(0.5f);
            setUpItem();
        }
        private void setUpItem()//データから初期の装備カードをセットする
        {
            //Debug.Log(PositionNum + "：" +PlayerConfig.Equipmnet[PositionNum]);
            var quipmentNum = PlayerConfig.Equipmnet[PositionNum];
            var quipmentData = equipmentData.CardDataArrayList[quipmentNum];

            Item item = new Item();
            item.MyItemName = quipmentData[1];
            equipmentIcon cardIcon = Resources.Load<equipmentIcon>("EquipmentIcon");
            item.MyCardID = int.Parse(quipmentData[0]);
            item.MyItemImage = cardIcon.equipmentIconList[int.Parse(quipmentData[0])];
            item.MyCardNum1 = int.Parse(quipmentData[4]);
            item.MyCardNum2 = int.Parse(quipmentData[5]);
            switch (quipmentData[2])
            {
                case "上部":
                    item.MyPartsType = Item.slotType.upper;
                    break;
                case "中央":
                    item.MyPartsType = Item.slotType.mid;
                    break;
                case "下部":
                    item.MyPartsType = Item.slotType.lower;
                    break;
                case "アクセサリ":
                    item.MyPartsType = Item.slotType.accessory;
                    break;
                default:
                    break;
            }
            Debug.Log("装備"+ PositionNum + "に設置されている装備のIDは："+ item.MyCardID +"です\n" +
                "アイテムのイメージ画像は"+ item.MyItemImage+ "です\n" + 
                "item.MyCardIDの設定されているカードは" + item.MyCardNum1 + "と" + item.MyCardNum2 + "です");
            setItem(item);
        }

        private void InstantiateCard(Item item)
        {
            if (card1 != null) Destroy(card1);
            if (card2 != null) Destroy(card2);
            card1 = Instantiate(cardPrefab, CardView.transform);
            card2 = Instantiate(cardPrefab, CardView.transform);
            card1.transform.localScale = new Vector3(2, 2, 2);
            card2.transform.localScale = new Vector3(2, 2, 2);
            var _cardController1 = card1.GetComponent<CardController>();
            var _cardController2 = card2.GetComponent<CardController>();
            _cardController1.Init(item.MyCardNum1);
            _cardController2.Init(item.MyCardNum2);
        }

        public override void setItem(Item item)
        {
            
            PlayerConfig.Equipmnet[PositionNum] = item.MyCardID;


            InstantiateCard(item);
            Myassemble.RemoveItem(MyItem);
            Myassemble.SetItem(item);

            MyItem = item;

            if (item != null)
            {
                itemImage.color = new Color(1, 1, 1, 1);
                itemImage.sprite = item.MyItemImage;
                itemShowImage.color = new Color(1, 1, 1, 1);

                Visual.SetImage(PlayerConfig.Equipmnet.ToArray());
                //itemShowImage.sprite = item.MyItemImage;
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
            setItem(gotItem);
        }
        private void dataTest()
        {
            for (int i = 0; i < 40; i++)
            {
                Debug.Log("生成したカードID：" + CardData.CardDataArrayList[i][0] 
                    + "\n名前：" + CardData.CardDataArrayList[i][1]
                    + "\n種類：" + CardData.CardDataArrayList[i][4]);
            }
        }
    }
}