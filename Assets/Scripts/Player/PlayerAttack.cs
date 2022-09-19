using System;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private AtamakkoStatus _atamakkoStatus;
        [SerializeField] private AtamakkoStatus enemyStatus;
        [NonSerialized] public int AttackPlace = 0;
        [SerializeField] private GameObject stage;
        [SerializeField] private AttackButton attackButton;
        
        public readonly Subject<bool> ASelected = new Subject<bool>();
        public IObservable<bool> AttackSelected => ASelected;

        void Start()
        {
            _atamakkoStatus = gameObject.GetComponent<AtamakkoStatus>();
        }

        public async UniTask Attack(CardModel card, int initiative)
        {
            if (card.Kind == "攻撃" && card.Initiative == initiative)
            {
                for (int i = 0; i < card.Attack.Length; i++)
                {
                    if (card.Attack[i] == "〇")
                    {
                        Debug.Log("攻撃可能場所 " + i);
                        var toPlace = (i + _atamakkoStatus.MyPosition) % 6;
                        var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                        attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * toPlace);
                        attackArea.AttackPlace = toPlace;
                        attackArea.PlayerAttack = this;
                    }
                }
                await ASelected.ToUniTask(true);
                Attack(card.Damage);
            }
        }

        private void Attack(int damage)
        {
            Debug.Log("自分の位置 " + _atamakkoStatus.MyPosition);
            Debug.Log("敵の位置 " + enemyStatus.MyPosition);
            Debug.Log("攻撃位置 " + AttackPlace);
            if (enemyStatus.MyPosition == AttackPlace)
            {
                Debug.Log("ダメージ " + damage);
                enemyStatus.MyHp.Value -= damage;
            }
        }
    }
}
