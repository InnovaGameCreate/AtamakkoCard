using System.Collections;
using UnityEngine;

namespace storyMode
{
    public class StoryBoardEvent : MonoBehaviour
    {
        /// <summary>
        /// 実装されているシナリオをscenarioTypeで用意する。用意されているもの以外はイベントが起こらない
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
        private scenarioType scenario;//どのscenarioに対応するか
        [SerializeField]
        private GameObject[] Scene;//シナリオに登場するステージ
        [SerializeField]
        private GameObject NestStageCheckPanel;//次のステージへ進むか確認用パネル
        private void Start()
        {
            NestStageCheckPanel = FindObjectOfType<NestStagePanel>().gameObject;
            NestStageCheckPanel.SetActive(false);
            Comment.currentChapter = 0;
            Scene[1].SetActive(false);
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
                    case 5:
                        activeText(4);
                        break;
                    case 6:
                        //5の終了時イベントに以降
                        break;
                    case 7:
                        activeText(6);
                        break;
                    case 8:
                        activeText(7);
                        break;
                    case 9:
                        //戦闘
                        activeText(8);
                        break;
                    case 10:
                        activeText(9);
                        break;
                    case 11:
                        activeText(10);
                        break;
                    case 12://ドラゴンとの戦闘
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
                    case 17://敵との戦闘
                        activeText(16);
                        break;
                    case 18:
                        activeText(17);
                        break;
                    case 19://ボス戦
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
                    case 24://仲間と合流
                        activeText(23);
                        break;
                    case 25://戦闘

                        break;
                    case 26:
                        activeText(25);
                        break;
                    case 27:
                        activeText(26);
                        break;
                    case 28://ボス戦
                        activeText(27);
                        break;
                    case 29:
                        activeText(28);
                        break;
                    case 30://戦闘
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
                    case 34://戦闘
                        activeText(33);
                        break;
                    case 35:
                        activeText(34);
                        break;
                    case 36://戦闘
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
                        activeText(13);
                        break;
                    case 15:
                        //戦闘
                        break;
                    case 16:
                        activeText(14);
                        break;
                    case 17:
                        //戦闘
                        break;
                    case 18:
                        activeText(15);
                        break;
                    case 19:
                    //ボス戦
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
                        //戦闘
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
                        //戦闘
                        break;
                    case 28:
                        //戦闘
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
                        //戦闘
                        break;
                    case 22:
                        activeText(20);
                        break;
                    case 23:
                        //戦闘
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
                        //戦闘
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
                        //戦闘
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
                        //野生生物と戦闘
                        break;
                    case 6:
                        activeText(4);
                        break;
                    case 7:
                        activeText(5);
                        break;
                    case 8:
                        //魔物との戦闘
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
                        //魔物との戦闘
                        break;
                    case 18:
                        activeText(14);
                        break;
                    case 19:
                        //ボス戦
                        StartCoroutine(changeStage(1, 19));//戦闘が終わると表示する
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
                        //邪神の加護を受けた魔物との戦闘
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
                        //魔物と戦闘
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
                    //魔物との戦闘
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
                        //余った魔物との戦闘
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
                        //道を塞いでる魔物との戦闘
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
                        //大きな魔物との戦闘
                        break;
                    case 28:
                        activeText(22);
                        break;
                    case 29:
                        //ローブの男との戦闘
                        break;
                    case 30:
                        //低級の邪神との戦闘

                        NestStageCheckPanel.SetActive(true);//戦闘が終わると表示する
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
                        //魔物との戦闘
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
                        //煌々の使徒と模擬戦
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
                        //湖の魔物との戦闘
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
                        //魔物との戦闘
                        break;
                    case 22:
                        activeText(17);
                        break;
                    case 23:
                        activeText(18);
                        break;
                    case 24:
                        //魔物との戦闘
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
                        //邪神との戦闘
                        break;
                    case 29:
                        activeText(22);
                        break;
                    case 30:
                        //完全体煌々の使徒との戦闘
                        NestStageCheckPanel.SetActive(true);//戦闘が終わると表示する
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
                        break;
                    case 3:
                        activeText(1);
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        //魔物との戦闘
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
                        break;
                    case 4:
                        activeText(2);
                        break;
                    case 5:
                        activeText(3);
                        break;
                    case 6:
                        //魔物との戦闘
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
                        break;
                    case 19:
                        activeText(15);
                        break;
                    case 21:
                        activeText(17);
                        break;
                    case 22:
                        //魔物との戦闘
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
                        break;
                    case 31:
                        activeText(25);
                        break;
                    case 32:
                        activeText(26);
                        break;
                    case 33:
                        //下水の魔物と戦闘
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
        /// eventNumの会話が終わった後に動作する。
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
            Debug.Log("終了イベント：" + currentStageNum);
        }
        IEnumerator talkEndEvent(int eventNum)
        {
            yield return new WaitForFixedUpdate();
            if (scenario == scenarioType.scenario1)
            {
                switch (eventNum)
                {
                    case 5:
                        StartCoroutine(changeStage(0, 5));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 17:
                        Comment.ChangeBackGroundImage(0);
                        //戦闘開始の信号を送る
                        Debug.Log("終了イベント17");
                        break;
                    case 19:
                        Comment.ChangeBackGroundImage(0);
                        //戦闘開始の信号を送る、戦闘後次のステージへ
                        Debug.Log("終了イベント19");
                        break;
                    case 28:
                        Comment.ChangeBackGroundImage(0);
                        //戦闘開始の信号を送る
                        Debug.Log("終了イベント28");
                        break;
                    case 39:
                        NestStageCheckPanel.SetActive(true);
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
                        Debug.Log("終了イベント8");
                        break;
                    case 13:
                        Scene[1].SetActive(true);
                        NovelCanvas.SetActive(true);
                        yield return new WaitForSeconds(4f);
                        Comment.ChangeBackGroundImage(0);
                        NovelCanvas.SetActive(false);
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
                    case 14:
                        //教導の使徒と戦闘
                        break;
                    case 26:
                        //ボス戦闘
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
                        StartCoroutine(changeStage(0, 7));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 14:
                        StartCoroutine(changeStage(1, 14));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 15:
                        //少年と戦闘
                        break;
                    case 22:
                        //ラスボス戦
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
                        StartCoroutine(changeStage(0, 12));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 17:
                        //ボス戦
                        break;
                    case 19:
                        //ボス戦
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
                        StartCoroutine(changeStage(0, 8));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 14:
                        //小鹿を襲う魔物と戦闘
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
                        StartCoroutine(changeStage(0, 6));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 15:
                        StartCoroutine(changeStage(1, 15));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                    case 8:
                        //水生の魔物との戦闘
                        StartCoroutine(changeStage(0, 8));//現在のステージ数：現在のチャプターを送って次のステージへ移る
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
                        StartCoroutine(changeStage(0, 8));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    case 16:
                        StartCoroutine(changeStage(1, 16));//現在のステージ数：現在のチャプターを送って次のステージへ移る
                        break;
                    default:
                        break;
                }
            }
        }
    }
}