using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardEvent : MonoBehaviour
{
    private bool[] eventCheck;
    public void Event(int eventNum)
    {
        switch (eventNum)
        {
            case 1:
                Debug.Log("tile1�̃C�x���g�ł�");
                break;
            case 2:
                Debug.Log("tile2�̃C�x���g�ł�");
                break;
            case 3:
                Debug.Log("tile3�̃C�x���g�ł�");
                break;
            case 4:
                Debug.Log("tile4�̃C�x���g�ł�");
                break;
            case 10:
                Debug.Log("�S�[��");
                break;
            default:
                Debug.Log("�����ݒ肳��Ă��Ȃ��C�x���g�ł�");
                break;
        }
    }
}
