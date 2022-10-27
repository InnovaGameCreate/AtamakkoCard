using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Manager
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private GameObject pUltimateCutIn;
        private PlayableDirector _pUltimateDirector;

        [SerializeField] private GameObject eUltimateCutIn;
        private PlayableDirector _eUltimateDirector;

        private void Start()
        {
            _pUltimateDirector = pUltimateCutIn.GetComponent<PlayableDirector>();
            
            _eUltimateDirector = eUltimateCutIn.GetComponent<PlayableDirector>();
        }

        public async UniTask MyUltimateCutIn()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _pUltimateDirector.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(_pUltimateDirector.duration), cancellationToken:token);
        }
        
        public async UniTask EnUltimateCutIn()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _eUltimateDirector.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(_eUltimateDirector.duration), cancellationToken:token);
        }
    }
}
