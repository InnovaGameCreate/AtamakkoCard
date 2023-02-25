using UnityEngine;
using UnityEngine.UI;

namespace storyMode
{
    public class EventCheck : MonoBehaviour
    {
        /// <summary>
        /// 一度来たタイルの色を暗くする
        /// </summary>
        [SerializeField]
        private GameObject TileObject;

        public void PlayerMove(int tileNum)
        {
            FindObjectOfType<StoryBoardPlayerMove>().GetComponent<StoryBoardPlayerMove>().PlayerMove(tileNum);
        }

        public void EventUsed(int tileNum)
        {
            Debug.Log("tile" + tileNum + "の処理を行いました");
            Used();
            FindObjectOfType<ProgressRecorder>().completeEvent(tileNum);
        }

        public void Used()
        {
            GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
            if(TileObject != null)
            {
                Destroy(TileObject);
            }
        }

    }
}