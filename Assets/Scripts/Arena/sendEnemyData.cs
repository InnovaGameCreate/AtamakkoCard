using UnityEngine;

namespace Arena
{

    public class sendEnemyData : MonoBehaviour
    {
        [SerializeField] RankerImage Images;
        private int enemyID = 0;
        private void Start()
        {
            SetEnemyData(36);
        }
        public void SetEnemyData(int ID)
        {
            Images.Init(ID);
            enemyID = ID;
        }
        public void SendData()
        {
            enemyDeckData.setDeckData(enemyID);
        }
    }
}