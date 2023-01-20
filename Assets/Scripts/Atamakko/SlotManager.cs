using System;
using UI;
using UniRx;
using UnityEngine;

namespace Atamakko
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private PlayerCore player; // プレイヤー
        [SerializeField] private EnemyCore enemy;
        [SerializeField] private ButtonController replaceButton; // 自身の場所を下にするボタン
        [SerializeField] private GameObject[] areaUIs = new GameObject[6]; // 予測UIの配列
        private int _replace;

        private void Start()
        {
            replaceButton.Pushed
                .Subscribe(_ => Replace())
                .AddTo(this);
        }

        private void Replace()
        {
            var rotate = _replace - player.AtamakkoData.MyPosition;
            foreach (var area in areaUIs)
            {
                if (rotate != 0)
                {
                    area.transform.rotation = Quaternion.Euler(0f, 0f, 60f * rotate);
                    _replace = player.AtamakkoData.MyPosition;
                }
                else
                {
                    area.transform.rotation = Quaternion.Euler(0f, 0f, -60f * _replace);
                    _replace = 0;
                }
            }
        }
    }
}
