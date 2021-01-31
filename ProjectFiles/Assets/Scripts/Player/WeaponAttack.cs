using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MPA
{
    public class WeaponAttack : MonoBehaviour
    {
        public int Damage = 100;
        public bool instaKill = false;
        private float instaCooldown = 0;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (instaCooldown > 0)
                instaCooldown -= Time.deltaTime;
            else
                if (instaKill)
                instaKill = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                if (instaKill)
                    other.GetComponent<EnemyHealth>().TakeDamage(10000);
                else
                    other.GetComponent<EnemyHealth>().TakeDamage(Damage);
                Debug.Log("SHit" + gameObject.name);

            }
        }
        public void SetInstaKill(int time)
        {
            instaKill = true;
            instaCooldown = time;
        }
    }
}
