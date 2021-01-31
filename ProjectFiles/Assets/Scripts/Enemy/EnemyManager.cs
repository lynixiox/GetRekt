using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPA
{
    public class EnemyManager : MonoBehaviour
    {

        public PlayerHealth playerhealth;
        public GameObject enemy;
        public float spawnTime = 3f;
        public Transform[] spawnPoints;
        // Use this for initialization
        void Start()
        {
            InvokeRepeating("Spawn", spawnTime, spawnTime);
            
        }

        void Spawn()
        {
            if (GetComponent<enemyCount>().Enemies < GetComponent<enemyCount>().EnemyMax)
            {
                if (playerhealth.health <= 0f)
                {
                    return;
                }

                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                GetComponent<enemyCount>().AddEnemy();
            }
        }

    }
}
