using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCheck : MonoBehaviour
{
    private bool EventYet = true;

    public void PlayerMove(int tileNum)
    {
        GameObject.FindObjectOfType<StoryBoardPlayerMove>().GetComponent<StoryBoardPlayerMove>().PlayerMove(tileNum);
    }

    public void EventUsed()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        EventYet = false;
    }

    public bool yet()
    {
        return EventYet;
    }
}
