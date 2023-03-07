using UnityEngine;

namespace storyMode
{
    public class HideTile : MonoBehaviour
    {
        /// <summary>
        /// ��\���̃I�u�W�F�N�g�����̏����ŕ\������
        /// </summary>
        [SerializeField] private GameObject[] hideTile;

        public void OnClick(bool _bool)
        {
            var PlayerObject = GameObject.FindGameObjectWithTag("Player");
            if (PlayerCanMoveDistance(PlayerObject.transform.position))
            {
                foreach (var item in hideTile)
                {
                    item.SetActive(_bool);
                }
            }
        }
        //�v���C���[���ړ��\�ȋ����̏ꍇ��true�̒l��Ԃ�
        private bool PlayerCanMoveDistance(Vector3 moveTargetPosition)
        {
            var distance = Vector3.Distance(gameObject.transform.position, moveTargetPosition);
            if (distance < 180) return true;
            else return false;
        }
    }
}