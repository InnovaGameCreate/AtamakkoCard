using System.Collections;
using UnityEngine;
using Arena;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class StoryBoardEvent : MonoBehaviour
    {
        /// <summary>
        /// ��������Ă���V�i���I��scenarioType�ŗp�ӂ���B�p�ӂ���Ă�����̈ȊO�̓C�x���g���N����Ȃ�
        /// </summary>
        private enum scenarioType//�V�i���I�̊e��
        {
            scenario1,
            scenario2,
            scenarioSelect,
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
        [SerializeField] NovelComment Comment;
        [SerializeField] private GameObject NovelCanvas;
        [SerializeField] private scenarioType scenario;//�ǂ̃V�i���I�̏͂ɑΉ����邩
        [SerializeField] private GameObject[] Scene;//�V�i���I�ɓo�ꂷ��X�e�[�W
        [SerializeField] private GameObject NextStageCheckPanel;//���̃X�e�[�W�֐i�ނ��m�F�p�p�l��
        private void Start()
        {
            NextStageCheckPanel.SetActive(false);
            Comment.currentChapter = 0;//��bchapter��0�ɏ�����
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
                        //�W���C�A���g�}���e�B�X�Ƃ̐퓬
                        encountEnemy(23);
                        break;
                    case 10:
                        activeText(8);
                        break;
                    case 11:
                        activeText(9);
                        //��b�I����dragon�Ƃ̐퓬
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
                        activeText(14);
                        break;
                    case 17:
                        activeText(15);
                        break;
                    case 18:
                        //�퓬�S�Ƃ̐퓬
                        encountEnemy(11);
                        break;
                    case 19:
                        activeText(16);
                        break;
                    case 20:
                        encountEnemy(11);
                        //�{�X��
                        break;
                    case 21:
                        activeText(17);
                        break;
                    case 23:
                        activeText(19);
                        break;
                    case 24:
                        activeText(20);
                        break;
                    case 25:
                        activeText(21);
                        break;
                    case 26://���Ԃƍ���
                        activeText(22);
                        break;
                    case 27:
                        //�L�}�C���Ƃ̐퓬
                        encountEnemy(24);
                        break;
                    case 28:
                        activeText(23);
                        break;
                    case 29:
                        activeText(24);
                        break;
                    case 30:
                        activeText(25);
                        break;
                    case 31:
                        //�{�X��:monster05
                        encountEnemy(25);
                        break;
                    case 32:
                        activeText(32);
                        break;
                    case 33:
                        activeText(26);
                        break;
                    case 34:
                        //�育��Ȗ����Ƃ̐퓬
                        encountEnemy(26);
                        break;
                    case 35:
                        activeText(27);
                        break;
                    case 36:
                        activeText(28);
                        break;
                    case 37:
                        activeText(29);
                        break;
                    case 38:
                        activeText(30);
                        break;
                    case 39:
                        activeText(31);
                        break;
                    case 40:
                        //�c�̃h���S���Ƃ̐퓬
                        encountEnemy(27);
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
                        //�퓬:monster07
                        encountEnemy(28);
                        break;
                    case 16:
                        activeText(14);
                        break;
                    case 17:
                        //�퓬:monster12
                        encountEnemy(30);
                        break;
                    case 18:
                        activeText(15);
                        break;
                    case 19:
                        //�{�X��:monster13
                        encountEnemy(33);
                        break;
                    case 20:
                        activeText(16);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioSelect)
            {
                switch (eventNum)
                {
                    case 1:
                        activeText(0);
                        break;
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
                        //��b�I����ɐ퓬
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
                        //�����̖����Ƃ̐퓬
                        encountEnemy(41);
                        break;
                    case 28:
                        //�����̖����Ƃ̐퓬
                        encountEnemy(42);
                        break;
                    case 29:
                        activeText(25);
                        break;
                    case 30:
                        activeText(26);
                        break;
                    case 31:
                        //���_�Ƃ̐퓬
                        encountEnemy(43);
                        break;
                    case 32:
                        activeText(27);
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
                        //�׋��c�̐M�҂Ƃ̐퓬
                        encountEnemy(44);
                        break;
                    case 22:
                        activeText(20);
                        break;
                    case 23:
                        //�׋��c�̐M�҂Ƃ̐퓬
                        encountEnemy(45);
                        break;
                    case 24:
                        activeText(21);
                        break;
                    case 25:
                        activeText(22);
                        break;
                    case 26:
                        //�_�̏]�҂Ƃ̐퓬
                        encountEnemy(47);
                        break;
                    case 27:
                        activeText(23);
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
                        //�����ǂ������Ƃ̐퓬
                        encountEnemy(48);
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
                        //�����ɏP����퓬
                        encountEnemy(49);
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
                        encountEnemy(51);
                        break;
                    case 6:
                        activeText(4);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        //�����Ƃ̐퓬
                        encountEnemy(50);
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
                        encountEnemy(52);
                        break;
                    case 18:
                        activeText(14);
                        break;
                    case 19:
                        //�{�X��
                        encountEnemy(53);

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
                        encountEnemy(54);
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
                        encountEnemy(56);
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
                        encountEnemy(57);
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
                        //�]���������Ƃ̐퓬
                        encountEnemy(58);
                        break;
                    case 17:
                        activeText(13);
                        //�A�^�}�b�R�̑�������ɓ����
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
                        encountEnemy(59);
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

                        NextStageCheckPanel.SetActive(true);//�퓬���I���ƕ\������
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
                        NextStageCheckPanel.SetActive(true);//�퓬���I���ƕ\������
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
                        encountEnemy(29);
                        break;
                    case 3:
                        activeText(1);
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        //�����Ƃ̐퓬
                        encountEnemy(31);
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
                        encountEnemy(38);
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
                        encountEnemy(12);
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        activeText(3);
                        break;
                    case 6:
                        //�����Ƃ̐퓬
                        encountEnemy(13);
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
                        encountEnemy(34);
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
                        encountEnemy(36);
                        break;
                    case 31:
                        activeText(25);
                        break;
                    case 32:
                        activeText(26);
                        break;
                    case 33:
                        //�����̖����Ɛ퓬
                        encountEnemy(39);
                        break;
                    case 34:
                        activeText(27);
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite3)
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
                        activeText(8);//���̏͂�
                        break;
                    case 10:
                        activeText(9);
                        break;
                    case 11:
                        activeText(10);//�A�^�}�b�R�̑�������ɓ����B
                        break;
                    case 13:
                        activeText(12);
                        break;
                    case 14:
                        //�S�[�����Ƃ̐퓬
                        encountEnemy(32);
                        break;
                    case 15:
                        activeText(13);
                        break;
                    case 16:
                        activeText(14);
                        break;
                    case 17:
                        //�S�[�����Ƃ̐퓬
                        encountEnemy(35);
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
                        //�G�ɏP����
                        encountEnemy(37);
                        break;
                    case 22:
                        activeText(18);
                        break;
                    case 23:
                        activeText(19);
                        break;
                    case 24:
                        //�������Ƃ̐퓬
                        encountEnemy(40);
                        break;
                    case 25:
                        activeText(20);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioend1)
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
                        //�G�Ƃ̐퓬
                        break;
                    case 7:
                        //�G�Ƃ̐퓬
                        break;
                    case 8:
                        activeText(5);
                        break;
                    case 9:
                        //�G�Ƃ̐퓬
                        break;
                    case 10:
                        activeText(6);
                        break;
                    case 11:
                        //�G�Ƃ̐퓬
                        break;
                    case 12:
                        //�{�X�Ƃ̐퓬
                        break;
                    case 13:
                        activeText(7);
                        break;
                    case 15:
                        activeText(9);
                        break;
                    case 16:
                        activeText(10);
                        break;
                    case 17:
                        activeText(11);
                        break;
                    case 18:
                        activeText(12);
                        break;
                    case 19:
                        activeText(13);
                        break;
                    case 21:
                        activeText(15);
                        break;
                    case 22:
                        //�����Ƃ̐퓬
                        break;
                    case 23:
                        activeText(16);
                        break;
                    case 24:
                        activeText(17);
                        break;
                    case 25:
                        //���X�{�X�Ƃ̐퓬
                        break;
                    case 26:
                        activeText(18);
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioend2)
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
                        //�G�Ƃ̐퓬
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        activeText(6);
                        break;
                    case 9:
                        //�G�Ƃ̐퓬
                        break;
                    case 10:
                        activeText(7);
                        break;
                    case 11:
                        activeText(8);
                        break;
                    case 12:
                        //�N���̎g�k�Ƃ̐퓬
                        break;
                    case 13:
                        activeText(9);
                        break;
                    case 15:
                        activeText(11);
                        break;
                    case 16:
                        activeText(12);
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
                        activeText(16);
                        break;
                    case 21:
                        //�����Ƃ̐퓬
                        break;
                    case 22:
                        activeText(17);
                        break;
                    case 23:
                        //�����Ƃ̐퓬
                        break;
                    case 24:
                        //�����Ƃ̐퓬
                        break;
                    case 25:
                        activeText(18);
                        break;
                    case 27:
                        //���̖�ԂƂ̐퓬
                        break;
                    case 28:
                        activeText(20);
                        break;


                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioend3)
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
                        //�퓬
                        break;
                    case 10:
                        activeText(8);
                        break;
                    case 11:
                        //�퓬
                        break;
                    case 12:
                        activeText(9);
                        break;
                    case 13:
                        activeText(10);
                        break;
                    case 14:
                        //�퓬
                        break;
                    case 15:
                        activeText(11);
                        break;
                    case 16:
                        //�N���̎g�k�Ɛ퓬
                        break;
                    case 17:
                        activeText(12);
                        break;
                    case 18:
                        //�ŌẪh���S���Ɛ퓬
                        break;
                    case 19:
                        activeText(13);
                        break;
                    case 21:
                        activeText(15);
                        break;
                    case 22:
                        activeText(16);
                        break;
                    case 23:
                        activeText(17);
                        break;
                    case 24:
                        activeText(18);
                        break;
                    case 25:
                        activeText(19);
                        break;
                    case 26:
                        //�퓬
                        break;
                    case 27:
                        //�퓬
                        break;
                    case 28:
                        activeText(20);
                        break;
                    case 29:
                        //�퓬
                        break;
                    case 30:
                        //�퓬
                        break;
                    case 31:
                        activeText(21);
                        break;
                    case 32:
                        //�퓬
                        break;
                    case 33:
                        activeText(22);
                        break;
                    case 34:
                        //�퓬
                        break;
                    case 35:
                        activeText(23);
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
        /// �X�e�[�W��ύX����ۂ̊֐�
        /// </summary>
        IEnumerator changeStage(int currentStageNum, int currentChapter)//���݂̃X�e�[�W�ԍ���chapter�������Ɏ����Ă���
        {
            Scene[currentStageNum].SetActive(false);//���݂̃X�e�[�W���\���ɂ���
            Scene[currentStageNum + 1].SetActive(true);//���̃X�e�[�W��\������
            Comment.currentChapter = currentChapter;
            NovelCanvas.SetActive(true);
            Comment.ChangeBackGroundImage(1);
            Comment.deleteText();
            Comment.onAnimation = true;
            FindObjectOfType<StoryBoardPlayerMove>().nextPosition(Scene[currentStageNum + 1].transform.GetChild(1).position);
            yield return new WaitForSeconds(2f);
            Comment.ChangeBackGroundImage(2);
            yield return new WaitForSeconds(2f);
            Comment.nextText();
            Comment.onAnimation = false;
            Debug.Log("�I���C�x���g�F" + currentStageNum);
        }
        /// <summary>
        /// �G�Ƒ��������ۂɁA�G�l�~�[�̃f�[�^�������Ă��Đ퓬�V�[���Ɉړ�����
        /// </summary>
        private void encountEnemy(int EnemyID)
        {
            enemyDeckData.setDeckData(EnemyID);
            SceneManager.LoadScene("BattleCPU");
        }
        /// <summary>
        /// eventNum�̉�b���I�������ɓ��삷��B
        /// </summary>
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
                    case 10:
                        //�h���S���Ƃ̐퓬:monster02
                        encountEnemy(10);
                        break;
                    case 18:
                        StartCoroutine(changeStage(1, 18));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[
                        break;
                    case 28:
                        NextStageCheckPanel.SetActive(true);
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
                    case 17:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        Debug.Log("�����ݒ肳��Ă��Ȃ��I���C�x���g�ł�");
                        break;
                }
            }
            if (scenario == scenarioType.scenarioSelect)
            {
                switch (eventNum)
                {
                    case 1:
                        NextStageCheckPanel.SetActive(true);
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
                    case 16:
                        //�����̎g�k�Ɛ퓬
                        break;
                    case 15:
                        //������Ɛ퓬
                        //encountEnemy(23);
                        break;
                    case 26:
                        //�{�X�퓬
                        NextStageCheckPanel.SetActive(true);
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
                        encountEnemy(46);
                        break;
                    case 24:
                        NextStageCheckPanel.SetActive(true);
                        break;

                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue3)
            {
                switch (eventNum)
                {
                    case 10:
                        //�썑�̎g�k�Ƃ̐퓬
                        break;
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
                        NextStageCheckPanel.SetActive(true);
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
                        NextStageCheckPanel.SetActive(true);
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
                        NextStageCheckPanel.SetActive(true);
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
                    case 28:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite3)
            {
                switch (eventNum)
                {
                    case 9:
                        StartCoroutine(changeStage(0, 9));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 21:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioend1)
            {
                switch (eventNum)
                {
                    case 8:
                        StartCoroutine(changeStage(0, 8));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 14:
                        StartCoroutine(changeStage(1, 14));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 19:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioend2)
            {
                switch (eventNum)
                {
                    case 10:
                        StartCoroutine(changeStage(0, 10));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 19:
                        StartCoroutine(changeStage(1, 19));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 21:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioend3)
            {
                switch (eventNum)
                {
                    case 14:
                        StartCoroutine(changeStage(0, 14));//���݂̃X�e�[�W���F���݂̃`���v�^�[�𑗂��Ď��̃X�e�[�W�ֈڂ�
                        break;
                    case 24:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}