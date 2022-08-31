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
        [NonSerialized] public int AttackPlace = 0;
        [SerializeField] private GameObject stage;
        [SerializeField] private AttackButton attackButton;
        
        public readonly Subject<bool> ASelected = new Subject<bool>();
        public IObservable<bool> AttackSelected => ASelected;

        void Start()
        {
            _atamakkoStatus = gameObject.GetComponent<AtamakkoStatus>();
        }

        public async UniTask Attack(CardModel card)
        {
            if (card.Kind == "攻撃")
            {
                for (int i = 0; i < card.Attack.Length; i++)
                {
                    if (card.Attack[i] == "〇")
                    {
                        var toPlace = (i + _atamakkoStatus.MyPosition) % 6;
                        var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                        attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 60 * toPlace);
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
            Debug.Log("攻撃 "+AttackPlace);
        }
    }
}
