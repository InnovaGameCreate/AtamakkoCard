using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace System.story
{
    public class NextStageSelect : MonoBehaviour
    {
        /// <summary>
        /// �V�i���I�ōŌ�̃}�X�ɍs�����㎟�̕����X�e�[�W�֐i�ނ��ǂ���
        /// </summary>

        [SerializeField] private SceneObject[] nextScenes;
        [SerializeField] private Button RedStorybutton;
        [SerializeField] private Button WhiteStorybutton;

        private void OnEnable()
        {
            StorySelect();
        }
        public void goNestStage(int i)//���̃V�i���I�ֈړ�����
        {
            SceneManager.LoadScene(nextScenes[i]);
        }

        public void BackToTitle()//�^�C�g���V�[���ɖ߂�
        {
            SceneManager.LoadScene("Title");
        }
        private void StorySelect()//�V�i���I�̐i�s�x��ۑ�
        {
            var ProgressInt = PlayerConfig.StoryProgress;
            switch (ProgressInt)
            {

                case 7:
                    WhiteStorybutton.interactable = false;
                    break;
                case 11:
                    RedStorybutton.interactable = false;
                    break;
                default:
                    break;
            }
        }
    }
}