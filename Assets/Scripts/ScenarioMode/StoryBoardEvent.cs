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

    //�^�C����I�������Ƃ��ɂ�����C�x���g
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
                    //5�̏I�����C�x���g�Ɉȍ~
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
                    //�퓬
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
                case 12://�h���S���Ƃ̐퓬
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
                case 17://�G�Ƃ̐퓬
                    Comment.currentChapter = 16;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 18:
                    Comment.currentChapter = 17;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 19://�{�X��
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
                case 24://���Ԃƍ���
                    Comment.currentChapter = 23;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 25://�퓬

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
                case 28://�{�X��
                    Comment.currentChapter = 27;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 29:
                    Comment.currentChapter = 28;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 30://�퓬
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
                case 34://�퓬
                    Comment.currentChapter = 33;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 35:
                    Comment.currentChapter = 34;
                    NovelCanvas.SetActive(true);
                    Comment.nextText();
                    break;
                case 36://�퓬
                    break;
                default:
                    Debug.Log("�����ݒ肳��Ă��Ȃ��J�n�C�x���g�ł�");
                    break;
            }
        }
    }

    //��b�I�����ɋN����C�x���g
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
                    Debug.Log("�I���C�x���g5");
                    break;
                case 17:
                    Comment.ChangeBackGroundImage(0);
                    //�퓬�J�n�̐M���𑗂�
                    Debug.Log("�I���C�x���g17");
                    break;
                case 19:
                    Comment.ChangeBackGroundImage(0);
                    //�퓬�J�n�̐M���𑗂�
                    Debug.Log("�I���C�x���g19");
                    break;
                case 28:
                    Comment.ChangeBackGroundImage(0);
                    //�퓬�J�n�̐M���𑗂�
                    Debug.Log("�I���C�x���g28");
                    break;
                default:
                    Debug.Log("�����ݒ肳��Ă��Ȃ��I���C�x���g�ł�");
                    break;
            }
        }
    }
}
