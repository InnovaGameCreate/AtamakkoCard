using System;
using System.Effect;
using Atamakko;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Manager
{
    /// <summary>
    /// アニメーションを管理するクラス
    /// </summary>
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance; // インスタンス
        
        // 必殺技のカットイン
        [SerializeField] private GameObject pUltimateCutIn;
        private PlayableDirector _pUltimateDirector;
        [SerializeField] private GameObject eUltimateCutIn;
        private PlayableDirector _eUltimateDirector;

        // 勝敗時のアニメーション
        private bool _winner;
        [SerializeField] private GameObject backgroundPanel;
        private PlayableDirector _resultIn;
        [SerializeField] private GameObject winnerImage;
        private PlayableDirector _winnerIn;
        [SerializeField] private GameObject loserImage;
        private PlayableDirector _loserIn;
        [SerializeField] private GameObject lobbyButton;

        // 攻撃のアニメーション
        [SerializeField] private GameObject attackEffect;
        private GameObject _aEffectPrefab;
        private PlayableDirector _attackIn;
        [SerializeField] private GameObject[] aSlots;

        private void Awake()
        {
            // シングルトン化
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

        /// <summary>
        /// 自身の必殺技のカットイン
        /// </summary>
        /// <param name="ultimateType">必殺技の種類</param>
        public void MyUltimateCutIn(UltimateState ultimateType)
        {
            //var token = this.GetCancellationTokenOnDestroy();
            //_pUltimateDirector.Play();

            switch (ultimateType)//使用するアルティメットに対応するエフェクトを再生する。
            {
                case UltimateState.Recover:
                    EffectManager.Instance.InstantiateEffect(EffectType.specialHeal, transform);//体力回復アルティメットの使用エフェクト
                    break;
                case UltimateState.Attack:
                    EffectManager.Instance.InstantiateEffect(EffectType.specialDamageUp, transform);//攻撃力上昇アルティメットの使用エフェクト
                    break;
                case UltimateState.Speed:
                    EffectManager.Instance.InstantiateEffect(EffectType.specialSpeedUp, transform);//先制度上昇アルティメットの使用エフェクト
                    break;
            }
            //await UniTask.Delay(TimeSpan.FromSeconds(_pUltimateDirector.duration), cancellationToken:token);
        }
        
        /// <summary>
        /// 敵の必殺技のカットイン
        /// </summary>
        public async UniTask EnUltimateCutIn()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _eUltimateDirector.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(_eUltimateDirector.duration), cancellationToken:token);
        }

        /// <summary>
        /// 勝敗結果の挿入
        /// </summary>
        /// <param name="result">勝敗</param>
        public async void ResultFadeIn(bool result)
        {
            _winner = result;
            backgroundPanel.SetActive(true);
            await UniTask.DelayFrame(1);
            _resultIn.Play();
        }

        /// <summary>
        /// 攻撃エフェクト
        /// </summary>
        /// <param name="num">攻撃位置</param>
        public async UniTask AttackEffect(int num)
        {
            var token = this.GetCancellationTokenOnDestroy();
            _aEffectPrefab = Instantiate(attackEffect, aSlots[num].transform.position, Quaternion.identity, transform);
            _attackIn = _aEffectPrefab.GetComponent<PlayableDirector>();
            await UniTask.DelayFrame(1, cancellationToken: token);
            _attackIn.Play();
        }

        /// <summary>
        /// 勝敗結果エフェクトのスタート
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// 勝敗結果エフェクト終了後にロビーに戻るボタン配置
        /// </summary>
        /// <param name="obj"></param>
        private void Result_Stopped(PlayableDirector obj)
        {
            lobbyButton.SetActive(true);
        }
    }
}
