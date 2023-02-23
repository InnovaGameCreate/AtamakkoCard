using UnityEngine;
using UnityEngine.UI;

namespace storyMode
{
    public class EventCheck : MonoBehaviour
    {
        /// <summary>
        /// 一度来たタイルの色を暗くする
        /// </summary>

        public void PlayerMove(int tileNum)
        {
            FindObjectOfType<StoryBoardPlayerMove>().GetComponent<StoryBoardPlayerMove>().PlayerMove(tileNum);
        }

        public void EventUsed(int tileNum)
        {
            Debug.Log("tile" + tileNum + "の処理を行いました");
            GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            FindObjectOfType<ProgressRecorder>().completeEvent(tileNum);
        }

    }
}