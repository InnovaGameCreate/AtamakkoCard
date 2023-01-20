using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ArenaText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;

    public void SetText(int Num)
    {
        switch (Num)
        {
            case 1:
                Text.text = "アリーナランク１\n" +
                            "煌々の使徒\n" +
                            "\n"+
                            "赤の国の邪神を身に宿す少女。寿命と自由を犠牲に得た力は人類最強を誇る\n";
                break;
            case 2:
                Text.text = "アリーナランク２\n" +
                            "湧水の使徒\n" +
                            "\n" +
                            "青の国の神殿に努める少女。幼少のころから鍛え上げられておりその強さは世界でもトップレベルを誇る\n";
                break;
            case 3:
                Text.text = "アリーナランク３\n" +
                            "漂泊の使徒\n" +
                            "\n" +
                            "白の国代表を勤める男。世界各地を放浪しており限られた人間しかコンタクトを取るすべを持っていない\n";
                break;
            case 4:
                Text.text = "アリーナランク４\n" +
                            "護国の使徒\n" +
                            "\n" +
                            "青の国の使徒の中でも上位の存在。一般的にその強さや戦い方は知られていないが、他国でも恐れられている\n";
                break;
            case 5:
                Text.text = "アリーナランク５\n" +
                            "マキナ\n" +
                            "\n" +
                            "人造兵器の究極体と呼ばれた存在。いつ何処で何のために作られたか不明の存在\n";
                break;
            case 6:
                Text.text = "アリーナランク６\n" +
                            "ヒューマノイド\n" +
                            "\n" +
                            "異界からの来訪者、世界侵略のために破壊活動に勤しむ心持たぬ兵器\n";
                break;
            case 7:
                Text.text = "アリーナランク７\n" +
                            "バネット\n" +
                            "\n" +
                            "いつの間にか存在していた生命体。消えることなくその場にあり続ける\n";
                break;
            case 8:
                Text.text = "アリーナランク８\n" +
                            "盗賊のエース アカネ\n" +
                            "\n" +
                            "盗賊集団のエース、生まれながらにして厳しい訓練に耐え抜いてきた\n";
                break;
            case 9:
                Text.text = "アリーナランク９\n" +
                            "ナインブルー\n" +
                            "\n" +
                            "不動の9位、上位ランクがいくら変動してもずっと9位に居続ける実力の持ち主\n";
                break;
            case 10:
                Text.text = "アリーナランク１０\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 11:
                Text.text = "アリーナランク１１\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 12:
                Text.text = "アリーナランク１２\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 13:
                Text.text = "アリーナランク１３\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 14:
                Text.text = "アリーナランク１４\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 15:
                Text.text = "アリーナランク１５\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 16:
                Text.text = "アリーナランク１６\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 17:
                Text.text = "アリーナランク１７\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 18:
                Text.text = "アリーナランク１８\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 19:
                Text.text = "アリーナランク１９\n" +
                            "テスートワン\n" +
                            "\n" +
                            "デバックのために生まれた。正式版では登場しないが開発陣は彼のことを忘れることはない。\n";
                break;
            case 20:
                Text.text = "アリーナランク２０\n" +
                            "ゴールデンアックス\n" +
                            "\n" +
                            "王都から派遣された兵士、訓練に耐えきれず地方に飛ばされてきた悲しき守り手\n";
                break;
            default:
                break;
        }
    }
}
