using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardEvent : MonoBehaviour
{
    private enum scenarioType
    {
        scenario1_1,

    }
    [SerializeField]
    NovelComment Comment;
    [SerializeField]
    private GameObject NovelCanvas;
    private scenarioType scenario;
    [SerializeField]
    private GameObject[] Scene;
    private void Start()
    {
        Comment.currentChapter = 0;
        if (scenario == scenarioType.scenario1_1)
        {
            Scene[1].SetActive(false);
        }
    }

    //タイルを選択したときにおこるイベント
    public void startEvent(int eventNum)
    {
        if (scenario == scenarioType.scenario1_1)
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
                    Comment.currentChapter = 10;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 11:
                    Comment.currentChapter = 11;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 12://ドラゴンとの戦闘
                    Comment.currentChapter = 12;
                    break;
                case 13:
                    Comment.currentChapter = 13;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 14:
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 15:
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 16:
                    Comment.currentChapter = 16;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 17://敵との戦闘
                    Comment.currentChapter = 17;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 18:
                    Comment.currentChapter = 18;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19://ボス戦
                    Comment.currentChapter = 19;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                default:
                    Debug.Log("何も設定されていない開始イベントです");
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
        if (scenario == scenarioType.scenario1_1)
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
                default:
                    Debug.Log("何も設定されていない終了イベントです");
                    break;
            }
        }
    }
}
