using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        private static int PlayerLastProgressed;

        public int GetPlayerLastProgressed { get => PlayerLastProgressed; }
        public bool[] GetProgressed { get => Progressed; }

        void Awake()
        {
            if (PlayerConfig.LastPlayStory != SceneManager.GetActiveScene().name) setResetProgressed();//�O��V��ł����X�g�[���[�V�[���ƈႤ�ꍇ�͐��l������������B
            else Debug.Log("ProgressRecorder�̃f�[�^���������͍s���܂���ł���");
            //if (!Init) setResetProgressed();//������������Ă��Ȃ��ꍇ�͏���������
        }

        public void setResetProgressed()//�e�f�[�^�̏�����
        {
            Debug.Log("ProgressRecorder�̃f�[�^�����������܂���");
            PlayerLastProgressed = 1;
            for (int i = 0; i < Progressed.Length; i++)
            {
                Progressed[i] = false;
            }
        }

        public void completeEvent(int i)
        {
            Progressed[i] = true;
            RecordPlayerLastEvent(i);
        }

        public void RecordPlayerLastEvent(int i)
        {
            PlayerLastProgressed = i;
        }
    }
}