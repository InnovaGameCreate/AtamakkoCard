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
        scenarioBlue1,
        scenarioBlue2,
        scenarioBlue3,
        scenarioRed1,
        scenarioRed2,
        scenarioRed3,
        scenarioWhite1,
        scenarioWhite2,
        scenarioWhite3,
        scenarioend1,
        scenarioend2,
        scenarioend3,

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
        Scene[1].SetActive(false);
    }

    //タイルを選択したときにおこるイベント
    public void startEvent(int eventNum)
    {
        if (scenario == scenarioType.scenario1)
        {
            #region
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
            #endregion
        }
        else if (scenario == scenarioType.scenario2)
        {
            #region
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
            #endregion
        }
        else if (scenario == scenarioType.scenarioBlue1)
        {
            #region
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
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    //戦闘
                    break;
                case 16:
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 17:
                    Comment.currentChapter = 16;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 18:
                    Comment.currentChapter = 17;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19:
                    Comment.currentChapter = 18;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 20:
                    Comment.currentChapter = 19;
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
                case 24:
                    break;
                case 25:
                    Comment.currentChapter = 23;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 26:
                    Comment.currentChapter = 24;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 27:
                    //戦闘
                    break;
                case 28:
                    //戦闘
                    break;
                case 29:
                    Comment.currentChapter = 25;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 30:
                    Comment.currentChapter = 26;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                default:
                    break;
            }
            #endregion
        }
        else if(scenario == scenarioType.scenarioBlue2)
        {
            #region
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
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 16:
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 17:
                    Comment.currentChapter = 16;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 18:
                    Comment.currentChapter = 17;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19:
                    Comment.currentChapter = 18;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 20:
                    Comment.currentChapter = 19;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 21:
                    //戦闘
                    break;
                case 22:
                    Comment.currentChapter = 20;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 23:
                    //戦闘
                    break;
                case 24:
                    Comment.currentChapter = 21;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 25:
                    Comment.currentChapter = 22;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                default:
                    break;
            }
            #endregion
        }
        else if(scenario == scenarioType.scenarioBlue3)
        {
            #region
            switch(eventNum)
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
                    //戦闘
                    break;
                case 6:
                    Comment.currentChapter = 4;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 7:
                    Comment.currentChapter = 5;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 8:
                    Comment.currentChapter = 6;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 9:
                    Comment.currentChapter = 7;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 10:
                    Comment.currentChapter = 8;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 11:
                    Comment.currentChapter = 9;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 12:
                    Comment.currentChapter = 10;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 13:
                    Comment.currentChapter = 11;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 14:
                    Comment.currentChapter = 12;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 15:
                    Comment.currentChapter = 13;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 16:
                    //戦闘
                    break;
                case 17:
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 18:
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19:
                    Comment.currentChapter = 16;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 20:
                    Comment.currentChapter = 17;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 21:
                    Comment.currentChapter = 18;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 22:
                    Comment.currentChapter = 19;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 23:
                    Comment.currentChapter = 20;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;

                default:
                    break;
            }
            #endregion
        }
        else if (scenario == scenarioType.scenarioRed1)
        {
            #region
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
                    //野生生物と戦闘
                    break;
                case 6:
                    Comment.currentChapter = 4;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 7:
                    Comment.currentChapter = 5;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 8:
                    //魔物との戦闘
                    break;
                case 9:
                    Comment.currentChapter = 6;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 10:
                    Comment.currentChapter = 7;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 11:
                    Comment.currentChapter = 8;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 12:
                    Comment.currentChapter = 9;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 13:
                    Comment.currentChapter = 10;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 14:
                    Comment.currentChapter = 11;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 15:
                    Comment.currentChapter = 12;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 16:
                    Comment.currentChapter = 13;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 17:
                    //魔物との戦闘
                    break;
                case 18:
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19:
                    //ボス戦
                    break;
                case 20:
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 21:
                    Comment.currentChapter = 16;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 22:
                    Comment.currentChapter = 17;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 23:
                    Comment.currentChapter = 18;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 24:
                    //邪神の加護を受けた魔物との戦闘
                    break;
                case 25:
                    Comment.currentChapter = 19;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;

                default:
                    break;
            }
            #endregion
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
            #region
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
            #endregion
        }
        else if (scenario == scenarioType.scenario2)
        {
            #region
            switch (eventNum)
            {
                case 8:
                    //アタマッコの新しい装備を手に入れる
                    Debug.Log("終了イベント8");
                    break;
                case 13:
                    Scene[1].SetActive(true);
                    NovelCanvas.SetActive(true);
                    yield return new WaitForSeconds(4f);
                    Comment.ChangeBackGroundImage(0);
                    NovelCanvas.SetActive(false);
                    break;
                default:
                    Debug.Log("何も設定されていない終了イベントです");
                    break;
            }
            #endregion
        }
        else if (scenario == scenarioType.scenarioBlue1)
        {
            #region
            switch (eventNum)
            {
                case 7:
                    Scene[0].SetActive(false);
                    Scene[1].SetActive(true);
                    Comment.currentChapter = 7;
                    NovelCanvas.SetActive(true);
                    Comment.ChangeBackGroundImage(1);
                    Comment.nullText();
                    Comment.onAnimation = true;
                    yield return new WaitForSeconds(2f);
                    Comment.ChangeBackGroundImage(2);
                    yield return new WaitForSeconds(2f);
                    Comment.nextText();
                    Comment.onAnimation = false;
                    Debug.Log("終了イベント7");
                    break;
                case 14:
                    //教導の使徒と戦闘
                    break;
                case 30:
                    //ボス戦闘
                    break;

                default:
                    break;
            }
            #endregion
        }
        else if (scenario == scenarioType.scenarioBlue2)
        {
            #region
            switch (eventNum)
            {
                case 7:
                    Scene[0].SetActive(false);
                    Scene[1].SetActive(true);
                    Comment.currentChapter = 7;
                    NovelCanvas.SetActive(true);
                    Comment.ChangeBackGroundImage(1);
                    Comment.nullText();
                    Comment.onAnimation = true;
                    yield return new WaitForSeconds(2f);
                    Comment.ChangeBackGroundImage(2);
                    yield return new WaitForSeconds(2f);
                    Comment.nextText();
                    Comment.onAnimation = false;
                    Debug.Log("終了イベント7");
                    break;
                case 14:
                    Scene[1].SetActive(false);
                    Scene[2].SetActive(true);
                    Comment.currentChapter = 14;
                    NovelCanvas.SetActive(true);
                    Comment.ChangeBackGroundImage(1);
                    Comment.nullText();
                    Comment.onAnimation = true;
                    yield return new WaitForSeconds(2f);
                    Comment.ChangeBackGroundImage(2);
                    yield return new WaitForSeconds(2f);
                    Comment.nextText();
                    Comment.onAnimation = false;
                    Debug.Log("終了イベント14");
                    break;
                case 15:
                    //少年と戦闘
                    break;
                case 25:
                    //ラスボス戦
                    break;

                default:
                    break;
            }
            #endregion
        }
        else if (scenario == scenarioType.scenarioBlue3)
        {
            #region
            switch (eventNum)
            {
                case 12:
                    Scene[0].SetActive(false);
                    Scene[1].SetActive(true);
                    Comment.currentChapter = 12;
                    NovelCanvas.SetActive(true);
                    Comment.ChangeBackGroundImage(1);
                    Comment.nullText();
                    Comment.onAnimation = true;
                    yield return new WaitForSeconds(2f);
                    Comment.ChangeBackGroundImage(2);
                    yield return new WaitForSeconds(2f);
                    Comment.nextText();
                    Comment.onAnimation = false;
                    Debug.Log("終了イベント7");
                    break;
                case 20:
                    //ボス戦
                    break;
                case 22:
                    //ボス戦
                    break;
                case 23:
                    Comment.currentChapter = 21;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;

                default:
                    break;
            }
            #endregion
        }
        else if (scenario == scenarioType.scenarioRed1)
        {
            #region
            switch (eventNum)
            {
                case 8:
                    Scene[0].SetActive(false);
                    Scene[1].SetActive(true);
                    Comment.currentChapter = 8;
                    NovelCanvas.SetActive(true);
                    Comment.ChangeBackGroundImage(1);
                    Comment.nullText();
                    Comment.onAnimation = true;
                    yield return new WaitForSeconds(2f);
                    Comment.ChangeBackGroundImage(2);
                    yield return new WaitForSeconds(2f);
                    Comment.nextText();
                    Comment.onAnimation = false;
                    Debug.Log("終了イベント9");
                    break;
                case 14:
                    //小鹿を襲う魔物と戦闘
                    break;
                case 19:
                    Scene[1].SetActive(false);
                    Scene[2].SetActive(true);
                    Comment.currentChapter = 19;
                    NovelCanvas.SetActive(true);
                    Comment.ChangeBackGroundImage(1);
                    Comment.nullText();
                    Comment.onAnimation = true;
                    yield return new WaitForSeconds(2f);
                    Comment.ChangeBackGroundImage(2);
                    yield return new WaitForSeconds(2f);
                    Comment.nextText();
                    Comment.onAnimation = false;
                    Debug.Log("終了イベント9");
                    break;


                default:
                    break;
            }
            #endregion
        }

    }
}
