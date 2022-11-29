using UnityEngine;
using UnityEngine.UI;

namespace storyMode
{
    public class EventCheck : MonoBehaviour
    {
        /// <summary>
        /// ��x�����^�C���̐F���Â�����
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