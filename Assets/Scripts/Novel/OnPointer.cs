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
                case 1://��1��
                    TitleText.text = "�n�܂�̕���";
                    SummaryText.text = "�@�S�Ă̎n�܂�B�m��Ȃ����A�̒��Ŗڂ��o�܂��B\n�@�Ȃ�����ȏꏊ�ɂ��������v���o�����ɂ��A�����v���o�����Ƃ��ł��Ȃ��B";
                    break;
                case 2://��2��
                    TitleText.text = "�l�C���Ȃ���";
                    SummaryText.text = "�@�h���S�����̐N�ƕ�����N���̎g�k�Ƌ��ɖK�ꂽ���͐Â��������B\n�@�Z���͉����ɍs�����̂��N���̎g�k�Ƌ��ɒ�����i�߂�B";
                    break;
                case 3://��1��
                    TitleText.text = "ᏋC���Y���C";
                    SummaryText.text = "�@�̍��̏ے��ł��鐅�_�̐_�a���������Ǝv����ᏋC���C��Y���Ă���B\n�@���̒����̂��߂ɗN���̎g�k�Ƌ��ɐ_�a�ւƌ������B";
                    break;
                case 4://��2��
                    TitleText.text = "�����̌̋�";
                    SummaryText.text = "�@ᏋC����_�a�̍ŉ��Ŗ����Ă�����̏����̋͂��ȋL���𗊂�ɏ����̌̋���T���B\n�@�l�X�ȑ���q�˕������A1�̑��Ŏ����Ɋ������܂��";
                    break;
                case 5://��3��
                    TitleText.text = "�׋��c����";
                    SummaryText.text = "�@���Ɏ׋��c�̖{���_�����邯���B\n�@�׋��c�����̂��߂ɑ傪����ȍ�킪���s����邱�ƂɂȂ�B";
                    break;
                case 6://�I1��
                    TitleText.text = "�w����ׂ��I���̓��x";
                    SummaryText.text = "�@���ɂ���Ă����w����ׂ��I���̓��x�B\n�@�يE����א_�����͂Ȗ�����������Ă���Ă���B";
                    break;
                case 7://��1��
                    TitleText.text = "��";
                    SummaryText.text = "�@�N���̎g�k�ƕ�����ĐԂ̍���ڎw���B\n�@�����Ă��鑺����������ɖ�����ǂ������Əo��B";
                    break;
                case 8://��2��
                    TitleText.text = "�Ԃ̉���";
                    SummaryText.text = "�@�����̐N�U��h���Ԃ̉����Ɍ������B\n�@�������A���Ɏ��͎̂v�������Ȃ��i�K�܂Ői��ł����B";
                    break;
                case 9://��3��
                    TitleText.text = "�Ăъo�܂���镕��";
                    SummaryText.text = "�@�Ԃ̍����s�̒n���ɕ��󂳂�Ă����א_���s���S�Ȃ�������������B\n�@�Ԃ���}���Ŗ߂邪���ɉ��s�͉�ŏ�ԂɂȂ��Ă����B";
                    break;
                case 10://�I2��
                    TitleText.text = "���������I���";
                    SummaryText.text = "�@�يE���痈��א_�̎x�����s���W�c�̓������s�����ƂɂȂ����B\n�@���X�̎g�k�Ƌ��Ɍ���������͐̍��ɂ���Âт��_�a�B";
                    break;
                case 11://��1��
                    TitleText.text = "���l�Ƃ̗�";
                    SummaryText.text = "�@���̍��ւƓ��̓r���A�P���Ă��鏤�l��������B�������珤�l�Ƃ̗����n�܂�";
                    break;
                case 12://��2��
                    TitleText.text = "�Ӌ��s�s�ł̖�";
                    SummaryText.text = "�@����������Ӌ��s�s�A�����ł͎v�������Ȃ��ƍٓ������s���Ă����B\n�@�����𐳂����߂ɗ̎�̌��ւƒ��k�������݂�B";
                    break;
                case 13://��3��
                    TitleText.text = "����";
                    SummaryText.text = "�@�Ñ��Ղɖ��邨���T���ɍs���B\n�@�������A�Ñ��Ղł͑z��O�̎������҂��󂯂Ă����B";
                    break;
                case 14://�I3��
                    TitleText.text = "�I���̕���";
                    SummaryText.text = "�@�����ߋ��ɖ߂邱�Ƃ͏o���Ȃ��Ō�̐킢���n�܂�B\n�@�א_��|�����ߐ_�̗͂���ɓ���Ɏn�܂�̒n�ւƌ������B";
                    break;
                default:
                    Debug.LogError("�z��O�̐��l�ł�");
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