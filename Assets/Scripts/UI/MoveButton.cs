using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MoveButton : MonoBehaviour
    {
        private int _place;

        private Button _myButton;

        public Player.PlayerMove playerMove;

        public int MyPlace
        {
            set => _place = value;
        }

        void Start()
        {
            _myButton = gameObject.GetComponent<Button>();

            _myButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    playerMove.Position = _place;
                    playerMove.MSelected.OnNext(true);
                })
                .AddTo(this);

            playerMove.MoveSelected
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
