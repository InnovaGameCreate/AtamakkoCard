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
        private bool isCard = true;//現在カードの一覧かどうか
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

        [SerializeField]
        private GameObject cardPrefab;
        private CardController _cardController;
        [SerializeField]
        private GameObject Content;//カードを表示するオブジェクト
        private void Awake()
        {
            StartCoroutine(CardData.GetData());
        }
        void Start()
        {
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
            Equipment.SetActive(false);
        }

        IEnumerator createCard()
        {
            yield return new WaitForSeconds(0.1f);
            var contentChildCount = Content.transform.childCount;
            for (int i = 0; i < contentChildCount; i++)
            {
                Destroy(Content.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < 10; i++)
            {
                createCard(i);
            }

        }


        private void createCard(int cardID)
        {
            if(isVisualable(CardData.CardDataArrayList[cardID]))
            {
                var card = Instantiate(cardPrefab, Content.transform);
                card.transform.localScale = new Vector3(2, 2, 2);
                _cardController = card.GetComponent<CardController>();
                _cardController.Init(CardData.CardDataArrayList[cardID]);
            }
        }




        private void colorChage()
        {
            StartCoroutine(createCard());
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

        public bool isVisualable(string[] cardData)
        {
            Debug.Log(cardData[4]);
            if(cardData[4] == "移動" && Cards[1] == true)
            {
                Debug.Log(cardData[4] + "：" + Cards[1]) ;
                return true;
            }
            else if(cardData[4] == "攻撃" && Cards[0] == true)
            {
                Debug.Log(cardData[4] + "：" + Cards[0]);
                return true;
            }

            return false;

        }
    }
}