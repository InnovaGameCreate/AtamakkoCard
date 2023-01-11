using Arena;
using Assemble;
using Card;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    public static List<bool> unLockCard = new List<bool>();//どのカードを取得しているか
    public static List<bool> unLockEquipment = new List<bool>();//どの装備を取得しているか
    public static string PlayerName;//プレイヤーの名前
    public static List<int> Deck = new List<int>();
    public static List<int> Equipmnet = new List<int>();//(上部、中央、下部、アクセサリ)
    public static bool DevelopMode = true;//開発モードかどうか（常に初期化される)
    public static bool IsOnline = false;
    public static int StoryProgress;//ストーリーの進行具合
    public static int ArenaRank;//アリーナのランク
    public static string LastPlayStory;//最後に遊んだ章を記録する
    private int DevelopModeCardNum;//cardの個数
    private int DevelopModeEquipmentNum;//equipmentの個数
    private static int isTutorial;//0の時はチュートリアルをまだ受けていない

    public static int IsTutorial { get => isTutorial; set => isTutorial = value; }

    public static void Init()
    {
        unLockCard = PlayerPrefsUtility.LoadList<bool>("unLockcard");//unLockcardから配列を読み込む
        unLockEquipment = PlayerPrefsUtility.LoadList<bool>("unLockEquipment");//unLockEquipmentから配列を読み込む
        PlayerName = PlayerPrefs.GetString("PlayerName", "Player");//名前を読み込む、
        Deck = PlayerPrefsUtility.LoadList<int>("MyDeck");//自分のデッキを読み込む
        Equipmnet = PlayerPrefsUtility.LoadList<int>("MyEquipmnet");//自分の装備を読み込む
        IsTutorial = PlayerPrefs.GetInt("Tutorial", 0);
        StoryProgress = PlayerPrefs.GetInt("StoryProgress", 0);
        ArenaRank = PlayerPrefs.GetInt("ArenaRank", 11);
        LastPlayStory = PlayerPrefs.GetString("LastPlayStory", "null");
    }

    public static void SetData()
    {
        PlayerPrefsUtility.SaveList("unLockcard", unLockCard);
        PlayerPrefsUtility.SaveList("unLockEquipment", unLockEquipment);
        PlayerPrefsUtility.SaveList<int>("MyDeck", Deck);
        PlayerPrefsUtility.SaveList<int>("MyEquipmnet", Equipmnet);
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.SetInt("StoryProgress", StoryProgress);
        PlayerPrefs.SetInt("ArenaRank", ArenaRank);
        PlayerPrefs.SetString("LastPlayStory", LastPlayStory);
    }
    void Start()
    {
        StartCoroutine(CardData.GetData());
        StartCoroutine(enemyDeckData.GetData());
        StartCoroutine(equipmentData.GetData());
        if (IsTutorial == 0 || DevelopMode)
        {
            Init();
            Debug.Log("チュートリアル処理を行いました。");
            IsTutorial = 1;
            PlayerPrefs.SetInt("Tutorial", IsTutorial);//チュートリアルを一度だけ行わせる
            Equipmnet.Clear();
            Deck.Clear();
            unLockCard.Clear();
            unLockEquipment.Clear();
            for (int i = 0; i < DevelopModeCardNum; i++)
            {
                unLockCard.Add(true);
            }
            for (int i = 0; i < DevelopModeEquipmentNum; i++)
            {
                unLockEquipment.Add(true);
            }
            /*
            for (int i = 26; i < 48; i++)
            {
                unLockEquipment.Add(false);
            }
            for (int i = 48; i < DevelopModeEquipmentNum; i++)
            {
                unLockEquipment.Add(true);
            }*/
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
            //Debug.Log(i+"番目に設定されている数は"+Equipmnet[i]);
        }
        if (DevelopMode)
        {
            Debug.Log("デバック処理を行いました。");
            DevelopModeEquipmentNum = Resources.Load<equipmentIcon>("EquipmentIcon").equipmentIconList.Count;
            DevelopModeCardNum = Resources.Load<CardIcon>("CardIcon").cardIconList.Count;

            Debug.Log("unLockCardの要素数は" + unLockCard.Count + "です");
            Debug.Log("unLockEquipmentの要素数は" + unLockEquipment.Count + "です");
            unLockCard.Clear();
            unLockEquipment.Clear();
            for (int i = 0; i < 10; i++)
            {
                unLockCard.Add(true);
            }
            for (int i = 20; i < DevelopModeCardNum; i++)
            {
                unLockCard.Add(false);
            }
            for (int i = 0; i < 40; i++)
            {
                unLockEquipment.Add(true);
            }
            for (int i = 40; i < DevelopModeEquipmentNum; i++)
            {
                unLockEquipment.Add(true);
            }
            DevelopMode = false;
        }
    }
}
