using UnityEngine;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class NextStageSelect : MonoBehaviour
    {
        /// <summary>
        /// シナリオで最後のマスに行った後次の複数ステージへ進むかどうか
        /// </summary>

        [SerializeField]
        private SceneObject[] nextScenes;

        public void goNestStage(int i)//次のシナリオへ移動する
        {
            SceneManager.LoadScene(nextScenes[i]);
        }

        public void BackToTitle()//タイトルシーンに戻る
        {
            SceneManager.LoadScene("Title");
        }
    }
}