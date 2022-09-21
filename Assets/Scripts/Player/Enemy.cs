using UnityEngine;

namespace Player
{
    public class Enemy : MonoBehaviour, IDamagable, IMobile
    {
        private AtamakkoStatus _enemyStatus;
        [SerializeField] private GameObject[] sSlot;

        private void Start()
        {
            _enemyStatus = gameObject.GetComponent<AtamakkoStatus>();
        }

        public void AddDamage(int damage)
        {
            _enemyStatus.MyHp.Value -= damage;
        }

        public void Move(int slotNum)
        {
            var position = (slotNum + 3) % 6;
            gameObject.transform.SetParent(sSlot[position].transform);
            _enemyStatus.MyPosition = position;
        }
    }
}
