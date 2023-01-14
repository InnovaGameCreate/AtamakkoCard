using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Option : MonoBehaviour
    {
        [SerializeField] private ButtonController surrenderButton; // 降参ボタン
        [SerializeField] private ButtonController optionButton; // オプションボタン
        [SerializeField] private GameObject optionContent; // オプションUI
        public static Option Instance { get; private set; } // インスタンス化

        // 降参時に発行するイベント
        private readonly Subject<bool> _surrender = new Subject<bool>();
        public IObservable<bool> Surrender => _surrender;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            optionButton.Pushed
                .Subscribe(_ =>
                {
                    optionContent.SetActive(true);
                    surrenderButton.gameObject.SetActive(
                        SceneManager.GetActiveScene().name is "BattleCPU" or "BattleOnline");
                })
                .AddTo(this);
            
            surrenderButton.Pushed
                .Subscribe(_ =>
                {
                    _surrender.OnNext(true);
                    optionContent.SetActive(false);
                })
                .AddTo(this);
        }
    }
}
