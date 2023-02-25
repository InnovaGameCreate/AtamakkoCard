using UnityEngine;
using UnityEngine.UI;

namespace storyMode
{
    public class EventCheck : MonoBehaviour
    {
        /// <summary>
        /// ��x�����^�C���̐F���Â�����
        /// </summary>
        [SerializeField]
        private GameObject TileObject;

        public void PlayerMove(int tileNum)
        {
            FindObjectOfType<StoryBoardPlayerMove>().GetComponent<StoryBoardPlayerMove>().PlayerMove(tileNum);
        }

        public void EventUsed(int tileNum)
        {
            Debug.Log("tile" + tileNum + "�̏������s���܂���");
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