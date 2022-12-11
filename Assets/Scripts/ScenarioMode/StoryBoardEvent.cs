using System.Collections;
using UnityEngine;

namespace storyMode
{
    public class StoryBoardEvent : MonoBehaviour
    {
        /// <summary>
        /// ��������Ă���V�i���I��scenarioType�ŗp�ӂ���B�p�ӂ���Ă�����̈ȊO�̓C�x���g���N����Ȃ�
        /// </summary>
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
        private scenarioType scenario;//�ǂ�scenario�ɑΉ����邩
        [SerializeField]
        private GameObject[] Scene;//�V�i���I�ɓo�ꂷ��X�e�[�W
        [SerializeField]
        private GameObject NestStageCheckPanel;//���̃X�e�[�W�֐i�ނ��m�F�p�p�l��
        private void Start()
        {
            NestStageCheckPanel = FindObjectOfType<NestStagePanel>().gameObject;
            NestStageCheckPanel.SetActive(false);
            Comment.currentChapter = 0;
            Scene[1].SetActive(false);
        }

        //�^�C����I�������Ƃ��ɂ�����C�x���g
        public void startEvent(int eventNum)
        {
            if (scenario == scenarioType.scenario1)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        activeText(3);
                        break;
                    case 5:
                        activeText(4);
                        break;
                    case 6:
                        //5�̏I�����C�x���g�Ɉȍ~
                        break;
                    case 7:
                        activeText(6);
                        break;
                    case 8:
                        activeText(7);
                        break;
                    case 9:
                        //�퓬
                        activeText(8);
                        break;
                    case 10:
                        activeText(9);
                        break;
                    case 11:
                        activeText(10);
                        break;
                    case 12://�h���S���Ƃ̐퓬
                        Comment.currentChapter = 11;
                        break;
                    case 13:
                        activeText(12);
                        break;
                    case 14:
                        activeText(13);
                        break;
                    case 15:
                        activeText(14);
                        break;
                    case 16:
                        activeText(15);
                        break;
                    case 17://�G�Ƃ̐퓬
                        activeText(16);
                        break;
                    case 18:
                        activeText(17);
                        break;
                    case 19://�{�X��
                        activeText(18);
                        break;
                    case 21:
                        activeText(20);
                        break;
                    case 22:
                        activeText(21);
                        break;
                    case 23:
                        activeText(22);
                        break;
                    case 24://���Ԃƍ���
                        activeText(23);
                        break;
                    case 25://�퓬

                        break;
                    case 26:
                        activeText(25);
                        break;
                    case 27:
                        activeText(26);
                        break;
                    case 28://�{�X��
                        activeText(27);
                        break;
                    case 29:
                        activeText(28);
                        break;
                    case 30://�퓬
                        activeText(29);
                        break;
                    case 31:
                        activeText(30);
                        break;
                    case 32:
                        activeText(31);
                        break;
                    case 33:
                        activeText(32);
                        break;
                    case 34://�퓬
                        activeText(33);
                        break;
                    case 35:
                        activeText(34);
                        break;
                    case 36://�퓬
                        break;
                    default:
                        Debug.Log("�����ݒ肳��Ă��Ȃ��J�n�C�x���g�ł�");
                        break;
                }
            }
            else if (scenario == scenarioType.scenario2)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        activeText(3);
                        break;
                    case 5:
                        activeText(4);
                        break;
                    case 6:
                        activeText(5);
                        break;
                    case 7:
                        activeText(6);
                        break;
                    case 8:
                        activeText(7);
                        break;
                    case 9:
                        activeText(8);
                        break;
                    case 10:
                        activeText(9);
                        break;
                    case 11:
                        activeText(10);
                        break;
                    case 12:
                        activeText(11);
                        break;
                    case 13:
                        activeText(12);
                        break;
                    case 14:
                        activeText(13);
                        break;
                    case 15:
                        //�퓬
                        break;
                    case 16:
                        activeText(14);
                        break;
                    case 17:
                        //�퓬
                        break;
                    case 18:
                        activeText(15);
                        break;
                    case 19:
                    //�{�X��
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue1)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        activeText(3);
                        break;
                    case 5:
                        activeText(4);
                        break;
                    case 6:
                        activeText(5);
                        break;
                    case 7:
                        activeText(6);
                        break;
                    case 8:
                        activeText(7);
                        break;
                    case 9:
                        activeText(8);
                        break;
                    case 10:
                        activeText(9);
                        break;
                    case 11:
                        activeText(10);
                        break;
                    case 12:
                        activeText(11);
                        break;
                    case 13:
                        activeText(12);
                        break;
                    case 14:
                        activeText(13);
                        break;
                    case 15:
                        activeText(14);
                        //�퓬
                        break;
                    case 16:
                        activeText(15);
                        break;
                    case 17:
                        activeText(16);
                        break;
                    case 18:
                        activeText(17);
                        break;
                    case 19:
                        activeText(18);
                        break;
                    case 20:
                        activeText(19);
                        break;
                    case 21:
                        activeText(20);
                        break;
                    case 22:
                        activeText(21);
                        break;
                    case 23:
                        activeText(22);
                        break;
                    case 24:
                        break;
                    case 25:
                        activeText(23);
                        break;
                    case 26:
                        activeText(24);
                        break;
                    case 27:
                        //�퓬
                        break;
                    case 28:
                        //�퓬
                        break;
                    case 29:
                        activeText(25);
                        break;
                    case 30:
                        activeText(26);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue2)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        activeText(3);
                        break;
                    case 5:
                        activeText(4);
                        break;
                    case 6:
                        activeText(5);
                        break;
                    case 7:
                        activeText(6);
                        break;
                    case 8:
                        activeText(7);
                        break;
                    case 9:
                        activeText(8);
                        break;
                    case 10:
                        activeText(9);
                        break;
                    case 11:
                        activeText(10);
                        break;
                    case 12:
                        activeText(11);
                        break;
                    case 13:
                        activeText(12);
                        break;
                    case 14:
                        activeText(13);
                        break;
                    case 15:
                        activeText(14);
                        break;
                    case 16:
                        activeText(15);
                        break;
                    case 17:
                        activeText(16);
                        break;
                    case 18:
                        activeText(17);
                        break;
                    case 19:
                        activeText(18);
                        break;
                    case 20:
                        activeText(19);
                        break;
                    case 21:
                        //�퓬
                        break;
                    case 22:
                        activeText(20);
                        break;
                    case 23:
                        //�퓬
                        break;
                    case 24:
                        activeText(21);
                        break;
                    case 25:
                        activeText(22);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue3)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        activeText(3);
                        break;
                    case 5:
                        //�퓬
                        break;
                    case 6:
                        activeText(4);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        activeText(6);
                        break;
                    case 9:
                        activeText(7);
                        break;
                    case 10:
                        activeText(8);
                        break;
                    case 11:
                        activeText(9);
                        break;
                    case 12:
                        activeText(10);
                        break;
                    case 13:
                        activeText(11);
                        break;
                    case 14:
                        activeText(12);
                        break;
                    case 15:
                        activeText(13);
                        break;
                    case 16:
                        //�퓬
                        break;
                    case 17:
                        activeText(14);
                        break;
                    case 18:
                        activeText(15);
                        break;
                    case 19:
                        activeText(16);
                        break;
                    case 20:
                        activeText(17);
                        break;
                    case 21:
                        activeText(18);
                        break;
                    case 22:
                        activeText(19);
                        break;
                    case 23:
                        activeText(20);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioRed1)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        activeText(3);
                        break;
                    case 5:
                        //�쐶�����Ɛ퓬
                        break;
                    case 6:
                        activeText(4);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        //�����Ƃ̐퓬
                        break;
                    case 9:
                        activeText(6);
                        break;
                    case 10:
                        activeText(7);
                        break;
                    case 11:
                        activeText(8);
                        break;
                    case 12:
                        activeText(9);
                        break;
                    case 13:
                        activeText(10);
                        break;
                    case 14:
                        activeText(11);
                        break;
                    case 15:
                        activeText(12);
                        break;
                    case 16:
                        activeText(13);
                        break;
                    case 17:
                        //�����Ƃ̐퓬
                        break;
                    case 18:
                        activeText(14);
                        break;
                    case 19:
                        //�{�X��
                        StartCoroutine(changeStage(1, 19));//�퓬���I���ƕ\������
                        break;
                    case 20:
                        activeText(15);
                        break;
                    case 21:
                        activeText(16);
                        break;
                    case 22:
                        activeText(17);
                        break;
                    case 23:
                        activeText(18);
                        break;
                    case 24:
                        //�א_�̉�����󂯂������Ƃ̐퓬
                        break;
                    case 25:
                        activeText(19);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioRed2)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        //�����Ɛ퓬
                        break;
                    case 5:
                        activeText(3);
                        break;
                    case 6:
                        activeText(4);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        activeText(6);
                        break;
                    case 9:
                        activeText(7);
                        break;
                    case 10:
                        activeText(8);
                        break;
                    case 11:
                        activeText(9);
                        break;
                    case 12:
                    //�����Ƃ̐퓬
                    case 13:
                        activeText(10);
                        break;
                    case 14:
                        activeText(11);
                        break;
                    case 15:
                        activeText(12);
                        break;
                    case 16:
                        //�]���������Ƃ̐퓬
                        break;
                    case 17:
                        activeText(13);
                        break;
                    case 18:
                        activeText(14);
                        break;
                    case 19:
                        activeText(15);
                        break;
                    case 20:
                        break;
                    case 21:
                        activeText(17);
                        break;
                    case 22:
                        activeText(18);
                        break;
                    case 23:
                        //�����ǂ��ł閂���Ƃ̐퓬
                        break;
                    case 24:
                        activeText(19);
                        break;
                    case 25:
                        activeText(20);
                        break;
                    case 26:
                        activeText(21);
                        break;
                    case 27:
                        //�傫�Ȗ����Ƃ̐퓬
                        break;
                    case 28:
                        activeText(22);
                        break;
                    case 29:
                        //���[�u�̒j�Ƃ̐퓬
                        break;
                    case 30:
                        //�ዉ�̎א_�Ƃ̐퓬

                        NestStageCheckPanel.SetActive(true);//�퓬���I���ƕ\������
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioRed3)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        //�����Ƃ̐퓬
                        break;
                    case 5:
                        activeText(3);
                        break;
                    case 6:
                        activeText(4);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        activeText(6);
                        break;
                    case 9:
                        //���X�̎g�k�Ɩ͋[��
                        break;
                    case 10:
                        activeText(7);
                        break;
                    case 11:
                        activeText(8);
                        break;
                    case 12:
                        activeText(9);
                        break;
                    case 13:
                        //�΂̖����Ƃ̐퓬
                        break;
                    case 14:
                        activeText(10);
                        break;
                    case 15:
                        activeText(11);
                        break;
                    case 16:
                        activeText(12);
                        break;
                    case 17:
                        break;
                    case 18:
                        activeText(14);
                        break;
                    case 19:
                        activeText(15);
                        break;
                    case 20:
                        activeText(16);
                        break;
                    case 21:
                        //�����Ƃ̐퓬
                        break;
                    case 22:
                        activeText(17);
                        break;
                    case 23:
                        activeText(18);
                        break;
                    case 24:
                        //�����Ƃ̐퓬
                        break;
                    case 25:
                        activeText(19);
                        break;
                    case 26:
                        activeText(20);
                        break;
                    case 27:
                        activeText(21);
                        break;
                    case 28:
                        //�א_�Ƃ̐퓬
                        break;
                    case 29:
                        activeText(22);
                        break;
                    case 30:
                        //���S�����X�̎g�k�Ƃ̐퓬
                        NestStageCheckPanel.SetActive(true);//�퓬���I���ƕ\������
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite1)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        //�����Ƃ̐퓬
                        break;
                    case 3:
                        activeText(1);
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        //�����Ƃ̐퓬
                        break;
                    case 6:
                        activeText(3);
                        break;
                    case 7:
                        activeText(4);
                        break;
                    case 8:
                        activeText(5);
                        break;
                    case 10:
                        activeText(7);
                        break;
                    case 11:
                        activeText(8);
                        break;
                    case 12:
                        activeText(9);
                        break;
                    case 13:
                        activeText(10);
                        break;
                    case 14:
                        //�����Ƃ̐퓬
                        break;
                    case 15:
                        activeText(11);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite2)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        //�����Ƃ̐퓬
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        activeText(3);
                        break;
                    case 6:
                        //�����Ƃ̐퓬
                        break;
                    case 7:
                        activeText(4);
                        break;
                    case 8:
                        activeText(5);
                        break;
                    case 9:
                        activeText(6);
                        break;
                    case 10:
                        activeText(7);
                        break;
                    case 12:
                        activeText(9);
                        break;
                    case 13:
                        activeText(10);
                        break;
                    case 14:
                        activeText(11);
                        break;
                    case 15:
                        activeText(12);
                        break;
                    case 16:
                        activeText(13);
                        break;
                    case 17:
                        activeText(14);
                        break;
                    case 18:
                        //��j�Ƃ̐퓬�A��������ƕ�V
                        break;
                    case 19:
                        activeText(15);
                        break;
                    case 21:
                        activeText(17);
                        break;
                    case 22:
                        //�����Ƃ̐퓬
                        break;
                    case 23:
                        activeText(18);
                        break;
                    case 24:
                        activeText(19);
                        break;
                    case 25:
                        activeText(20);
                        break;
                    case 26:
                        activeText(21);
                        break;
                    case 27:
                        activeText(22);
                        break;
                    case 28:
                        activeText(23);
                        break;
                    case 29:
                        activeText(24);
                        break;
                    case 30:
                        //���m�Ɛ퓬
                        break;
                    case 31:
                        activeText(25);
                        break;
                    case 32:
                        activeText(26);
                        break;
                    case 33:
                        //�����̖����Ɛ퓬
                        break;
                    case 34:
                        activeText(27);
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite2)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
                    case 2:
                        activeText(1);
                        break;
                    case 3:
                        activeText(2);
                        break;
                    case 4:
                        activeText(3);
                        break;
                    case 5:
                        activeText(4);
                        break;
                    case 6:
                        activeText(5);
                        break;
                    case 7:
                        activeText(6);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// �C�x���g�̑Ή�����ԍ��������Ɏ����Ă��āA��b�p�e�L�X�g�ɐݒ肵����
        /// ��b�p��canvas��\�����ĉ�b���n�߂�B
        /// </summary>
        private void activeText(int eventTextNumber)
        {
            Comment.currentChapter = eventTextNumber;
            NovelCanvas.SetActive(true);
            Comment.nextText();
        }
        //��b�I�����ɋN����C�x���g
        public void endEvent(int eventNum)
        {
            StartCoroutine(talkEndEvent(eventNum));
        }
        /// <summary>
        /// eventNum�̉�b���I�������ɓ��삷��B
        /// </summary>
        /// <param name="eventNum"></param>
        IEnumerator changeStage(int currentStageNum, int currentChapter)
        {
            Scene[currentStageNum].SetActive(false);
            Scene[currentStageNum + 1].SetActive(true);
            Comment.currentChapter = currentChapter;
            NovelCanvas.SetActive(true);
            Comment.ChangeBackGroundImage(1);
            Comment.nullText();
            Comment.onAnimation = true;
            yield return new WaitForSeconds(2f);
            Comment.ChangeBackGroundImage(2);
            yield return new WaitForSeconds(2f);
            Comment.nextText();
            Comment.onAnimation = false;
            Debug.Log("�I���C�x���g�F" + currentStageNum);
        }
        IEnumerator talkEndEvent(int eventNum)
        {
            yield return new WaitForFixedUpdate();
            if (scenario == scenarioType.scenario1)
            {
                switch (eventNum)
                {
                    case 5:
                        StartCoroutine(changeStage(0, 5));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 17:
                        Comment.ChangeBackGroundImage(0);
                        //�퓬�J�n�̐M���𑗂�
                        Debug.Log("�I���C�x���g17");
                        break;
                    case 19:
                        Comment.ChangeBackGroundImage(0);
                        //�퓬�J�n�̐M���𑗂�A�퓬�㎟�̃X�e�[�W��
                        Debug.Log("�I���C�x���g19");
                        break;
                    case 28:
                        Comment.ChangeBackGroundImage(0);
                        //�퓬�J�n�̐M���𑗂�
                        Debug.Log("�I���C�x���g28");
                        break;
                    case 39:
                        NestStageCheckPanel.SetActive(true);
                        break;
                    default:
                        Debug.Log("�����ݒ肳��Ă��Ȃ��I���C�x���g�ł�");
                        break;
                }
            }
            else if (scenario == scenarioType.scenario2)
            {
                switch (eventNum)
                {
                    case 8:
                        //�A�^�}�b�R�̐V������������ɓ����
                        Debug.Log("�I���C�x���g8");
                        break;
                    case 13:
                        Scene[1].SetActive(true);
                        NovelCanvas.SetActive(true);
                        yield return new WaitForSeconds(4f);
                        Comment.ChangeBackGroundImage(0);
                        NovelCanvas.SetActive(false);
                        break;
                    default:
                        Debug.Log("�����ݒ肳��Ă��Ȃ��I���C�x���g�ł�");
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue1)
            {
                switch (eventNum)
                {
                    case 7:
                        StartCoroutine(changeStage(0, 7));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 14:
                        //�����̎g�k�Ɛ퓬
                        break;
                    case 26:
                        //�{�X�퓬
                        NestStageCheckPanel.SetActive(true);
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue2)
            {
                switch (eventNum)
                {
                    case 7:
                        StartCoroutine(changeStage(0, 7));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 14:
                        StartCoroutine(changeStage(1, 14));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 15:
                        //���N�Ɛ퓬
                        break;
                    case 22:
                        //���X�{�X��
                        NestStageCheckPanel.SetActive(true);
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue3)
            {
                switch (eventNum)
                {
                    case 12:
                        StartCoroutine(changeStage(0, 12));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 17:
                        //�{�X��
                        break;
                    case 19:
                        //�{�X��
                        break;
                    case 20:
                        activeText(21);
                        NestStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioRed1)
            {
                switch (eventNum)
                {
                    case 8:
                        StartCoroutine(changeStage(0, 8));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 14:
                        //�������P�������Ɛ퓬
                        break;
                    case 19:
                        NestStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioRed2)
            {
                switch (eventNum)
                {
                    case 6:
                        StartCoroutine(changeStage(0, 6));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 15:
                        StartCoroutine(changeStage(1, 15));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioRed3)
            {
                switch (eventNum)
                {
                    case 7:
                        StartCoroutine(changeStage(0, 7));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 13:
                        StartCoroutine(changeStage(1, 13));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite1)
            {
                switch (eventNum)
                {
                    case 8:
                        //�����̖����Ƃ̐퓬
                        StartCoroutine(changeStage(0, 8));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 11:
                        NestStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite2)
            {
                switch (eventNum)
                {
                    case 8:
                        StartCoroutine(changeStage(0, 8));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 16:
                        StartCoroutine(changeStage(1, 16));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    default:
                        break;
                }
            }
        }
    }
}