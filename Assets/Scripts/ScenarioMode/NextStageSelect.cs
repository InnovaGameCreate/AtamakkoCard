using UnityEngine;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class NextStageSelect : MonoBehaviour
    {
        /// <summary>
        /// �V�i���I�ōŌ�̃}�X�ɍs�����㎟�̕����X�e�[�W�֐i�ނ��ǂ���
        /// </summary>

        [SerializeField]
        private SceneObject[] nextScenes;

        public void goNestStage(int i)//���̃V�i���I�ֈړ�����
        {
            SceneManager.LoadScene(nextScenes[i]);
        }

        public void BackToTitle()//�^�C�g���V�[���ɖ߂�
        {
            SceneManager.LoadScene("Title");
        }
    }
}