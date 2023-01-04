using UnityEngine;

namespace system.story
{
    public class StorySelect : MonoBehaviour
    {
        [SerializeField] private GameObject[] StoryBoard;//シナリオの選択肢
        private int _StoryProgress;//シナリオの進行具合
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