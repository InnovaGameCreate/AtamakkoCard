using UnityEngine;
using UnityEngine.UI;

namespace storyMode
{
    public class EventCheck : MonoBehaviour
    {
        /// <summary>
        /// 一度来たタイルの色を暗くする
        /// </summary>
        private bool EventYet = true;

        public void PlayerMove(int tileNum)
        {
            FindObjectOfType<StoryBoardPlayerMove>().GetComponent<StoryBoardPlayerMove>().PlayerMove(tileNum);
        }

        public void EventUsed()
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            EventYet = false;
        }

        public bool yet()
        {
            return EventYet;
        }
    }
}