using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class ProgressRecorder : MonoBehaviour
    {
        /// <summary>
        /// ストーリーモードでプレイヤーの進捗状況の管理を行う
        /// </summary>
        private static bool Init = false;
        private static bool[] Progressed = new bool[100];
        private static int PlayerLastProgressed;

        public int GetPlayerLastProgressed { get => PlayerLastProgressed; }
        public bool[] GetProgressed { get => Progressed; }

        void Awake()
        {
            if (PlayerConfig.LastPlayStory != SceneManager.GetActiveScene().name) setResetProgressed();//前回遊んでいたストーリーシーンと違う場合は数値を初期化する。
            else Debug.Log("ProgressRecorderのデータを初期化は行いませんでした");
            //if (!Init) setResetProgressed();//初期化がされていない場合は初期化する
        }

        public void setResetProgressed()//各データの初期化
        {
            Debug.Log("ProgressRecorderのデータを初期化しました");
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