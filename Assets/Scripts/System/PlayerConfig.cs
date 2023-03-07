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
    [SerializeField]private bool DevelopMode = true;//開発モードかどうか（常に初期化される)
    public static bool IsOnline = false;
    public static int StoryProgress =0;//ストーリーの進行具合
    public static int lastChapter = 1;
    public static int ArenaRank;//アリーナのランク
    public static string LastPlayStory;//最後に遊んだ章を記録する
    public static int PlayerRate;//オンラインのプレイヤーのレート
    private static int isTutorial;//0の時はチュートリアルをまだ受けていない
    public static bool afterBattle = false;//戦闘後かどうか
    private static bool isInit = true;
    [SerializeField] private bool isDataReset;
    private static bool DataReset = true;

    public static int IsTutorial { get => isTutorial; set => isTutorial = value; }

    public static void Init()
    {
        unLockCard = PlayerPrefsUtility.LoadList<bool>("unLockcard");//unLockcardから配列を読み込む
        unLockEquipment = PlayerPrefsUtility.LoadList<bool>("unLockEquipment");//unLockEquipmentから配列を読み込む
        PlayerName = PlayerPrefs.GetString("PlayerName", "Player");//名前を読み込む、
        Deck = PlayerPrefsUtility.LoadList<int>("MyDeck");//自分のデッキを読み込む
        Equipmnet = PlayerPrefsUtility.LoadList<int>("MyEquipmnet");//自分の装備を読み込む
        IsTutorial = PlayerPrefs.GetInt("Tutorial", 0);
        StoryProgress = PlayerPrefs.GetInt("StoryProgress", 1);
        ArenaRank = PlayerPrefs.GetInt("ArenaRank", 21);
        LastPlayStory = PlayerPrefs.GetString("LastPlayStory", "null");
        PlayerRate = PlayerPrefs.GetInt("PlayerRate", 1500);
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
        PlayerPrefs.SetInt("PlayerRate", PlayerRate);
    }
    void Start()
    {
        StartCoroutine(CardData.GetData());
        StartCoroutine(enemyDeckData.GetData());
        StartCoroutine(equipmentData.GetData());
        if (isInit)
        {
            Init();//プレイヤーコンフィグを読み込む
            isInit = false;
        }
        if (IsTutorial == 0 || DevelopMode)
        {
            Debug.Log("チュートリアル処理を行いました。");
            Init();//プレイヤーコンフィグを読み込む
            IsTutorial = 1;
            PlayerPrefs.SetInt("Tutorial", IsTutorial);//チュートリアルを一度だけ行わせる
            DataClear();//データをすべて消す
            DataSet();//データの初期値を入力する。
            PlayerStatusReset();//プレイヤーのステータスの初期化

            SetData();
        }
        if (isDataReset && DataReset) PlayerStatusReset();
    }

    public void PlayerStatusReset()
    {
        Debug.Log("デバック処理を行いました。");
        ArenaRank = 21;
        LastPlayStory = "null";
        PlayerRate = 1500;
        StoryProgress = 1;
        DataReset = false;
    }

    //データのリセットと空データの設定
    private void DataClear()
    {
        Equipmnet.Clear();
        Deck.Clear();
        unLockCard.Clear();
        unLockEquipment.Clear();

        var DevelopModeEquipmentNum = Resources.Load<equipmentIcon>("EquipmentIcon").equipmentIconList.Count;//用意されているEquipmentの数
        var DevelopModeCardNum = Resources.Load<CardIcon>("CardIcon").cardIconList.Count;//用意されているcardの数
        for (int i = 0; i < DevelopModeCardNum; i++)
        {
            unLockCard.Add(false);
        }
        for (int i = 0; i < DevelopModeEquipmentNum; i++)
        {
            unLockEquipment.Add(false);
        }
    }

    //初期値の設定
    private void DataSet()
    {
        for (int i = 0; i < 8; i++)
        {
            unLockCard[i] = true;
        }
        unLockEquipment[0] = true;
        unLockEquipment[1] = true;
        unLockEquipment[2] = true;
        unLockEquipment[48] = true;
        unLockEquipment[49] = true;
        unLockEquipment[51] = true;

        Equipmnet.AddRange(new List<int>() { 1, 0, 2, 48, 49, 51 });
        Deck.AddRange(new List<int>() { 1, 1, 0, 0, 2, 2, 5, 5, 6, 6, 7, 8 });
    }

    
}
