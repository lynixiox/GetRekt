using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MPA
{
    public class EnemyMovement : MonoBehaviour
    {

        public Transform player;
        PlayerHealth playerHealth; //reference to players health//
        EnemyHealth enemyHealth; //reference to enemy health//
        NavMeshAgent nav;
        public float Distance;
        public float EvenLongerDistance;
        private float cooldown = 0;
        private float longCooldown = 0;
        private float evenLongerCooldown = 0;
        private float gameTime = 0;
        // Use this for initialization
        void Awake()
        {
            player = GameObject.Find("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
        }

        // Update is called once per frame
        void Update()
        {
            longCooldown -= Time.deltaTime;
            evenLongerCooldown -= Time.deltaTime;


            if (cooldown <= 0)
            {
                cooldown = 0.1f;
                if(Vector3.Distance(player.position, transform.position) < Distance)
                {
                    Debug.Log("Yay");
                    RecalculateNavMesh();
                }
                else if(Vector3.Distance(player.position, transform.position) < EvenLongerDistance)
                {
                    if (longCooldown <= 0)
                    {
                        longCooldown = 0.2f;
                        RecalculateNavMesh();
                    }

                }
                else
                {
                    if (evenLongerCooldown <= 0)
                    {
                        evenLongerCooldown = 0.8f;
                        RecalculateNavMesh();
                    }
                }

            }
            else
                cooldown -= Time.deltaTime;
            

        }
        public void RecalculateNavMesh()
        {
            if (enemyHealth.currentHealth > 0)
            {
                nav.SetDestination(player.position); //This causes laggg hey?~

            }
            else
            {
                //  GetComponent<EnemyHealth>().StartSinking();
                // GetComponent<EnemyHealth>().isDead = true; 
                Debug.Log("Dead Bynny");
                nav.enabled = false;
            }
        }
    }
}
