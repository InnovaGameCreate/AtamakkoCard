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
            var TileName = "tile (" + tileNum + ")";//タイルの名前を検索
            var TileObject = GameObject.Find(TileName);//タイルのオブジェクトを取得
            if (TileObject == null)
            {
                Debug.LogError("指定したタイルが存在しません");
                return;
            }
            if (PlayerCanMoveDistance(tileObjectPosition))
            {
                this.transform.position = TileObject.transform.position;

                if (!ProgressCharacter.GetProgressed[tileNum])
                {
                    _playerProgress.Value = tileNum;

                    Event.startEvent(tileNum);//tileのイベントを行う
                }
                else
                {
                    Debug.LogError("すでに処理済みのタイルです");
                }
            }
            else
            {
                Debug.LogError("タイル間の距離が遠すぎます");
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

        //プレイヤーが移動可能な距離の場合はtrueの値を返す
        private bool PlayerCanMoveDistance(Vector3 moveTargetPosition)
        {
            var distance = Vector3.Distance(gameObject.transform.position, moveTargetPosition);
            if (distance < 180) return true;
            else return false;
        }
    }
}