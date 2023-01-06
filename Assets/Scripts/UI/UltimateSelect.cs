using Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Field
{
    public class UltimateSelect : MonoBehaviour
    {
        [SerializeField] private GameObject uiUltimate;
        [SerializeField] private  UltimateState ultimate;
        [SerializeField] private AtamakkoData playerData;

        private Button _bUltimate;

        private void Start()
        {
            _bUltimate = GetComponent<Button>();
            _bUltimate.interactable = playerData.UltimateState == ultimate;
            _bUltimate.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    playerData.UltimateState = ultimate;
                    uiUltimate.SetActive(false);
                })
                .AddTo(this);
        }

        private void Update()
        {
            _bUltimate.interactable = playerData.UltimateState != ultimate;
        }
    }
}
