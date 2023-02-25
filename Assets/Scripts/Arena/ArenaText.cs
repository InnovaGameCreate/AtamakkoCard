using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ArenaText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    private void Start()
    {
        SetText(20);
    }

    public void SetText(int Num)
    {
        switch (Num)
        {
            case 1:
                Text.text = "アリーナランク" + RankCheck(1) + "\n" +
                            "煌々の使徒\n" +
                            "\n"+
                            "赤の国の邪神を身に宿す少女。寿命と自由を犠牲に得た力は人類最強を誇る。\n";
                break;
            case 2:
                Text.text = "アリーナランク" + RankCheck(2) + "\n" +
                            "湧水の使徒\n" +
                            "\n" +
                            "青の国の神殿に努める少女。幼少のころから鍛え上げられておりその強さは世界でもトップレベルを誇る。\n";
                break;
            case 3:
                Text.text = "アリーナランク" + RankCheck(3) + "\n" +
                            "漂泊の使徒\n" +
                            "\n" +
                            "白の国代表を勤める男。世界各地を放浪しており限られた人間しかコンタクトを取るすべを持っていない。\n";
                break;
            case 4:
                Text.text = "アリーナランク" + RankCheck(4) + "\n" +
                            "護国の使徒\n" +
                            "\n" +
                            "青の国の使徒の中でも上位の存在。一般的にその強さや戦い方は知られていないが、他国でも恐れられている。\n";
                break;
            case 5:
                Text.text = "アリーナランク" + RankCheck(5) + "\n" +
                            "マキナ\n" +
                            "\n" +
                            "人造兵器の究極体と呼ばれた存在。いつ何処で何のために作られたか不明の存在。\n";
                break;
            case 6:
                Text.text = "アリーナランク" + RankCheck(6) + "\n" +
                            "教導の使徒\n" +
                            "\n" +
                            "孤児院を経営する元冒険者。\n" +
                            "冒険者時代よりもかなり落ち着いた正確になっているが、秘めたる闘争本能は昔のままである。。\n";
                break;
            case 7:
                Text.text = "アリーナランク" + RankCheck(7) + "\n" +
                            "バネット\n" +
                            "\n" +
                            "いつの間にか存在していた生命体。消えることなくその場にあり続ける。\n";
                break;
            case 8:
                Text.text = "アリーナランク" + RankCheck(8) + "\n" +
                            "盗賊のエース アカネ\n" +
                            "\n" +
                            "盗賊集団のエース、生まれながらにして厳しい訓練に耐え抜いてきた。\n";
                break;
            case 9:
                if (RankCheck(9) == 9)
                {
                    Text.text = "アリーナランク" + RankCheck(9) + "\n" +
                                "ナインブルー\n" +
                                "\n" +
                                "不動の9位、上位ランクがいくら変動してもずっと9位に居続ける実力の持ち主。\n";
                }
                else
                {
                    Text.text = "アリーナランク" + RankCheck(9) + "\n" +
                                "ナインブルー\n" +
                                "\n" +
                                "かつては不動の9位、今では見る影もなくその地を彷徨っている。\n";
                }
                break;
            case 10:
                Text.text = "アリーナランク" + RankCheck(10) + "\n" +
                            "雷雲の使徒\n" +
                            "\n" +
                            "赤の国出身、煌々の使徒とは休日を共にするほど仲が良い。煌々の使徒との一番の思い出は遠征した街でスイーツ巡りをしたことである。\n" +
                            "雷雲ある所に勝利ありと呼ばれるほどの実力を持ち、国を平和にすることを信条として生きている。\n";
                break;
            case 11:
                Text.text = "アリーナランク" + RankCheck(11) + "\n" +
                            "崩壊の使徒\n" +
                            "\n" +
                            "黒翼の使徒の幼馴染であり、幼い頃将来を約束した仲。白の国の国家転覆を目論んでおり、その機会を伺っている。\n" +
                            "世界を混沌と崩壊に導くことを信条としている。\n";
                break;
            case 12:
                Text.text = "アリーナランク" + RankCheck(12) + "\n" +
                            "テレスト\n" +
                            "\n" +
                            "各国でクーデターを行っている国家犯罪者。国も手を焼いており度々討伐作戦が行われるが全て失敗に終わっている。\n";
                break;
            case 13:
                Text.text = "アリーナランク" + RankCheck(13) + "\n" +
                            "黒翼の使徒\n" +
                            "\n" +
                            "白の国に住むステーキ屋の看板娘。美味しい食材を手に入れるために身に着けた力は凄まじく有事の際は国から声が掛かる。\n" +
                            "お客さんを笑顔にすることを信条としており、彼女の店では笑顔が絶えない幸せな空間がある。\n";
                break;
            case 14:
                Text.text = "アリーナランク" + RankCheck(14) + "\n" +
                            "夕立の使徒\n" +
                            "\n" +
                            "治安部隊に所属する優秀な隊員。銭湯が好みの少女、世界各地の温泉を巡り歩いている。\n";
                break;
            case 15:
                Text.text = "アリーナランク" + RankCheck(15) + "\n" +
                            "道標の使徒\n" +
                            "\n" +
                            "赤の国の切り込み隊長。誰よりも先に立ち進んでいく姿は戦場で味方の支えとなっている。\n" +
                            "戦争の最初の犠牲者は自分であると心に決め、大きな戦場には必ずその姿がある。\n";
                break;
            case 16:
                Text.text = "アリーナランク" + RankCheck(16) + "\n" +
                            "無法の使徒\n" +
                            "\n" +
                            "青の国が保有する暗殺部隊の一員。部隊内でもお互いの素顔は知らない。\n";
                break;
            case 17:
                Text.text = "アリーナランク" + RankCheck(17) + "\n" +
                            "障壁の使徒\n" +
                            "\n" +
                            "最前線の砦で魔物からの侵攻を食い止めていた。優秀な攻撃範囲を持ち、相手を寄せ付けない強さを持つ\n" +
                            "明るい未来を提供することを信条としており、国民には魔物に怯えない生活を送ってほしいと考えている。\n";
                break;
            case 18:
                Text.text = "アリーナランク" + RankCheck(18) + "\n" +
                            "少年\n" +
                            "\n" +
                            "元気いっぱいの暴れん坊な少年。夢は大きく、明るい未来を目指して日々頑張っている。\n";
                break;
            case 19:
                Text.text = "アリーナランク" + RankCheck(19) + "\n" +
                            "街の落ちぶれ\n" +
                            "\n" +
                            "昔はそれなりに腕が立っていたが今では明日の生活にも困る様になってしまった、人に絡んでは金品を巻き上げている。\n";
                break;
            case 20:
                Text.text = "アリーナランク" + RankCheck(20) + "\n" +
                            "ゴールデンアックス\n" +
                            "\n" +
                            "王都から派遣された兵士、訓練に耐えきれず地方に飛ばされてきた悲しき守り手\n";
                break;
            default:
                break;
        }
    }

    private int RankCheck(int baseRank)
    {
        if (baseRank < PlayerConfig.ArenaRank) return baseRank;
        else return (baseRank + 1);
    }
}
