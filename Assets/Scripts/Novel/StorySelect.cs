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
                StoryBoard[i].SetActive(true);//デバック用
                Debug.LogError("デバック用のスクリプトを走らせています。");
                /*
                if (i <= _StoryProgress) StoryBoard[i].SetActive(true);
                else StoryBoard[i].SetActive(false);
                */
            }
        }
    }
}