using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MPA
{
    public class EnemyAttack : MonoBehaviour
    {


        public float CooldownTime;
        private float Cooldown;
        private float cooldown;
        public int damage = 10;



        private void Update()
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
        }
        private void OnTriggerStay(Collider other)
        {


            if (other.gameObject.tag == "Player")
            {
                if (cooldown <= 0)
                {
                    Vector3 direction = (other.transform.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
                    cooldown = 0.12f;
                }
                else
                    cooldown -= Time.deltaTime;

                if (Cooldown <= 0)
                {
                    GetComponentInChildren<Animator>().SetTrigger("Attack");
                    Debug.Log("Damage Given");
                    other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                    Cooldown = CooldownTime;
                }



            }
        }
    }
}