using System.Audio;
using Atamakko;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 必殺技を選択するUIクラス
    /// </summary>
    public class UltimateSelect : MonoBehaviour
    {
        [SerializeField] private GameObject uiUltimate; // 必殺技を選ぶUI
        [SerializeField] private  UltimateState ultimate; // 設定された必殺技
        [SerializeField] private AtamakkoData playerData; // 設定先のアタマッコ

        private Button _bUltimate; // 自身のボタン
        [SerializeField] private SeType se; // SE

        private void Start()
        {
            _bUltimate = GetComponent<Button>();
            _bUltimate.interactable = playerData.UltimateState == ultimate;
            // ボタンを推した時に必殺技の種類を変更する
            _bUltimate.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    playerData.UltimateState = ultimate;
                    SeManager.Instance.ShotSe(se);
                    uiUltimate.SetActive(false);
                })
                .AddTo(this);
        }
        
        private void Update()
        {
            //必殺技が選択済みなら押せないように
            _bUltimate.interactable = playerData.UltimateState != ultimate;
        }
    }
}
