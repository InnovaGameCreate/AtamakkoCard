using UnityEngine;
using UniRx;
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

        private readonly ReactiveProperty<int> _playerProgress = new ReactiveProperty<int>(1);
        public IReadOnlyReactiveProperty<int> PlayerProgress => _playerProgress;
        void Start()
        {
            GameManager = GameObject.Find("GameManager");
            Recoder = GameManager.GetComponent<ProgressRecorder>();
            Event = GameManager.GetComponent<StoryBoardEvent>();
            PlayerFarstPosition();

        }

        public void PlayerMove(int tileNum,Vector3 tileObjectPosition)
        {
            var ProgressCharacter = FindObjectOfType<ProgressRecorder>();
            var TileName = "tile (" + tileNum + ")";//�^�C���̖��O������
            var TileObject = GameObject.Find(TileName);//�^�C���̃I�u�W�F�N�g���擾
            if (TileObject == null)
            {
                Debug.LogError("�w�肵���^�C�������݂��܂���");
                return;
            }
            if (PlayerCanMoveDistance(tileObjectPosition))
            {
                this.transform.position = TileObject.transform.position;

                if (!ProgressCharacter.GetProgressed[tileNum])
                {
                    _playerProgress.Value = tileNum;

                    Event.startEvent(tileNum);//tile�̃C�x���g���s��
                }
                else
                {
                    Debug.LogError("���łɏ����ς݂̃^�C���ł�");
                }
            }
            else
            {
                Debug.LogError("�^�C���Ԃ̋������������܂�");
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

        //�v���C���[���ړ��\�ȋ����̏ꍇ��true�̒l��Ԃ�
        private bool PlayerCanMoveDistance(Vector3 moveTargetPosition)
        {
            var distance = Vector3.Distance(gameObject.transform.position, moveTargetPosition);
            if (distance < 180) return true;
            else return false;
        }
    }
}