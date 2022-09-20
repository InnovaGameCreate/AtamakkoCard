using Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Field
{
    public class UltimateSelect : MonoBehaviour
    {
        [SerializeField] private GameObject uiUltimate;
        [SerializeField] private readonly AtamakkoStatus.Ultimate _ultimate;
        [SerializeField] private AtamakkoStatus playerStatus;

        private Button _bUltimate;

        private void Start()
        {
            _bUltimate = GetComponent<Button>();

            _bUltimate.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    playerStatus.UState = playerStatus.UState == _ultimate ? AtamakkoStatus.Ultimate.Normal : _ultimate;

                    uiUltimate.SetActive(false);
                })
                .AddTo(this);
        }
    }
}
