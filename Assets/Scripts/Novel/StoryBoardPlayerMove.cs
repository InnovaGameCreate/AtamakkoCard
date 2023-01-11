using UnityEngine;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class StoryBoardPlayerMove : MonoBehaviour
    {
        [SerializeField]
        private GameObject PlayerPosition;
        private GameObject GameManager;
        StoryBoardEvent Event;
        ProgressRecorder Recoder;
        void Start()
        {
            GameManager = GameObject.Find("GameManager");
            Recoder = GameManager.GetComponent<ProgressRecorder>();
            Event = GameManager.GetComponent<StoryBoardEvent>();
            PlayerFarstPosition();
        }

        public void PlayerMove(int tileNum)
        {
            var ProgressCharacter = FindObjectOfType<ProgressRecorder>();
            var TileName = "tile (" + tileNum + ")";//�^�C���̖��O������
            var TileObject = GameObject.Find(TileName);//�^�C���̃I�u�W�F�N�g���擾
            if (TileObject == null)
            {
                Debug.LogError("�w�肵���^�C�������݂��܂���");
                return;
            }
            var distance = Vector3.Distance(gameObject.transform.position, TileObject.transform.position);
            if (distance < 180)
            {
                this.transform.position = TileObject.transform.position;

                if (!ProgressCharacter.GetProgressed[tileNum])
                {
                    Event.startEvent(tileNum);//tile�̃C�x���g���s��
                    TileObject.GetComponent<EventCheck>().EventUsed(tileNum);//�^�C���̃C�x���g������̃A�N�V�������s��
                }
            }
        }

        private void PlayerFarstPosition()
        {
            if (PlayerConfig.LastPlayStory != SceneManager.GetActiveScene().name)
            {
                transform.position = PlayerPosition.transform.position;
            }
            else
            {
                transform.position = GameObject.Find("tile (" + Recoder.GetPlayerLastProgressed + ")").transform.position;
            }
        }

        public void nextPosition(Vector3 movePositionObject)
        {
            transform.position = movePositionObject;
        }
    }
}