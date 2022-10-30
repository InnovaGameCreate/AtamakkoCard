using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Manager
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance;
        
        [SerializeField] private GameObject pUltimateCutIn;
        private PlayableDirector _pUltimateDirector;
        [SerializeField] private GameObject eUltimateCutIn;
        private PlayableDirector _eUltimateDirector;

        private bool _winner;
        [SerializeField] private GameObject backgroundPanel;
        private PlayableDirector _resultIn;
        [SerializeField] private GameObject winnerImage;
        private PlayableDirector _winnerIn;
        [SerializeField] private GameObject loserImage;
        private PlayableDirector _loserIn;
        [SerializeField] private GameObject lobbyButton;

        [SerializeField] private GameObject attackEffect;
        private GameObject _aEffectPrefab;
        private PlayableDirector _attackIn;
        [SerializeField] private GameObject[] aSlots;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _pUltimateDirector = pUltimateCutIn.GetComponent<PlayableDirector>();
            _eUltimateDirector = eUltimateCutIn.GetComponent<PlayableDirector>();

            _resultIn = backgroundPanel.GetComponent<PlayableDirector>();
            _resultIn.stopped += Result_Started;
            _winnerIn = winnerImage.GetComponent<PlayableDirector>();
            _winnerIn.stopped += Result_Stopped;
            _loserIn = loserImage.GetComponent<PlayableDirector>();
            _loserIn.stopped += Result_Stopped;
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

        public async void ResultFadeIn(bool result)
        {
            _winner = result;
            backgroundPanel.SetActive(true);
            await UniTask.DelayFrame(1);
            _resultIn.Play();
        }

        public async UniTask AttackEffect(int num)
        {
            var token = this.GetCancellationTokenOnDestroy();
            _aEffectPrefab = Instantiate(attackEffect, aSlots[num].transform.position, Quaternion.identity, transform);
            _attackIn = _aEffectPrefab.GetComponent<PlayableDirector>();
            await UniTask.DelayFrame(1, cancellationToken: token);
            _attackIn.Play();
        }

        private async void Result_Started(PlayableDirector obj)
        {
            if (_winner)
            {
                winnerImage.SetActive(true);
                await UniTask.DelayFrame(1);
                _winnerIn.Play();
            }
            else
            {
                loserImage.SetActive(true);
                await UniTask.DelayFrame(1);
                _loserIn.Play();
            }
        }

        private void Result_Stopped(PlayableDirector obj)
        {
            lobbyButton.SetActive(true);
        }
    }
}
