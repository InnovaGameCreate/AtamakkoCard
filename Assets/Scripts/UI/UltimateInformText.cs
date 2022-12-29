using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using Photon.Pun;
using Player;

namespace UI
{
    public class UltimateInformText : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// �A���e�B���b�g���g�p����Ǝ������Ɏ����̎g�p�����A���e�B���b�g���e�L�X�g�ŕ\��
        /// ���葤�ɂ������̎g�p�����A���e�B���b�g���e�L�X�g�ŕ\��
        /// </summary>
        [SerializeField] private TextMeshProUGUI _playerInformText;
        [SerializeField] private TextMeshProUGUI _enemyInformText;

        private void Start()
        {
            _enemyInformText.text = null;
            _playerInformText.text = null;
        }

        public async void setText(UltimateState UltimateType)
        {
            await Task.Delay(2000);
            string ultimateText = null;//�\���p�e�L�X�g�̐���
            switch (UltimateType)//�g�p����A���e�B���b�g�ɑΉ�����G�t�F�N�g���Đ�����B
            {
                case UltimateState.Recover:
                    ultimateText = "�̗͉񕜂̃A���e�B���b�g���g�p���܂���";//�̗͉񕜃A���e�B���b�g�̎g�p�G�t�F�N�g
                    break;
                case UltimateState.Attack:
                    ultimateText = "�U���͏㏸�̃A���e�B���b�g���g�p���܂���";//�U���͏㏸�A���e�B���b�g�̎g�p�G�t�F�N�g
                    break;
                case UltimateState.Speed:
                    ultimateText = "�搧�x�㏸�̃A���e�B���b�g���g�p���܂���";//�搧�x�㏸�A���e�B���b�g�̎g�p�G�t�F�N�g
                    break;
                default:
                    Debug.LogError("�������̃A���e�B���b�g��\�����悤�Ƃ��Ă��܂�");
                    break;
            }
            _playerInformText.text = ultimateText;
            if (PlayerConfig.IsOnline)
            {
                ultimateText = "�����" + ultimateText;
                photonView.RPC(nameof(setEnemyInformText), RpcTarget.Others, ultimateText);
            }

            await Task.Delay(3000);
            if (PlayerConfig.IsOnline)
            {
                ultimateText = "�����" + ultimateText;
                photonView.RPC(nameof(setEnemyInformText), RpcTarget.Others, "");
            }
            _playerInformText.text = null;
        }

        [PunRPC]
        private void setEnemyInformText(string ultimateText)
        {
            _enemyInformText.text = ultimateText;
        }
    }
}