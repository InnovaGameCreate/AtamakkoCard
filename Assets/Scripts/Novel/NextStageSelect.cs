using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace System.story
{
    public class NextStageSelect : MonoBehaviour
    {
        /// <summary>
        /// シナリオで最後のマスに行った後次の複数ステージへ進むかどうか
        /// </summary>

        [SerializeField] private SceneObject[] nextScenes;
        [SerializeField] private Button RedStorybutton;
        [SerializeField] private Button WhiteStorybutton;

        private void OnEnable()
        {
            StorySelect();
        }
        public void goNestStage(int i)//次のシナリオへ移動する
        {
            SceneManager.LoadScene(nextScenes[i]);
        }

        public void BackToTitle()//タイトルシーンに戻る
        {
            SceneManager.LoadScene("Title");
        }
        private void StorySelect()//シナリオの進行度を保存
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