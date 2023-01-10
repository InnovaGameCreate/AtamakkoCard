using UniRx;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// ボタンが押されたときにUIを表示するクラス
    /// </summary>
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private ButtonController buttonController; // 対象のボタン
        [SerializeField] private GameObject indicateObject; // 表示するUI
        void Start()
        {
            // ボタンが押されたときUIを表示する
            buttonController.Pushed
                .Subscribe(_ => indicateObject.SetActive(true))
                .AddTo(this);
        }
    }
}
