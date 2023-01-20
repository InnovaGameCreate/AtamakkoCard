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
                Text.text = "�A���[�i�����N�P\n" +
                            "���X�̎g�k\n" +
                            "\n"+
                            "�Ԃ̍��̎א_��g�ɏh�������B�����Ǝ��R���]���ɓ����͂͐l�ލŋ����ւ�\n";
                break;
            case 2:
                Text.text = "�A���[�i�����N�Q\n" +
                            "�N���̎g�k\n" +
                            "\n" +
                            "�̍��̐_�a�ɓw�߂鏭���B�c���̂��납��b���グ���Ă��肻�̋����͐��E�ł��g�b�v���x�����ւ�\n";
                break;
            case 3:
                Text.text = "�A���[�i�����N�R\n" +
                            "�Y���̎g�k\n" +
                            "\n" +
                            "���̍���\���΂߂�j�B���E�e�n����Q���Ă������ꂽ�l�Ԃ����R���^�N�g����邷�ׂ������Ă��Ȃ�\n";
                break;
            case 4:
                Text.text = "�A���[�i�����N�S\n" +
                            "�썑�̎g�k\n" +
                            "\n" +
                            "�̍��̎g�k�̒��ł���ʂ̑��݁B��ʓI�ɂ��̋�����킢���͒m���Ă��Ȃ����A�����ł�������Ă���\n";
                break;
            case 5:
                Text.text = "�A���[�i�����N�T\n" +
                            "�}�L�i\n" +
                            "\n" +
                            "�l������̋��ɑ̂ƌĂ΂ꂽ���݁B�������ŉ��̂��߂ɍ��ꂽ���s���̑���\n";
                break;
            case 6:
                Text.text = "�A���[�i�����N�U\n" +
                            "�q���[�}�m�C�h\n" +
                            "\n" +
                            "�يE����̗��K�ҁA���E�N���̂��߂ɔj�󊈓��ɋ΂��ސS�����ʕ���\n";
                break;
            case 7:
                Text.text = "�A���[�i�����N�V\n" +
                            "�o�l�b�g\n" +
                            "\n" +
                            "���̊Ԃɂ����݂��Ă��������́B�����邱�ƂȂ����̏�ɂ��葱����\n";
                break;
            case 8:
                Text.text = "�A���[�i�����N�W\n" +
                            "�����̃G�[�X �A�J�l\n" +
                            "\n" +
                            "�����W�c�̃G�[�X�A���܂�Ȃ���ɂ��Č������P���ɑς������Ă���\n";
                break;
            case 9:
                Text.text = "�A���[�i�����N�X\n" +
                            "�i�C���u���[\n" +
                            "\n" +
                            "�s����9�ʁA��ʃ����N��������ϓ����Ă�������9�ʂɋ���������͂̎�����\n";
                break;
            case 10:
                Text.text = "�A���[�i�����N�P�O\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 11:
                Text.text = "�A���[�i�����N�P�P\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 12:
                Text.text = "�A���[�i�����N�P�Q\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 13:
                Text.text = "�A���[�i�����N�P�R\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 14:
                Text.text = "�A���[�i�����N�P�S\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 15:
                Text.text = "�A���[�i�����N�P�T\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 16:
                Text.text = "�A���[�i�����N�P�U\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 17:
                Text.text = "�A���[�i�����N�P�V\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 18:
                Text.text = "�A���[�i�����N�P�W\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 19:
                Text.text = "�A���[�i�����N�P�X\n" +
                            "�e�X�[�g����\n" +
                            "\n" +
                            "�f�o�b�N�̂��߂ɐ��܂ꂽ�B�����łł͓o�ꂵ�Ȃ����J���w�͔ނ̂��Ƃ�Y��邱�Ƃ͂Ȃ��B\n";
                break;
            case 20:
                Text.text = "�A���[�i�����N�Q�O\n" +
                            "�S�[���f���A�b�N�X\n" +
                            "\n" +
                            "���s����h�����ꂽ���m�A�P���ɑς����ꂸ�n���ɔ�΂���Ă����߂�������\n";
                break;
            default:
                break;
        }
    }
}
