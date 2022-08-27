using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

namespace Field
{
    public class PButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private int _place;
        private Button _myButton;

        public Player player;

        public int MyPlace
        {
            set => _place = value;
        }

        void Start()
        {
            _myButton = GetComponent<Button>();

            _myButton.OnClickAsObservable()
                .Subscribe(_ =>player.MyPosition = _place)
                .AddTo(this);

            player.Moved
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
