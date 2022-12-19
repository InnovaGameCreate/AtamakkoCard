using System;
using System.Collections;
using Card;
using Manager;
using Photon.Pun;
using UI;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviourPunCallbacks, IDamagable
    {
        private AtamakkoData _atamakkoData;
        private AtamakkoData _enemyData;
        private Enemy _enemy;
        private bool _attacked;
        private int _damage;
        [NonSerialized] public int AttackPlace = 0;
        [SerializeField] private GameObject enemy;
        [SerializeField] private GameObject stage;
        [SerializeField] private AttackButton attackButton;
        
        public readonly Subject<bool> ASelected = new Subject<bool>();
        public IObservable<bool> AttackSelected => ASelected;

        void Start()
        {
            _atamakkoData = gameObject.GetComponent<AtamakkoData>();
            _enemyData = enemy.GetComponent<AtamakkoData>();
            _enemy = enemy.GetComponent<Enemy>();
        }

        public IEnumerator AttackSelect(CardModel card, int initiative)
        {
            for (int i = 0; i < card.Attack.Length; i++)
            {
                if (card.Attack[i] == "〇")
                {
                    var toPlace = (i + _atamakkoData.MyPosition) % 6;
                    var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                    attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * toPlace);
                    attackArea.AttackPlace = toPlace;
                }
            }

            yield return TimeCounter.Instance.Timer.ToYieldInstruction();
            _attacked = true;
            _damage = card.Damage;
            if (_atamakkoData.UltimateState == UltimateState.Attack)
            {
                _damage += 1;
            }
        }

        public void AttackDamage()
        {
            if (_attacked && _enemyData.MyPosition == AttackPlace)
            {
                _enemy.AddDamage(_damage, _damage);
                photonView.RPC(nameof(AddDamage), RpcTarget.Others, _damage);
                
                AttackAnimation(AttackPlace);
                photonView.RPC(nameof(AttackAnimation), RpcTarget.Others, (AttackPlace + 3) % 6);
            }
            _attacked = false;
        }

        [PunRPC]
        public void AddDamage(int damage, int position)
        {
            _atamakkoData.MyHp.Value -= damage;
        }
        
        [PunRPC]
        private async void AttackAnimation(int pos)
        {
            await AnimationManager.Instance.AttackEffect(pos);
        }
    }
}
