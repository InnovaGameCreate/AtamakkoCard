using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Atamakko
{
    /// <summary>
    /// アタマッコの内部情報
    /// </summary>
    public class AtamakkoData : MonoBehaviour
    {
        public UltimateState UltimateState { get; set; } = UltimateState.Normal; // 必殺技情報。基本はNormal。
        public int SpeedCorrection { get; set; } // 先制度補正
        public int DamageCorrection { get; set; } // ダメージ補正

        public ReactiveProperty<int> MyHp { get; set; } = new ReactiveProperty<int>(6); // HP

        [SerializeField] private int position; // 位置
        [SerializeField] private PlayerCharacterVisual equipment;

        public int MyPosition // 外部からアクセス可能な位置
        {
            get => position;
            set => position = value;
        }

        void Start()
        {
            // HPが6~0の値に収まる処理
            MyHp
                .Subscribe(hp =>
                {
                    if (hp >= 6) MyHp.Value = 6;
                    if (hp <= 0) MyHp.Value = 0;
                })
                .AddTo(this);
        }

        public void SetImage(int[] equipments)
        {
            equipment.SetImage(equipments);
        }
    }
}
