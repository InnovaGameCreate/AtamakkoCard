using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class ProgressRecorder : MonoBehaviour
    {
        /// <summary>
        /// �X�g�[���[���[�h�Ńv���C���[�̐i���󋵂̊Ǘ����s��
        /// </summary>
        private static bool Init = false;
        private static bool[] Progressed = new bool[100];
        private static int LastProgressed;
        private static int PlayerLastProgressed;

        public static bool BattleDefeated = false;

        public int GetPlayerLastProgressed { get => PlayerLastProgressed; }
        public bool[] GetProgressed { get => Progressed; }

        void Awake()
        {
            if (PlayerConfig.LastPlayStory != SceneManager.GetActiveScene().name)
            {
                setResetProgressed();//�O��V��ł����X�g�[���[�V�[���ƈႤ�ꍇ�͐��l������������B
            }
            else
            {
                defeatedCheck();
                TileColorGray();
                Debug.Log("ProgressRecorder�̃f�[�^���������͍s���܂���ł���");
            }
            //if (!Init) setResetProgressed();//������������Ă��Ȃ��ꍇ�͏���������
        }

        public void setResetProgressed()//�e�f�[�^�̏�����
        {
            Debug.Log("ProgressRecorder�̃f�[�^�����������܂���");
            LastProgressed = 1;
            PlayerLastProgressed = 1;
            for (int i = 0; i < Progressed.Length; i++)
            {
                Progressed[i] = false;
            }
        }

        public void completeEvent(int i)
        {
            LastProgressed = PlayerLastProgressed;
            Progressed[i] = true;
            RecordPlayerLastEvent(i);
        }

        public void RecordPlayerLastEvent(int i)
        {
            PlayerLastProgressed = i;
        }

        private void defeatedCheck()
        {
            if(BattleDefeated)
            {
                Debug.Log("�O��̐퓬�ŕ����Ă��܂���");
                Progressed[PlayerLastProgressed] = false;
                PlayerLastProgressed = LastProgressed;
                BattleDefeated = false;
            }
        }

        private void TileColorGray()
        {
            for (int i = 2; i <= PlayerLastProgressed; i++)
            {
                if(Progressed[i])
                {
                    string ObjectName = "tile (" + i + ")";
                    var Object = GameObject.Find(ObjectName);
                    Object.GetComponent<Image>().color = Color.gray;
                }
            }
        }
    }
}