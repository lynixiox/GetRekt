using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MPA
{

    public class PlayerHealth : MonoBehaviour
    {
        public float health = 100;
        public float healthMax = 100;
        public Image HurtDecal;
        public GameObject Healllllth;
        public GameObject[] deathMenu;
        public float HurtTime;
        private float cooldown;
        public bool SUICIDE = false;
        public void TakeDamage(int amount)
        {

            HurtDecal.gameObject.SetActive(true);
            Color c = HurtDecal.color;

            c.a = 0.7f - ((health / healthMax)*0.7f);
            HurtDecal.color = c;
            cooldown = HurtTime;
            health -= amount;
            GameObject.FindGameObjectWithTag("Health").GetComponent<HealthManager>().HealthLeft -= amount;
            
            if (health <= 0)
            {
                Die();
                Cursor.visible = true; 
              //  GameObject.FindGameObjectWithTag("Dead").active = true;
            }
                
        }
        public void Die()
        {
            GetComponent<TP_Controller>().dead = true;
            GetComponent<TP_AnimatorController>().Die();
            foreach (GameObject u in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                u.GetComponent<EnemyHealth>().TakeDamage(10000);
            }
            foreach (GameObject u in deathMenu)
                u.SetActive(true);
            Healllllth.SetActive(false);
            
        }
        public void AddHealth(int amount)
        {
            Color c = HurtDecal.color;

            c.a = 0.7f - ((health / healthMax) * 0.7f);
            HurtDecal.color = c;
            health += amount;
            if (health > healthMax)
                health = healthMax;
        }
        public void Update()
        {
            if(health > 0)
            {
               // GameObject.FindGameObjectWithTag("Dead").active = false;
            }
            GameObject.Find("Healthhhhh").GetComponent<HealthManager>().HealthLeft = health;
            if (SUICIDE)
                TakeDamage(1000);
            else if (transform.position.y < -25)
                SUICIDE = true;
            if(HurtDecal.gameObject.activeSelf)
            {

                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                    HurtDecal.enabled = false;
          }
        }
    }
}