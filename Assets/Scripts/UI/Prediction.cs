using Atamakko;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 間合いの予測表示を管理するクラス
    /// </summary>
    public class Prediction : MonoBehaviour
    {
        [SerializeField] private GameObject areaUI; // 予測表示するUI
        [SerializeField] private PlayerCore player; // プレイヤー
        private readonly GameObject[] _areaUIs = new GameObject[6]; // 予測UIの配列
        private readonly Image[] _areaImages = new Image[6]; // 予測UIImageの配列
        
        void Start()
        {
            // UIの初期化
            for (int i = 0; i < 6; i++)
            {
                var transform1 = transform;
                _areaUIs[i] = Instantiate(areaUI, transform1.position , Quaternion.identity, transform1);
                _areaUIs[i].transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * i);
                _areaUIs[i].SetActive(false);
                _areaImages[i] = _areaUIs[i].GetComponentInChildren<Image>();
            }
        }

        /// <summary>
        /// 間合いを表示する。
        /// </summary>
        /// <param name="place">間合いの位置</param>
        /// <param name="isAttack">攻撃カードかどうか</param>
        public void Show(int place, bool isAttack)
        {
            int i = (place + player.AtamakkoData.MyPosition) % 6;
            _areaUIs[i].SetActive(true);
            _areaImages[i].color = isAttack ? new Color(1f, 0f, 0f, 0.5f) : new Color(0f, 1f, 0f, 0.5f);
        }

        /// <summary>
        /// 表示を消す。
        /// </summary>
        public void Hide()
        {
            foreach (var area in _areaUIs)
            {
                area.SetActive(false);
            }
        }
    }
}
