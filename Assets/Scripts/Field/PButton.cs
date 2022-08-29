using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Button = UnityEngine.UI.Button;

namespace Field
{
    public class PButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private int _place;
        private Button _myButton;

        [FormerlySerializedAs("atamakko")] [FormerlySerializedAs("player")] public Player.PlayerMove playerMove;

        public int MyPlace
        {
            set => _place = value;
        }

        void Start()
        {
            _myButton = GetComponent<Button>();

            _myButton.OnClickAsObservable()
                .Subscribe(_ =>playerMove.MyPosition = _place)
                .AddTo(this);

            playerMove.Moved
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            _myButton.interactable = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _myButton.interactable = false;
        }
    }
}
