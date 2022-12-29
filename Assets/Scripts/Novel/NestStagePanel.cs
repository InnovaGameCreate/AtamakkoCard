using UnityEngine;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class NestStagePanel : MonoBehaviour
    {
        /// <summary>
        /// �V�i���I�ōŌ�̃}�X�ɍs�����㎟�̃X�e�[�W�֐i�ނ��ǂ���
        /// </summary>

        [SerializeField] private SceneObject nextScene;

        public void goNestStage()
        {
            SceneManager.LoadScene(nextScene);
        }

        public void BackToTitle()
        {
            SceneManager.LoadScene("Title");
        }
    }
}