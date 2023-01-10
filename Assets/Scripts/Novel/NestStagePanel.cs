using UnityEngine;
using UnityEngine.SceneManagement;

namespace System.story
{
    public class NestStagePanel : MonoBehaviour
    {
        /// <summary>
        /// シナリオで最後のマスに行った後次のステージへ進むかどうか
        /// </summary>

        [SerializeField] private SceneObject nextScene;
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void goNestStage()
        {
            SceneManager.LoadScene(nextScene);
            completeStage();
        }

        public void BackToTitle()
        {
            SceneManager.LoadScene("Title");
            completeStage();
        }

        private void completeStage()//シナリオの進行度を保存
        {
            var CurrentScene = SceneManager.GetActiveScene().name;
            int ProgressInt = 0;
            switch (CurrentScene)
            {

                case "StoryBoard1":
                    ProgressInt = 1;
                    break;
                case "StoryBoard2":
                    ProgressInt = 2;
                    break;
                case "StoryBoardBlue1":
                    ProgressInt = 3;
                    break;
                case "StoryBoardBlue2":
                    ProgressInt = 4;
                    break;
                case "StoryBoardBlue3":
                    ProgressInt = 5;
                    break;
                case "StoryBoardEnd1":
                    ProgressInt = 6;
                    break;
                case "StoryBoardRed1":
                    ProgressInt = 7;
                    break;
                case "StoryBoardRed2":
                    ProgressInt = 8;
                    break;
                case "StoryBoardRed3":
                    ProgressInt = 9;
                    break;
                case "StoryBoardEnd2":
                    ProgressInt = 10;
                    break;
                case "StoryBoardWhite1":
                    ProgressInt = 11;
                    break;
                case "StoryBoardWhite2":
                    ProgressInt = 12;
                    break;
                case "StoryBoardWhite3":
                    ProgressInt = 13;
                    break;
                case "StoryBoardEnd3":
                    ProgressInt = 14;
                    break;
                default:
                    break;
            }
            PlayerConfig.StoryProgress = ProgressInt;
            PlayerConfig.SetData();
        }
    }
}