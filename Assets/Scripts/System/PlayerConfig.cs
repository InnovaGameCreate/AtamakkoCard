using Arena;
using Assemble;
using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    public static List<bool> unLockCard = new List<bool>();//�ǂ̃J�[�h���擾���Ă��邩
    public static List<bool> unLockEquipment = new List<bool>();//�ǂ̑������擾���Ă��邩
    public static string PlayerName;//�v���C���[�̖��O
    public static List<int> Deck = new List<int>();
    public static List<int> Equipmnet = new List<int>();//(�㕔�A�����A�����A�A�N�Z�T��)
    public static bool DevelopMode = true;//�J�����[�h���ǂ����i��ɏ����������)
    private int DevelopModeCardNum;//card�̌�
    private int DevelopModeEquipmentNum;//equipment�̌�
    private static int isTutorial;//0�̎��̓`���[�g���A�����܂��󂯂Ă��Ȃ�

    public static void Init()
    {
        unLockCard = PlayerPrefsUtility.LoadList<bool>("unLockcard");//unLockcard����z���ǂݍ���
        unLockEquipment = PlayerPrefsUtility.LoadList<bool>("unLockEquipment");//unLockEquipment����z���ǂݍ���
        PlayerName = PlayerPrefs.GetString("PlayerName", "Player");//���O��ǂݍ��ށA
        Deck = PlayerPrefsUtility.LoadList<int>("MyDeck");//�����̃f�b�L��ǂݍ���
        Equipmnet = PlayerPrefsUtility.LoadList<int>("MyEquipmnet");//�����̑�����ǂݍ���
        isTutorial = PlayerPrefs.GetInt("Tutorial", 0);
    }

    public static void SetData()
    {
        PlayerPrefsUtility.SaveList("unLockcard", unLockCard);
        PlayerPrefsUtility.SaveList("unLockEquipment", unLockEquipment);
        PlayerPrefsUtility.SaveList<int>("MyDeck", Deck);
        PlayerPrefsUtility.SaveList<int>("MyEquipmnet", Equipmnet);
        PlayerPrefs.SetString("PlayerName", PlayerName);
    }
    void Start()
    {
        StartCoroutine(CardData.GetData());
        StartCoroutine(enemyDeckData.GetData());
        StartCoroutine(equipmentData.GetData());
        if (isTutorial == 0 || DevelopMode)
        {
            Init();
            Debug.Log("�`���[�g���A���������s���܂����B");
            isTutorial = 1;
            PlayerPrefs.SetInt("Tutorial", isTutorial);//�`���[�g���A������x�����s�킹��
            Equipmnet.Clear();
            Deck.Clear();
            unLockCard.Clear();
            unLockEquipment.Clear();
            for (int i = 0; i < 10; i++)
            {
                unLockCard.Add(false);
            }
            for (int i = 10; i < DevelopModeCardNum; i++)
            {
                unLockCard.Add(true);
            }
            for (int i = 0; i < 40; i++)
            {
                unLockEquipment.Add(false);
            }
            for (int i = 40; i < DevelopModeEquipmentNum; i++)
            {
                unLockEquipment.Add(true);
            }
            Equipmnet.Add(1);
            Equipmnet.Add(0);
            Equipmnet.Add(2);
            Equipmnet.Add(48);
            Equipmnet.Add(49);
            Equipmnet.Add(50);
            Deck.Add(1);
            Deck.Add(1);
            Deck.Add(0);
            Deck.Add(0);
            Deck.Add(2);
            Deck.Add(2);
            Deck.Add(5);
            Deck.Add(5);
            Deck.Add(6);
            Deck.Add(6);
            Deck.Add(7);
            Deck.Add(8);
            SetData();

        }
        for (int i = 0; i < Equipmnet.Count; i++)
        {
            Debug.Log(i+"�Ԗڂɐݒ肳��Ă��鐔��"+Equipmnet[i]);
        }
        if (DevelopMode)
        {
            Debug.Log("�f�o�b�N�������s���܂����B");
            DevelopModeEquipmentNum = Resources.Load<equipmentIcon>("EquipmentIcon").equipmentIconList.Count;
            DevelopModeCardNum = Resources.Load<CardIcon>("CardIcon").cardIconList.Count;

            Debug.Log("unLockCard�̗v�f����" + unLockCard.Count + "�ł�");
            Debug.Log("unLockEquipment�̗v�f����" + unLockEquipment.Count + "�ł�");
            unLockCard.Clear();
            unLockEquipment.Clear();
            for (int i = 0; i < 10; i++)
            {
                unLockCard.Add(false);
            }
            for (int i = 10; i < DevelopModeCardNum; i++)
            {
                unLockCard.Add(true);
            }
            for (int i = 0; i < 40; i++)
            {
                unLockEquipment.Add(false);
            }
            for (int i = 40; i < DevelopModeEquipmentNum; i++)
            {
                unLockEquipment.Add(true);
            }
            DevelopMode = false;
        }
    }
}
