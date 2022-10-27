using System;
using Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Field
{
    public class UltimateSelect : MonoBehaviour
    {
        [SerializeField] private GameObject uiUltimate;
        [SerializeField] private  AtamakkoStatus.Ultimate ultimate;
        [SerializeField] private AtamakkoStatus playerStatus;

        private Button _bUltimate;

        private void Start()
        {
            _bUltimate = GetComponent<Button>();
            _bUltimate.interactable = playerStatus.UState == ultimate;
            _bUltimate.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    playerStatus.UState = ultimate;
                    uiUltimate.SetActive(false);
                })
                .AddTo(this);
        }

        private void Update()
        {
            _bUltimate.interactable = playerStatus.UState != ultimate;
        }
    }
}
