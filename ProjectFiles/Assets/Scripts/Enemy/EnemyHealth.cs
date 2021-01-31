using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MPA
{
    public class EnemyHealth : MonoBehaviour
    {

        public int startingHealth = 100;
        public int currentHealth;
        public float sinkSpeed = 2.5f;
        public bool superDead = false; 
        public int scoreValue = 10;

        Animator anim;
        CapsuleCollider capsuleCollider;
        public bool isDead;
        public bool isSinking;

        AudioSource Dying;

        // Use this for initialization
        void Start()
        {
            var audio = this.GetComponents<AudioSource>();
            Dying = audio[1];
        }

        void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            currentHealth = startingHealth;

           
        }

        // Update is called once per frame
        void Update()
        {

            if (isSinking)
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
                
            }

            
        }
        public void SuperDie()
        {
            StartSinking();
        }
        public void TakeDamage(int amount)
        {
            if (isDead)
            {
                Dying.Play();
                return;
            }
               

            currentHealth -= amount;
            Debug.Log("Took Damage");
            if (currentHealth <= 0)
            {
                Death();
            }
        }

        void Death()
        {
            isDead = true;
           GetComponent<ItemDrop>().DropItem();

            capsuleCollider.enabled = false;
            GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>().Score += scoreValue;
            anim.SetTrigger("Death");

        }

        public void StartSinking()
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            anim.enabled = false;
            isSinking = true;
            
            

            Destroy(gameObject, 1f);
            GameObject.Find("EnemyManager").GetComponent<enemyCount>().DeleteEnemy();
        }
    }
}