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
        /// ストーリーモードでプレイヤーの進捗状況の管理を行う
        /// </summary>
        private static bool Init = false;
        private static bool[] Progressed = new bool[100];
        private static int LastProgressed = 1;
        private static int PlayerLastProgressed = 1;

        public static bool BattleDefeated = false;
        StoryBoardEvent eventSystem;

        public int GetPlayerLastProgressed { get => PlayerLastProgressed; }
        public bool[] GetProgressed { get => Progressed; }

        void Awake()
        {

            eventSystem = FindObjectOfType<StoryBoardEvent>().GetComponent<StoryBoardEvent>();
            if (PlayerConfig.LastPlayStory != SceneManager.GetActiveScene().name)
            {
                setResetProgressed();//前回遊んでいたストーリーシーンと違う場合は数値を初期化する。
            }
            else
            {
                defeatedCheck();
                TileColorGray();
                Debug.Log("ProgressRecorderのデータを初期化は行いませんでした");
            }
            //if (!Init) setResetProgressed();//初期化がされていない場合は初期化する
        }

        public void setResetProgressed()//各データの初期化
        {
            Debug.Log("ProgressRecorderのデータを初期化しました");
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
            if(BattleDefeated)          //戦闘に敗北した場合
            {
                Debug.Log("前回の戦闘で負けていました");
                Progressed[PlayerLastProgressed] = false;
                PlayerLastProgressed = LastProgressed;
                BattleDefeated = false;
            }
            else                        //戦闘に勝利した場合
            {
                //eventSystem.endEvent(PlayerConfig.lastChapter + 1);
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
                        Object.GetComponent<Image>().color = Color.gray;
                    }
                }
            }
        }
    }
}