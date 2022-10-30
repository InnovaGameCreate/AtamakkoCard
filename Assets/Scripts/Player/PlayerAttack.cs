using System;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using Manager;
using Photon.Pun;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviourPunCallbacks, IDamagable
    {
        private AtamakkoStatus _atamakkoStatus;
        private AtamakkoStatus _enemyStatus;
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
            _atamakkoStatus = gameObject.GetComponent<AtamakkoStatus>();
            _enemyStatus = enemy.GetComponent<AtamakkoStatus>();
            _enemy = enemy.GetComponent<Enemy>();
        }

        public async UniTask AttackSelect(CardModel card, int initiative)
        {
            if (card.Kind == "攻撃" && card.Initiative == initiative)
            {
                for (int i = 0; i < card.Attack.Length; i++)
                {
                    if (card.Attack[i] == "〇")
                    {
                        var toPlace = (i + _atamakkoStatus.MyPosition) % 6;
                        var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                        attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * toPlace);
                        attackArea.AttackPlace = toPlace;
                        attackArea.PlayerAttack = this;
                    }
                }
                await ASelected.ToUniTask(true);
                _attacked = true;
                _damage = card.Damage;
                if (_atamakkoStatus.UState == AtamakkoStatus.Ultimate.Attack)
                {
                    _damage += 1;
                }
            }
        }

        public void AttackDamage()
        {
            if (_attacked && _enemyStatus.MyPosition == AttackPlace)
            {
                _enemy.AddDamage(_damage);
                photonView.RPC(nameof(AddDamage), RpcTarget.Others, _damage);
                
                AttackAnimation(AttackPlace);
                photonView.RPC(nameof(AttackAnimation), RpcTarget.Others, (AttackPlace + 3) % 6);
            }
            _attacked = false;
        }

        [PunRPC]
        public void AddDamage(int damage)
        {
            _atamakkoStatus.MyHp.Value -= damage;
        }
        
        [PunRPC]
        private async void AttackAnimation(int pos)
        {
            await AnimationManager.Instance.AttackEffect(pos);
        }
    }
}
