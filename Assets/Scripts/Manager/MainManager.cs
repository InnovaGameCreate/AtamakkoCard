using UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField] private ButtonController toMatching;
        void Start()
        {
            toMatching.Pushed
                .Subscribe(_ => SceneManager.LoadScene("MatchingManager"))
                .AddTo(this);
        }
    }
}
