using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
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
        private static int LastProgressed = 1;
        private static int PlayerLastProgressed = 1;

        public static bool BattleDefeated = false;
        StoryBoardEvent eventSystem;
        StoryBoardPlayerMove playerMove;

        public int GetPlayerLastProgressed { get => PlayerLastProgressed; }
        public bool[] GetProgressed { get => Progressed; }

        void Awake()
        {
            eventSystem = FindObjectOfType<StoryBoardEvent>().GetComponent<StoryBoardEvent>();
            playerMove = FindObjectOfType<StoryBoardPlayerMove>().GetComponent<StoryBoardPlayerMove>();
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

            playerMove.PlayerProgress
                .Where(PlayerProgress => !Progressed[PlayerProgress])
                .Subscribe(PlayerProgress =>
                {
                    LastProgressed = PlayerLastProgressed;
                    Progressed[PlayerProgress] = true;
                    PlayerLastProgressed = PlayerProgress;
                });


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

        private void defeatedCheck()
        {
            if(BattleDefeated)          //�퓬�ɔs�k�����ꍇ
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
                    if(Object != null)
                    {
                        Object.GetComponent<EventCheck>().Used();
                    }
                }
            }
        }
    }
}