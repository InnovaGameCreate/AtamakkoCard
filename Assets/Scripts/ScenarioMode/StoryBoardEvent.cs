using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardEvent : MonoBehaviour
{
    private enum scenarioType
    {
        scenario1,
        scenario2,
        scenario3,
        scenario4,

    }
    [SerializeField]
    NovelComment Comment;
    [SerializeField]
    private GameObject NovelCanvas;
    [SerializeField]
    private scenarioType scenario;
    [SerializeField]
    private GameObject[] Scene;
    private void Start()
    {
        Comment.currentChapter = 0;
        if (scenario == scenarioType.scenario1)
        {
            Scene[1].SetActive(false);
        }
    }

    //タイルを選択したときにおこるイベント
    public void startEvent(int eventNum)
    {
        if (scenario == scenarioType.scenario1)
        {
            switch (eventNum)
            {
                case 1:
                    Comment.currentChapter = 0;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 2:
                    Comment.currentChapter = 1;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 3:
                    Comment.currentChapter = 2;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 4:
                    Comment.currentChapter = 3;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 5:
                    Comment.currentChapter = 4;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 6:
                    //5の終了時イベントに以降
                    break;
                case 7:
                    Comment.currentChapter = 6;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 8:
                    Comment.currentChapter = 7;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 9:
                    //戦闘
                    Comment.currentChapter = 8;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 10:
                    Comment.currentChapter = 9;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 11:
                    Comment.currentChapter = 10;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 12://ドラゴンとの戦闘
                    Comment.currentChapter = 11;
                    break;
                case 13:
                    Comment.currentChapter = 12;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 14:
                    Comment.currentChapter = 13;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 15:
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 16:
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 17://敵との戦闘
                    Comment.currentChapter = 16;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 18:
                    Comment.currentChapter = 17;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19://ボス戦
                    Comment.currentChapter = 18;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 21:
                    Comment.currentChapter = 20;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 22:
                    Comment.currentChapter = 21;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 23:
                    Comment.currentChapter = 22;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 24://仲間と合流
                    Comment.currentChapter = 23;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 25://戦闘

                    break;
                case 26:
                    Comment.currentChapter = 25;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 27:
                    Comment.currentChapter = 26;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 28://ボス戦
                    Comment.currentChapter = 27;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 29:
                    Comment.currentChapter = 28;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 30://戦闘
                    Comment.currentChapter = 29;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 31:
                    Comment.currentChapter = 30;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 32:
                    Comment.currentChapter = 31;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 33:
                    Comment.currentChapter = 32;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 34://戦闘
                    Comment.currentChapter = 33;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 35:
                    Comment.currentChapter = 34;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 36://戦闘
                    break;
                default:
                    Debug.Log("何も設定されていない開始イベントです");
                    break;
            }
        }
        if (scenario == scenarioType.scenario2)
        {
            switch (eventNum)
            {
                case 1:
                    Comment.currentChapter = 0;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 2:
                    Comment.currentChapter = 1;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 3:
                    Comment.currentChapter = 2;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 4:
                    Comment.currentChapter = 3;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 5:
                    Comment.currentChapter = 4;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 6:
                    Comment.currentChapter = 5;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 7:
                    Comment.currentChapter = 6;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 8:
                    Comment.currentChapter = 7;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 9:
                    Comment.currentChapter = 8;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 10:
                    Comment.currentChapter = 9;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 11:
                    Comment.currentChapter = 10;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 12:
                    Comment.currentChapter = 11;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 13:
                    Comment.currentChapter = 12;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 14:
                    Comment.currentChapter = 13;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 15:
                    //戦闘
                    break;
                case 16:
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 17:
                    //戦闘
                    break;
                case 18:
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19:
                    //ボス戦
                default:
                    break;
            }
        }
    }
    //会話終了時に起こるイベント
    public void endEvent(int eventNum)
    {
        StartCoroutine(talkEndEvent(eventNum));
    }

    IEnumerator talkEndEvent(int eventNum)
    {
        yield return new WaitForFixedUpdate();
        if (scenario == scenarioType.scenario1)
        {
            switch (eventNum)
            {
                case 5:
                    Scene[0].SetActive(false);
                    Scene[1].SetActive(true);
                    Comment.currentChapter = 5;
                    NovelCanvas.SetActive(true);
                    Comment.ChangeBackGroundImage(1);
                    Comment.nullText();
                    Comment.onAnimation = true;
                    yield return new WaitForSeconds(2f);
                    Comment.ChangeBackGroundImage(2);
                    yield return new WaitForSeconds(2f);
                    Comment.nextText();
                    Comment.onAnimation = false;
                    Debug.Log("終了イベント5");
                    break;
                case 17:
                    Comment.ChangeBackGroundImage(0);
                    //戦闘開始の信号を送る
                    Debug.Log("終了イベント17");
                    break;
                case 19:
                    Comment.ChangeBackGroundImage(0);
                    //戦闘開始の信号を送る
                    Debug.Log("終了イベント19");
                    break;
                case 28:
                    Comment.ChangeBackGroundImage(0);
                    //戦闘開始の信号を送る
                    Debug.Log("終了イベント28");
                    break;
                default:
                    Debug.Log("何も設定されていない終了イベントです");
                    break;
            }
        }
        else if (scenario == scenarioType.scenario2)
        {
            switch (eventNum)
            {
                case 8:
                    //アタマッコの新しい装備を手に入れる
                    Debug.Log("終了イベント8");
                    break;
                case 10:
                    var tile12 = GameObject.Find("tile (12)");
                    tile12.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile12.GetComponent<Button>().enabled = true;
                    tile12.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //新しい道の開拓
                    break;
                case 12:
                    var tile13 = GameObject.Find("tile (13)");
                    tile13.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile13.GetComponent<Button>().enabled = true;
                    tile13.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //新しい道の開拓
                    break;
                case 13:
                    Scene[1].SetActive(true);
                    NovelCanvas.SetActive(true);
                    yield return new WaitForSeconds(4f);
                    Comment.ChangeBackGroundImage(0);
                    var tile14 = GameObject.Find("tile (14)");
                    var tile15 = GameObject.Find("tile (15)");
                    var tile16 = GameObject.Find("tile (16)");
                    var tile17 = GameObject.Find("tile (17)");
                    var tile18 = GameObject.Find("tile (18)");
                    var tile19 = GameObject.Find("tile (19)");
                    tile14.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile15.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile16.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile17.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile18.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile19.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    tile14.GetComponent<Button>().enabled = true;
                    tile15.GetComponent<Button>().enabled = true;
                    tile16.GetComponent<Button>().enabled = true;
                    tile17.GetComponent<Button>().enabled = true;
                    tile18.GetComponent<Button>().enabled = true;
                    tile19.GetComponent<Button>().enabled = true;
                    tile14.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    tile15.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    tile16.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    tile17.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    tile18.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    tile19.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                    //新しい道の開拓
                    NovelCanvas.SetActive(false);
                    break;
                default:
                    Debug.Log("何も設定されていない終了イベントです");
                    break;
            }
        }
    }
}
