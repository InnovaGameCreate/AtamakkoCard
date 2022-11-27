using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{

    public class sendEnemyData : MonoBehaviour
    {
        public void sendData(int enemyID)
        {
            enemyDeckData.setDeckData(enemyID);
        }
    }
}