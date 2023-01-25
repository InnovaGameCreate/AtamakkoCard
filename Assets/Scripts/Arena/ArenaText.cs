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
                Text.text = "�A���[�i�����N" + RankCheck(1) + "\n" +
                            "���X�̎g�k\n" +
                            "\n"+
                            "�Ԃ̍��̎א_��g�ɏh�������B�����Ǝ��R���]���ɓ����͂͐l�ލŋ����ւ�\n";
                break;
            case 2:
                Text.text = "�A���[�i�����N" + RankCheck(2) + "\n" +
                            "�N���̎g�k\n" +
                            "\n" +
                            "�̍��̐_�a�ɓw�߂鏭���B�c���̂��납��b���グ���Ă��肻�̋����͐��E�ł��g�b�v���x�����ւ�\n";
                break;
            case 3:
                Text.text = "�A���[�i�����N" + RankCheck(3) + "\n" +
                            "�Y���̎g�k\n" +
                            "\n" +
                            "���̍���\���΂߂�j�B���E�e�n����Q���Ă������ꂽ�l�Ԃ����R���^�N�g����邷�ׂ������Ă��Ȃ�\n";
                break;
            case 4:
                Text.text = "�A���[�i�����N" + RankCheck(4) + "\n" +
                            "�썑�̎g�k\n" +
                            "\n" +
                            "�̍��̎g�k�̒��ł���ʂ̑��݁B��ʓI�ɂ��̋�����킢���͒m���Ă��Ȃ����A�����ł�������Ă���\n";
                break;
            case 5:
                Text.text = "�A���[�i�����N" + RankCheck(5) + "\n" +
                            "�}�L�i\n" +
                            "\n" +
                            "�l������̋��ɑ̂ƌĂ΂ꂽ���݁B�������ŉ��̂��߂ɍ��ꂽ���s���̑���\n";
                break;
            case 6:
                Text.text = "�A���[�i�����N" + RankCheck(6) + "\n" +
                            "�q���[�}�m�C�h\n" +
                            "\n" +
                            "�يE����̗��K�ҁA���E�N���̂��߂ɔj�󊈓��ɋ΂��ސS�����ʕ���\n";
                break;
            case 7:
                Text.text = "�A���[�i�����N" + RankCheck(7) + "\n" +
                            "�o�l�b�g\n" +
                            "\n" +
                            "���̊Ԃɂ����݂��Ă��������́B�����邱�ƂȂ����̏�ɂ��葱����\n";
                break;
            case 8:
                Text.text = "�A���[�i�����N" + RankCheck(8) + "\n" +
                            "�����̃G�[�X �A�J�l\n" +
                            "\n" +
                            "�����W�c�̃G�[�X�A���܂�Ȃ���ɂ��Č������P���ɑς������Ă���\n";
                break;
            case 9:
                Text.text = "�A���[�i�����N" + RankCheck(9) + "\n" +
                            "�i�C���u���[\n" +
                            "\n" +
                            "�s����9�ʁA��ʃ����N��������ϓ����Ă�������9�ʂɋ���������͂̎�����\n";
                break;
            case 10:
                Text.text = "�A���[�i�����N" + RankCheck(10) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 11:
                Text.text = "�A���[�i�����N" + RankCheck(11) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 12:
                Text.text = "�A���[�i�����N" + RankCheck(12) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 13:
                Text.text = "�A���[�i�����N" + RankCheck(13) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 14:
                Text.text = "�A���[�i�����N" + RankCheck(14) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 15:
                Text.text = "�A���[�i�����N" + RankCheck(15) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 16:
                Text.text = "�A���[�i�����N" + RankCheck(16) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 17:
                Text.text = "�A���[�i�����N" + RankCheck(17) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 18:
                Text.text = "�A���[�i�����N" + RankCheck(18) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 19:
                Text.text = "�A���[�i�����N" + RankCheck(19) + "\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 20:
                Text.text = "�A���[�i�����N" + RankCheck(20) + "\n" +
                            "�S�[���f���A�b�N�X\n" +
                            "\n" +
                            "���s����h�����ꂽ���m�A�P���ɑς����ꂸ�n���ɔ�΂���Ă����߂�������\n";
                break;
            default:
                break;
        }
    }

    private int RankCheck(int baseRank)
    {
        if (baseRank > PlayerConfig.ArenaRank) return baseRank;
        else return (baseRank + 1);
    }
}
