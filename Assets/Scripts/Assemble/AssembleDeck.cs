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


        private void Start()
        {
        }


        public void createDeck()
        {
            if(DeckDataList != null) DeckDataList.RemoveRange(0, DeckDataList.Count);//DeckDatList��null�łȂ��ꍇ�͏���������
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
            createDeck();//�f�b�L�����
            saveData();

            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene("Title");//�^�C�g���ɖ߂�
        }
        public void finish()
        {
            StartCoroutine(finishAssemble());
        }

        private void saveData()
        {
            //�J�[�h�̃f�b�L���Z�[�u����
            if (DeckDataList.Count != 12)
            {
                Debug.Log("�J�[�h�̖�����12���ł͂Ȃ��ł�");
                return;
            }
            for (int i = 0; i < 11; i++)
            {
                if(PlayerConfig.Deck != null) PlayerConfig.Deck.RemoveRange(0, PlayerConfig.Deck.Count);//�f�b�L�Ƀf�[�^������Ώ���
                PlayerConfig.Deck.Add(DeckDataList[i]);
            }
            if (DeckDataList.Count == 12)
            {
                Debug.Log("�J�[�h��ۑ����邱�Ƃ��ł��܂���");
                PlayerConfig.SetData();
                return;
            }
        }

    }
}