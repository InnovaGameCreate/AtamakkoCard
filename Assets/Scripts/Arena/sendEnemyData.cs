using UnityEngine;

namespace Arena
{

    public class sendEnemyData : MonoBehaviour
    {
        private int enemyID = 0;
        public void SetEnemyData(int ID)
        {
            enemyID = ID;
        }
        public void SendData()
        {
            enemyDeckData.setDeckData(enemyID);
        }
    }
}