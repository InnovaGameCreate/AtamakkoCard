using UnityEngine;

namespace Player
{
    public class Enemy : MonoBehaviour, IMobile
    {
        private AtamakkoData _enemyData;
        [SerializeField] private GameObject[] sSlot;

        private void Start()
        {
            _enemyData = gameObject.GetComponent<AtamakkoData>();
        }

        public void AddDamage(int damage, int a)
        {
            _enemyData.MyHp.Value -= damage;
        }

        public void Move(int slotNum)
        {
            var position = (slotNum + 3) % 6;
            gameObject.transform.SetParent(sSlot[position].transform);
            _enemyData.MyPosition = position;
        }
    }
}
