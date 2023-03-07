using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace storyMode
{
    public class EventCheck : MonoBehaviour
    {
        /// <summary>
        /// ��x�����^�C���̐F���Â�����
        /// </summary>
        [SerializeField]
        private GameObject TileObject;
        StoryBoardPlayerMove playerMove;

        public void PlayerMove(int tileNum)
        {
            playerMove = FindObjectOfType<StoryBoardPlayerMove>().GetComponent<StoryBoardPlayerMove>();
            playerMove.PlayerMove(tileNum,gameObject.transform.position);


            playerMove.PlayerProgress
                .Subscribe(PlayerProgress =>
                {
                    Used();
                });
        }

        //�O������g�p�ς݃^�C���̐F�ɕύX
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