using UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    /// <summary>
    /// マッチングシーンへ行くクラス
    /// </summary>
    public class MainManager : MonoBehaviour
    {
        [SerializeField] private ButtonController toMatching; // マッチングシーンへ行くボタン
        void Start()
        {
            // ボタンが押されたらマッチングシーンへ
            toMatching.Pushed
                .Subscribe(_ => SceneManager.LoadScene("Matching"))
                .AddTo(this);
        }
    }
}
