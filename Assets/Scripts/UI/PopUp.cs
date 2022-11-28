using UniRx;
using UnityEngine;

namespace UI
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private ButtonController buttonController;
        [SerializeField] private GameObject indicateObject;
        void Start()
        {
            buttonController.Pushed
                .Subscribe(_ => indicateObject.SetActive(true))
                .AddTo(this);
        }
    }
}
