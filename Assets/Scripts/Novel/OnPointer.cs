using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace system.story
{
    public class OnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TextMeshProUGUI TitleText;
        [SerializeField]
        private TextMeshProUGUI SummaryText;
        public int StoryNum;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnMouseEnter");
            switch (StoryNum)
            {
                case 1://序1章
                    TitleText.text = "始まりの物語";
                    SummaryText.text = "　全ての始まり。知らない洞窟の中で目を覚ます。\n　なぜこんな場所にいたかを思い出そうにも、何も思い出すことができない。";
                    break;
                case 2://序2章
                    TitleText.text = "人気がない村";
                    SummaryText.text = "　ドラゴン乗りの青年と分かれ湧水の使徒と共に訪れた村は静かだった。\n　住民は何処に行ったのか湧水の使徒と共に調査を進める。";
                    break;
                case 3://青1章
                    TitleText.text = "瘴気が漂う海";
                    SummaryText.text = "　青の国の象徴である水神の神殿が発生源と思われる瘴気が海を漂っている。\n　その調査のために湧水の使徒と共に神殿へと向かう。";
                    break;
                case 4://青2章
                    TitleText.text = "少女の故郷";
                    SummaryText.text = "　瘴気を放つ神殿の最奥で眠っていた謎の少女の僅かな記憶を頼りに少女の故郷を探す。\n　様々な村を尋ね歩く中、1つの村で事件に巻き込まれる";
                    break;
                case 5://青3章
                    TitleText.text = "邪教団討伐";
                    SummaryText.text = "　ついに邪教団の本拠点を見るけた。\n　邪教団討伐のために大がかりな作戦が実行されることになる。";
                    break;
                case 6://終1章
                    TitleText.text = "『来るべき終焉の日』";
                    SummaryText.text = "　ついにやってきた『来るべき終焉の日』。\n　異界から邪神が強力な魔物を引きつれてやってくる。";
                    break;
                case 7://赤1章
                    TitleText.text = "邂逅";
                    SummaryText.text = "　湧水の使徒と分かれて赤の国を目指す。\n　困っている村を助けた先に魔物を追う少女と出会う。";
                    break;
                case 8://赤2章
                    TitleText.text = "砦の応援";
                    SummaryText.text = "　魔物の侵攻を防ぐ砦の応援に向かう。\n　しかし、既に自体は思いもよらない段階まで進んでいた。";
                    break;
                case 9://赤3章
                    TitleText.text = "呼び覚まされる封印";
                    SummaryText.text = "　赤の国王都の地下に封印されていた邪神が不完全ながらも復活した。\n　砦から急いで戻るが既に王都は壊滅状態になっていた。";
                    break;
                case 10://終2章
                    TitleText.text = "早すぎた終わり";
                    SummaryText.text = "　異界から来る邪神の支援を行う集団の討伐を行うことになった。\n　煌々の使徒と共に向かった先は青の国にある古びた神殿。";
                    break;
                case 11://白1章
                    TitleText.text = "商人との旅";
                    SummaryText.text = "　白の国へと道の途中、襲われている商人を助ける。そこから商人との旅が始まる";
                    break;
                case 12://白2章
                    TitleText.text = "辺境都市での厄介事";
                    SummaryText.text = "　立ち寄った辺境都市、そこでは思いもよらない独裁統治が行われていた。\n　統治を正すために領主の元へと直談判を試みる。";
                    break;
                case 13://白3章
                    TitleText.text = "試練";
                    SummaryText.text = "　古代遺跡に眠るお宝を探しに行く。\n　しかし、古代遺跡では想定外の試練が待ち受けていた。";
                    break;
                case 14://終3章
                    TitleText.text = "終わりの物語";
                    SummaryText.text = "　もう過去に戻ることは出来ない最後の戦いが始まる。\n　邪神を倒すため神の力を手に入れに始まりの地へと向かう。";
                    break;
                default:
                    Debug.LogError("想定外の数値です");
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TitleText.text = "";
            SummaryText.text = "";
            Debug.Log("OnMouseExit");
        }
    }
}