using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardEvent : MonoBehaviour
{
    [SerializeField]
    NovelComment Comment;
    [SerializeField]
    private GameObject NovelCanvas;
    private void Start()
    {
        Comment.currentChapter = 0;
    }
    public void Event(int eventNum)
    {
        switch (eventNum)
        {
            case 1:
                Debug.Log("tile1のイベントです");
                break;
            case 2:
                Debug.Log("tile2のイベントです");
                break;
            case 3:
                Debug.Log("tile3のイベントです");
                break;
            case 4:
                Debug.Log("tile4のイベントです");
                break;
            case 10:
                Comment.currentChapter = 1;
                NovelCanvas.SetActive(true);
                Comment.nextText();
                Debug.Log("ゴール");
                break;
            default:
                Debug.Log("何も設定されていないイベントです");
                break;
        }
    }
}
