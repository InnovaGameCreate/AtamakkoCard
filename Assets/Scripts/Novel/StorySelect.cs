using UnityEngine;

namespace system.story
{
    public class StorySelect : MonoBehaviour
    {
        [SerializeField] private GameObject[] StoryBoard;//�V�i���I�̑I����
        private int _StoryProgress;//�V�i���I�̐i�s�
        void Start()
        {
            _StoryProgress = PlayerConfig.StoryProgress;
            for (int i = 0; i < StoryBoard.Length; i++)
            {
                if (i <= _StoryProgress) StoryBoard[i].SetActive(true);
                else StoryBoard[i].SetActive(false);
            }
        }
    }
}