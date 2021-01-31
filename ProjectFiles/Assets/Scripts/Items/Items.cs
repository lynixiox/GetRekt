using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPA
{
    public class Items : MonoBehaviour
    {
        public int amountOfHealth;
        public int timeOfInstaKill;
        public int EggScore = 50; 
        public enum ItemType
        {
            Gold,
            Health,
            InstaDeath,
            Kaboom
        }
        public ItemType itemType;
        // Use this for initialization
        void Start()
        {
           // Destroy(this.gameObject, 20);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("sd");
                if (itemType == ItemType.Health)
                    other.GetComponent<PlayerHealth>().AddHealth(amountOfHealth);
                else if (itemType == ItemType.Kaboom)
                    foreach (GameObject u in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        u.GetComponent<EnemyHealth>().TakeDamage(10000);
                    }
                else if (itemType == ItemType.Gold)
                {
                    //////ADD TO FUCKING SCORE
                    GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>().Score += EggScore;
                    Debug.Log("Score added");
                }
                GameObject.Find("ItemDropManager").GetComponent<DropItemsOnMesh>().currentObjects--; 
                Destroy(this.gameObject);
            }
        }
    }
}