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
    public void Event(int eventNum)
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
                    Scene[0].SetActive(false);
                    Scene[1].SetActive(true);
                    break;
                case 6:
                    Comment.currentChapter = 5;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 7:
                    Comment.currentChapter = 7;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 8:
                    Comment.currentChapter = 8;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 9:
                    Comment.currentChapter = 9;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 10:
                    //新しいカードの入手
                    Comment.currentChapter = 10;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 11:
                    Comment.currentChapter = 11;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 12:
                    Comment.currentChapter = 12;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
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
                    //ボス戦
                    Comment.currentChapter = 15;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                default:
                    Debug.Log("何も設定されていないイベントです");
                    break;
            }
        }
    }
    
}
