using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assemble
{



    public class AllCard : MonoBehaviour
    {
        [SerializeField]
        private GameObject Window;

        private bool[] haveCard = new bool[2];//どのカードが表示されるか（所持、未所持）
        [SerializeField]
        private Image[] haveButton;//カードを所持しているか未所持か（所持、未所持）
        [SerializeField]
        private Image EquipmentButton;//表示しているのは装備かどうか
        [SerializeField]
        private Image CardButton;//表示しているのはカードかどうか
        private bool isCard = false;//現在カードの一覧かどうか
        [SerializeField]
        private GameObject Equipment;//装備のボタン
        [SerializeField]
        private Image[] EquipmentButtons;
        private bool[] Equipments = new bool[4];//どのタイプの装備が表示されるか（上部、中央、下部、アクセサリ）
        [SerializeField]
        private GameObject Card;//カードのボタン
        [SerializeField]
        private Image[] CardButtons;
        private bool[] Cards = new bool[2];//どのタイプのカードが表示されるか（攻撃、移動）

        private bool setUp = false;//セットアップ中かどうか

        [SerializeField]
        private GameObject cardPrefab;//生成するカードオブジェクトのプレファブ
        private CardController _cardController;
        [SerializeField]
        private GameObject equipmentPrefab;//生成するカードオブジェクトのプレファブ
        private equipmentController _equipmentController;
        [SerializeField]
        private GameObject Content;//カードを表示するオブジェクト
        int DevelopModeEquipmentNum;
        int DevelopModeCardNum;
        private void Awake()
        {
            Debug.Log("カードと装備データの読み込み");
            StartCoroutine(CardData.GetData());
            StartCoroutine(equipmentData.GetData());
        }
        void Start()
        {
            DevelopModeEquipmentNum = Resources.Load<equipmentIcon>("EquipmentIcon").equipmentIconList.Count;
            DevelopModeCardNum = Resources.Load<CardIcon>("CardIcon").cardIconList.Count;

            haveCard[0] = true;
            haveCard[1] = false;
            for (int i = 0; i < 4; i++)
            {
                Equipments[i] = true;
            }
            for (int i = 0; i < 2; i++)
            {
                Cards[i] = true;
            }
            colorChage();
            StartCoroutine(SetUp());
            Equipment.SetActive(false);
        }
        IEnumerator SetUp()
        {
            yield return new WaitForSeconds(0.5f);
            setUp = true;
            StartCoroutine(createCard());

        }
        IEnumerator createCard()
        {
            yield return new WaitForSeconds(0.1f);
            var contentChildCount = Content.transform.childCount;
            for (int i = 0; i < contentChildCount; i++)
            {
                Destroy(Content.transform.GetChild(i).gameObject);
            }
            if (isCard)
            {
                for (int i = 0; i < DevelopModeCardNum; i++)
                {
                    createCard(i);
                }
            }
            else
            {
                for (int i = 0; i < DevelopModeEquipmentNum; i++)
                {
                    createEquipment(i);
                }
            }

        }


        private void createCard(int cardID)
        {
            if(isVisualable(CardData.CardDataArrayList[cardID],true))
            {
                var card = Instantiate(cardPrefab, Content.transform);
                card.transform.localScale = new Vector3(2, 2, 2);
                _cardController = card.GetComponent<CardController>();
                _cardController.Init(cardID);
            }
        }
        private void createEquipment(int equipmentID)
        {
            if (isVisualable(equipmentData.CardDataArrayList[equipmentID],false))
            {
                var card = Instantiate(equipmentPrefab, Content.transform);
                card.transform.localScale = new Vector3(2, 2, 2);
                _equipmentController = card.GetComponent<equipmentController>();
                _equipmentController.Init(equipmentData.CardDataArrayList[equipmentID]);
            }
        }




        private void colorChage()
        {
            if(setUp)StartCoroutine(createCard());
            if (isCard)
            {
                EquipmentButton.color = Color.gray;
                CardButton.color = Color.white;
            }
            else
            {
                EquipmentButton.color = Color.white;
                CardButton.color = Color.gray;
            }
            for (int i = 0; i < 2; i++)
            {
                if (haveCard[i]) haveButton[i].color = Color.white;
                else haveButton[i].color = Color.gray;
            }
            for (int i = 0; i < 4; i++)
            {
                if (Equipments[i]) EquipmentButtons[i].color = Color.white;
                else EquipmentButtons[i].color = Color.gray;
            }
            for (int i = 0; i < 2; i++)
            {
                if (Cards[i]) CardButtons[i].color = Color.white;
                else CardButtons[i].color = Color.gray;
            }
        }

        public void changeType(bool b)
        {
            isCard = b;
            if (b)
            {
                Equipment.SetActive(false);
                Card.SetActive(true);
            }
            else
            {
                Equipment.SetActive(true);
                Card.SetActive(false);
            }
            colorChage();
        }
        public void changeHave(int i)
        {
            if (haveCard[i])
                haveCard[i] = false;
            else haveCard[i] = true;
            colorChage();
        }
        public void changeCard(int i)
        {
            if (Cards[i]) Cards[i] = false;
            else Cards[i] = true;
            colorChage();
        }
        public void changeEquipments(int i)
        {
            if (Equipments[i])
                Equipments[i] = false;
            else Equipments[i] = true;
            colorChage();
        }

        public void open()
        {
            Window.SetActive(true);
        }
        public void close()
        {
            Window.SetActive(false);
        }

        public bool isVisualable(string[] cardData,bool isSelectCard)
        {
            if (isCard && isSelectCard)
            {
                if ((haveCard[0] && PlayerConfig.unLockCard[int.Parse(cardData[0])]) || (haveCard[1] && !PlayerConfig.unLockCard[int.Parse(cardData[0])]))
                {
                    if (cardData[4] == "移動" && Cards[1] == true)
                    {
                        return true;
                    }
                    else if (cardData[4] == "攻撃" && Cards[0] == true)
                    {
                        return true;
                    }
                }

            }
            else if(!isCard && !isSelectCard)
            {

                if ((haveCard[0] && PlayerConfig.unLockEquipment[int.Parse(cardData[0])]) || (haveCard[1] && !PlayerConfig.unLockEquipment[int.Parse(cardData[0])]))
                {
                    if (Equipments[0] && cardData[2] == "上部") return true;
                    if (Equipments[1] && cardData[2] == "中央") return true;
                    if (Equipments[2] && cardData[2] == "下部") return true;
                    if (Equipments[3] && cardData[2] == "アクセサリ") return true;
                }
            }
            return false;

        }
    }
}