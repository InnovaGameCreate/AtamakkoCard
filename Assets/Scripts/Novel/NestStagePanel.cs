using UnityEngine;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class NestStagePanel : MonoBehaviour
    {
        /// <summary>
        /// シナリオで最後のマスに行った後次のステージへ進むかどうか
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