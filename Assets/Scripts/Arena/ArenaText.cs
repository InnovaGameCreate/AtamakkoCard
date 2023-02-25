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
                Text.text = "�A���[�i�����N" + RankCheck(1) + "\n" +
                            "���X�̎g�k\n" +
                            "\n"+
                            "�Ԃ̍��̎א_��g�ɏh�������B�����Ǝ��R���]���ɓ����͂͐l�ލŋ����ւ�B\n";
                break;
            case 2:
                Text.text = "�A���[�i�����N" + RankCheck(2) + "\n" +
                            "�N���̎g�k\n" +
                            "\n" +
                            "�̍��̐_�a�ɓw�߂鏭���B�c���̂��납��b���グ���Ă��肻�̋����͐��E�ł��g�b�v���x�����ւ�B\n";
                break;
            case 3:
                Text.text = "�A���[�i�����N" + RankCheck(3) + "\n" +
                            "�Y���̎g�k\n" +
                            "\n" +
                            "���̍���\���΂߂�j�B���E�e�n����Q���Ă������ꂽ�l�Ԃ����R���^�N�g����邷�ׂ������Ă��Ȃ��B\n";
                break;
            case 4:
                Text.text = "�A���[�i�����N" + RankCheck(4) + "\n" +
                            "�썑�̎g�k\n" +
                            "\n" +
                            "�̍��̎g�k�̒��ł���ʂ̑��݁B��ʓI�ɂ��̋�����킢���͒m���Ă��Ȃ����A�����ł�������Ă���B\n";
                break;
            case 5:
                Text.text = "�A���[�i�����N" + RankCheck(5) + "\n" +
                            "�}�L�i\n" +
                            "\n" +
                            "�l������̋��ɑ̂ƌĂ΂ꂽ���݁B�������ŉ��̂��߂ɍ��ꂽ���s���̑��݁B\n";
                break;
            case 6:
                Text.text = "�A���[�i�����N" + RankCheck(6) + "\n" +
                            "�����̎g�k\n" +
                            "\n" +
                            "�ǎ��@���o�c���錳�`���ҁB\n" +
                            "�`���Ҏ���������Ȃ藎�����������m�ɂȂ��Ă��邪�A��߂��铬���{�\�͐̂̂܂܂ł���B�B\n";
                break;
            case 7:
                Text.text = "�A���[�i�����N" + RankCheck(7) + "\n" +
                            "�o�l�b�g\n" +
                            "\n" +
                            "���̊Ԃɂ����݂��Ă��������́B�����邱�ƂȂ����̏�ɂ��葱����B\n";
                break;
            case 8:
                Text.text = "�A���[�i�����N" + RankCheck(8) + "\n" +
                            "�����̃G�[�X �A�J�l\n" +
                            "\n" +
                            "�����W�c�̃G�[�X�A���܂�Ȃ���ɂ��Č������P���ɑς������Ă����B\n";
                break;
            case 9:
                if (RankCheck(9) == 9)
                {
                    Text.text = "�A���[�i�����N" + RankCheck(9) + "\n" +
                                "�i�C���u���[\n" +
                                "\n" +
                                "�s����9�ʁA��ʃ����N��������ϓ����Ă�������9�ʂɋ���������͂̎�����B\n";
                }
                else
                {
                    Text.text = "�A���[�i�����N" + RankCheck(9) + "\n" +
                                "�i�C���u���[\n" +
                                "\n" +
                                "���Ă͕s����9�ʁA���ł͌���e���Ȃ����̒n��f�r���Ă���B\n";
                }
                break;
            case 10:
                Text.text = "�A���[�i�����N" + RankCheck(10) + "\n" +
                            "���_�̎g�k\n" +
                            "\n" +
                            "�Ԃ̍��o�g�A���X�̎g�k�Ƃ͋x�������ɂ���قǒ����ǂ��B���X�̎g�k�Ƃ̈�Ԃ̎v���o�͉��������X�ŃX�C�[�c������������Ƃł���B\n" +
                            "���_���鏊�ɏ�������ƌĂ΂��قǂ̎��͂������A���𕽘a�ɂ��邱�Ƃ�M���Ƃ��Đ����Ă���B\n";
                break;
            case 11:
                Text.text = "�A���[�i�����N" + RankCheck(11) + "\n" +
                            "����̎g�k\n" +
                            "\n" +
                            "�����̎g�k�̗c����ł���A�c����������񑩂������B���̍��̍��Ɠ]����ژ_��ł���A���̋@����f���Ă���B\n" +
                            "���E�����ׂƕ���ɓ������Ƃ�M���Ƃ��Ă���B\n";
                break;
            case 12:
                Text.text = "�A���[�i�����N" + RankCheck(12) + "\n" +
                            "�e���X�g\n" +
                            "\n" +
                            "�e���ŃN�[�f�^�[���s���Ă��鍑�ƔƍߎҁB��������Ă��Ă���x�X������킪�s���邪�S�Ď��s�ɏI����Ă���B\n";
                break;
            case 13:
                Text.text = "�A���[�i�����N" + RankCheck(13) + "\n" +
                            "�����̎g�k\n" +
                            "\n" +
                            "���̍��ɏZ�ރX�e�[�L���̊Ŕ��B���������H�ނ���ɓ���邽�߂ɐg�ɒ������͂͐��܂����L���̍ۂ͍����琺���|����B\n" +
                            "���q������Ί�ɂ��邱�Ƃ�M���Ƃ��Ă���A�ޏ��̓X�ł͏Ί炪�₦�Ȃ��K���ȋ�Ԃ�����B\n";
                break;
            case 14:
                Text.text = "�A���[�i�����N" + RankCheck(14) + "\n" +
                            "�[���̎g�k\n" +
                            "\n" +
                            "���������ɏ�������D�G�ȑ����B�K�����D�݂̏����A���E�e�n�̉������������Ă���B\n";
                break;
            case 15:
                Text.text = "�A���[�i�����N" + RankCheck(15) + "\n" +
                            "���W�̎g�k\n" +
                            "\n" +
                            "�Ԃ̍��̐؂荞�ݑ����B�N������ɗ����i��ł����p�͐��Ŗ����̎x���ƂȂ��Ă���B\n" +
                            "�푈�̍ŏ��̋]���҂͎����ł���ƐS�Ɍ��߁A�傫�Ȑ��ɂ͕K�����̎p������B\n";
                break;
            case 16:
                Text.text = "�A���[�i�����N" + RankCheck(16) + "\n" +
                            "���@�̎g�k\n" +
                            "\n" +
                            "�̍����ۗL����ÎE�����̈���B�������ł����݂��̑f��͒m��Ȃ��B\n";
                break;
            case 17:
                Text.text = "�A���[�i�����N" + RankCheck(17) + "\n" +
                            "��ǂ̎g�k\n" +
                            "\n" +
                            "�őO���̍ԂŖ�������̐N�U��H���~�߂Ă����B�D�G�ȍU���͈͂������A������񂹕t���Ȃ�����������\n" +
                            "���邢������񋟂��邱�Ƃ�M���Ƃ��Ă���A�����ɂ͖����ɋ����Ȃ������𑗂��Ăق����ƍl���Ă���B\n";
                break;
            case 18:
                Text.text = "�A���[�i�����N" + RankCheck(18) + "\n" +
                            "���N\n" +
                            "\n" +
                            "���C�����ς��̖\���V�ȏ��N�B���͑傫���A���邢������ڎw���ē��X�撣���Ă���B\n";
                break;
            case 19:
                Text.text = "�A���[�i�����N" + RankCheck(19) + "\n" +
                            "�X�̗����Ԃ�\n" +
                            "\n" +
                            "�̂͂���Ȃ�ɘr�������Ă��������ł͖����̐����ɂ�����l�ɂȂ��Ă��܂����A�l�ɗ���ł͋��i�������グ�Ă���B\n";
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
        if (baseRank < PlayerConfig.ArenaRank) return baseRank;
        else return (baseRank + 1);
    }
}
