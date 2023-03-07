using System.Collections;
using UnityEngine;
using Arena;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class StoryBoardEvent : MonoBehaviour
    {
        /// <summary>
        /// 実装されているシナリオをscenarioTypeで用意する。用意されているもの以外はイベントが起こらない
        /// </summary>
        private enum scenarioType//シナリオの各章
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
        [SerializeField] private scenarioType scenario;//どのシナリオの章に対応するか
        [SerializeField] private GameObject instantTextPrefab;//文字表示用のプレファブ
        [SerializeField] private GameObject[] Scene;//シナリオに登場するステージ
        [SerializeField] private GameObject NextStageCheckPanel;//次のステージへ進むか確認用パネル
        private void Start()
        {
            Comment.currentChapter = 0;//会話chapterを0に初期化
        }

        //タイルを選択したときにおこるイベント
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
                    case 6:
                        activeText(5);
                        break;
                    case 7:
                        activeText(6);
                        break;
                    case 8:
                        encountEnemy(23);
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
                        activeText(14);
                        break;
                    case 17:
                        encountEnemy(11);
                        break;
                    case 18:
                        activeText(15);
                        break;
                    case 19:
                        encountEnemy(24);
                        break;
                    case 20:
                        activeText(16);
                        break;
                    case 22:
                        activeText(18);
                        break;
                    case 23:
                        activeText(19);
                        break;
                    case 24:
                        activeText(21);
                        break;
                    case 25:
                        encountEnemy(24);
                        break;
                    case 26:
                        activeText(22);
                        break;
                    case 27:
                        activeText(23);
                        break;
                    case 28:
                        activeText(24);
                        break;
                    case 29:
                        encountEnemy(25);
                        break;
                    case 30:
                        activeText(25);
                        break;
                    case 31:
                        activeText(26);
                        break;
                    case 32:
                        encountEnemy(26);
                        break;
                    case 33:
                        activeText(27);
                        break;
                    case 34:
                        activeText(28);
                        break;
                    case 35:
                        activeText(29);
                        break;
                    case 36:
                        encountEnemy(27);
                        break;
                    default:
                        Debug.Log("何も設定されていない開始イベントです");
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
                        encountEnemy(28);
                        break;
                    case 15:
                        activeText(13);
                        break;
                    case 16:
                        encountEnemy(30);
                        break;
                    case 17:
                        activeText(14);
                        break;
                    case 18:
                        encountEnemy(33);
                        break;
                    case 19:
                        activeText(15);
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
                        activeText(16);
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
                        activeText(12);
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
                        activeText(23);
                        break;
                    case 25:
                        activeText(24);
                        break;
                    case 26:
                        encountEnemy(41);
                        break;
                    case 27:
                        encountEnemy(42);
                        break;
                    case 28:
                        activeText(25);
                        break;
                    case 29:
                        activeText(26);
                        break;
                    case 30:
                        encountEnemy(43);
                        break;
                    case 31:
                        activeText(27);
                        break;
                    case 32:
                        displayText("ストライク改のアクセサリを手に入れた", 1f, 1f);
                        PlayerConfig.unLockEquipment[53] = true;
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
                        activeText(15);
                        break;
                    case 16:
                        activeText(14);
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
                        encountEnemy(44);
                        break;
                    case 22:
                        activeText(20);
                        break;
                    case 23:
                        encountEnemy(45);
                        break;
                    case 24:
                        activeText(21);
                        break;
                    case 25:
                        activeText(22);
                        break;
                    case 26:
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
                        encountEnemy(8);
                        break;
                    case 22:
                        StartCoroutine(changeStage(1, 18));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                        encountEnemy(51);
                        break;
                    case 6:
                        activeText(4);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
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
                        encountEnemy(52);
                        break;
                    case 18:
                        activeText(14);
                        break;
                    case 19:
                        encountEnemy(53);
                        break;
                    case 20:
                        StartCoroutine(changeStage(1, 15));
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
                        encountEnemy(54);
                        break;
                    case 26:
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
                        encountEnemy(58);
                        break;
                    case 17:
                        activeText(13);
                        //アタマッコの装備を手に入れる
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
                        encountEnemy(60);
                        break;
                    case 28:
                        activeText(22);
                        break;
                    case 29:
                        encountEnemy(61);
                        break;
                    case 30:
                        encountEnemy(62);
                        break;
                    case 31:
                        NextStageCheckPanel.SetActive(true);//戦闘が終わると表示する
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
                        encountEnemy(63);
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
                        encountEnemy(3);
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
                        encountEnemy(64);
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
                        encountEnemy(65);
                        break;
                    case 22:
                        activeText(17);
                        break;
                    case 23:
                        activeText(18);
                        break;
                    case 24:
                        encountEnemy(66);
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
                        encountEnemy(67);
                        break;
                    case 29:
                        activeText(22);
                        break;
                    case 30:
                        encountEnemy(68);
                        break;
                    case 31:
                        NextStageCheckPanel.SetActive(true);//戦闘が終わると表示する
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
                        //魔物との戦闘
                        encountEnemy(29);
                        break;
                    case 3:
                        activeText(1);
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        //魔物との戦闘
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
                        //盗賊との戦闘
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
                        //魔物との戦闘
                        encountEnemy(12);
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        activeText(3);
                        break;
                    case 6:
                        //魔物との戦闘
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
                        //大男との戦闘、勝利すると報酬
                        encountEnemy(14);
                        break;
                    case 19:
                        activeText(15);
                        break;
                    case 21:
                        activeText(17);
                        break;
                    case 22:
                        //魔物との戦闘
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
                        //兵士と戦闘
                        encountEnemy(36);
                        break;
                    case 31:
                        activeText(25);
                        break;
                    case 32:
                        activeText(26);
                        break;
                    case 33:
                        //下水の魔物と戦闘
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
                        activeText(10);
                        break;
                    case 10:
                        activeText(9);
                        break;
                    case 11:
                        activeText(8);//アタマッコの装備を手に入れる。
                        break;
                    case 13:
                        activeText(12);
                        break;
                    case 14:
                        encountEnemy(32);
                        break;
                    case 15:
                        activeText(13);
                        break;
                    case 16:
                        activeText(14);
                        break;
                    case 17:
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
                        encountEnemy(37);
                        break;
                    case 22:
                        activeText(18);
                        break;
                    case 23:
                        activeText(19);
                        break;
                    case 24:
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
                        encountEnemy(69);
                        break;
                    case 7:
                        encountEnemy(70);
                        break;
                    case 8:
                        activeText(5);
                        break;
                    case 9:
                        encountEnemy(71);
                        break;
                    case 10:
                        activeText(6);
                        break;
                    case 11:
                        encountEnemy(72);
                        break;
                    case 12:
                        encountEnemy(73);
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
                        encountEnemy(74);
                        break;
                    case 23:
                        activeText(16);
                        break;
                    case 24:
                        activeText(17);
                        break;
                    case 25:
                        encountEnemy(75);
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
                        encountEnemy(76);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        activeText(6);
                        break;
                    case 9:
                        encountEnemy(77);
                        break;
                    case 10:
                        activeText(7);
                        break;
                    case 11:
                        activeText(8);
                        break;
                    case 12:
                        encountEnemy(5);
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
                        encountEnemy(78);
                        break;
                    case 22:
                        activeText(17);
                        break;
                    case 23:
                        encountEnemy(79);
                        break;
                    case 24:
                        encountEnemy(80);
                        break;
                    case 25:
                        activeText(18);
                        break;
                    case 27:
                        encountEnemy(81);
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
                        encountEnemy(82);
                        break;
                    case 10:
                        activeText(8);
                        break;
                    case 11:
                        encountEnemy(83);
                        break;
                    case 12:
                        activeText(9);
                        break;
                    case 13:
                        activeText(10);
                        break;
                    case 14:
                        encountEnemy(92);
                        break;
                    case 15:
                        activeText(11);
                        break;
                    case 16:
                        encountEnemy(5);
                        break;
                    case 17:
                        activeText(12);
                        break;
                    case 18:
                        encountEnemy(89);
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
                        encountEnemy(84);
                        break;
                    case 27:
                        encountEnemy(85);
                        break;
                    case 28:
                        activeText(20);
                        break;
                    case 29:
                        encountEnemy(86);
                        break;
                    case 30:
                        encountEnemy(87);
                        break;
                    case 31:
                        activeText(21);
                        break;
                    case 32:
                        encountEnemy(88);
                        break;
                    case 33:
                        activeText(22);
                        break;
                    case 34:
                        encountEnemy(90);
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
        /// イベントの対応する番号を引数に持ってきて、会話用テキストに設定した後
        /// 会話用のcanvasを表示して会話を始める。
        /// </summary>
        private void activeText(int eventTextNumber)
        {
            Comment.currentChapter = eventTextNumber;
            NovelCanvas.SetActive(true);
            Comment.nextText();
        }
        //会話終了時に起こるイベント
        public void endEvent(int eventNum)
        {
            StartCoroutine(talkEndEvent(eventNum));
        }
        /// <summary>
        /// ステージを変更する際の関数
        /// </summary>
        IEnumerator changeStage(int currentStageNum, int currentChapter)//現在のステージ番号とchapterを引数に持ってくる
        {
            Scene[currentStageNum].SetActive(false);//現在のステージを非表示にする
            Scene[currentStageNum + 1].SetActive(true);//次のステージを表示する
            Comment.currentChapter = currentChapter;
            NovelCanvas.SetActive(true);
            Comment.ChangeBackGroundImage(1);
            Comment.deleteText();
            Comment.onAnimation = true;
            Debug.Log("nextTile:" + Scene[currentStageNum + 1].transform.GetChild(1).name);
            FindObjectOfType<StoryBoardPlayerMove>().nextPosition(Scene[currentStageNum + 1].transform.GetChild(0).position);
            yield return new WaitForSeconds(2f);
            Comment.ChangeBackGroundImage(2);
            yield return new WaitForSeconds(2f);
            Comment.nextText();
            Comment.onAnimation = false;
            Debug.Log("終了イベント：" + currentStageNum);
        }
        /// <summary>
        /// 敵と遭遇した際に、エネミーのデータを持ってきて戦闘シーンに移動する
        /// </summary>
        private void encountEnemy(int EnemyID)
        {
            PlayerConfig.afterBattle = true;
            enemyDeckData.setDeckData(EnemyID);
            SceneManager.LoadScene("BattleCPU");
        }

        private void displayText(string Text,float FadeInTime,float FadeOutTime)
        {
            var InstantiatePosition = NextStageCheckPanel.transform.parent.gameObject;
            var instantPrefab = Instantiate(instantTextPrefab, InstantiatePosition.transform);
            instantPrefab.GetComponent<InstantTMP>().Init(Text, FadeInTime, FadeOutTime);
        }
        /// <summary>
        /// eventNumの会話が終わった後に動作する。
        /// </summary>
        IEnumerator talkEndEvent(int eventNum)
        {
            yield return new WaitForFixedUpdate();
            if (scenario == scenarioType.scenario1)
            {
                switch (eventNum)
                {
                    case 4:
                        StartCoroutine(changeStage(0, 4));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 9:
                        encountEnemy(10);
                        break;
                    case 12:
                        displayText("霊魂使いの装備を手に入れた", 1f, 1f);
                        PlayerConfig.unLockEquipment[12] = true;
                        PlayerConfig.unLockEquipment[13] = true;
                        PlayerConfig.unLockEquipment[14] = true;
                        break;
                    case 17:
                        StartCoroutine(changeStage(1, 17));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 26:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        Debug.Log("何も設定されていない終了イベントです");
                        break;
                }
            }
            else if (scenario == scenarioType.scenario2)
            {
                switch (eventNum)
                {
                    case 8:
                        //アタマッコの新しい装備を手に入れる
                        displayText("ショートボウのアクセサリを手に入れた", 1f, 1f);
                        PlayerConfig.unLockEquipment[63] = true;
                        Debug.Log("終了イベント8");
                        break;
                    case 12:
                        Scene[1].SetActive(true);
                        NovelCanvas.SetActive(true);
                        yield return new WaitForSeconds(4f);
                        Comment.ChangeBackGroundImage(0);
                        NovelCanvas.SetActive(false);
                        break;
                    case 16:
                        NextStageCheckPanel.SetActive(true);
                        break;
                    default:
                        Debug.Log("何も設定されていない終了イベントです");
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
                        Debug.Log("何も設定されていない終了イベントです");
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioBlue1)
            {
                switch (eventNum)
                {
                    case 7:
                        StartCoroutine(changeStage(0, 7));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 13:
                        displayText("ストライクのアクセサリを手に入れた", 1f, 1f);
                        PlayerConfig.unLockEquipment[52] = true;
                        break;
                    case 14:
                        displayText("六角玉石のアクセサリを手に入れた", 1f, 1f);
                        PlayerConfig.unLockEquipment[54] = true;
                        break;
                    case 16:
                        encountEnemy(102);
                        break;
                    case 15:
                        encountEnemy(93);
                        break;
                    case 17:
                        StartCoroutine(changeStage(1, 17));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 28:
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
                        StartCoroutine(changeStage(0, 7));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 15:
                        encountEnemy(46);
                        break;
                    case 16:
                        StartCoroutine(changeStage(1, 16));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                        encountEnemy(91);
                        break;
                    case 11:
                        StartCoroutine(changeStage(0, 11));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 21:
                        encountEnemy(5);
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
                        StartCoroutine(changeStage(0, 8));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 20:
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
                        StartCoroutine(changeStage(0, 6));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 15:
                        StartCoroutine(changeStage(1, 15));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 18:
                        displayText("追加移動のアクセサリを手に入れた", 1f, 1f);
                        PlayerConfig.unLockEquipment[56] = true;
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
                        StartCoroutine(changeStage(0, 7));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 13:
                        StartCoroutine(changeStage(1, 13));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    default:
                        break;
                }
            }
            else if (scenario == scenarioType.scenarioWhite1)
            {
                switch (eventNum)
                {
                    case 6:
                        StartCoroutine(changeStage(0, 6));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 12:
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
                        StartCoroutine(changeStage(0, 8));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 16:
                        StartCoroutine(changeStage(1, 16));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                    case 11:
                        StartCoroutine(changeStage(0, 11));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                        StartCoroutine(changeStage(0, 8));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 14:
                        StartCoroutine(changeStage(1, 14));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                        StartCoroutine(changeStage(0, 10));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 19:
                        StartCoroutine(changeStage(1, 19));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                        StartCoroutine(changeStage(0, 14));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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