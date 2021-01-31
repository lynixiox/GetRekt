using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MPA
{
    public class TP_AnimatorController : MonoBehaviour
    {
        public Animator anim;
        public Animator anim2;
        public Animator anim3;
        private TP_Animator TPA;
        private TP_Motor TPM;
        public GameObject DeathRun;
        public GameObject DeathIdle;
        public GameObject NormalRun;
        
        // Use this for initialization
        void Start()
        {

            TPA = GetComponent<TP_Animator>();
            TPM = GetComponent<TP_Motor>();
        }

        // Update is called once per frame
        void Update()
        {
            if (TPA.moveDirection != TP_Animator.Direction.Stationary)
            {
                if (TPA.moveDirection == TP_Animator.Direction.Backward)
                {
                    anim.SetInteger("Movement", 2);
                 //   anim2.SetInteger("Movement", 2);
               //     anim3.SetInteger("Movement", 2);
                }
                else
                {
                    anim.SetInteger("Movement", 1);
             //       anim2.SetInteger("Movement", 1);
           //         anim3.SetInteger("Movement", 1);

                }
            }
            else
            {
                anim.SetInteger("Movement", 0);
       //         anim2.SetInteger("Movement", 0);
        //        anim3.SetInteger("Movement", 0);

            }
            anim.SetBool("Attacking", TPM.attacking);
          //  anim2.SetBool("Attacking", TPM.attacking);
         //   anim3.SetBool("Attacking", TPM.attacking);


            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                DeathRun.SetActive(true);
                GetComponent<TP_Controller>().enabled = false;
                TPA.enabled = false;
                TPM.enabled = false;
                NormalRun.SetActive(false);
                this.enabled = false;
            }
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Dead2"))
            {
                DeathIdle.SetActive(true);
                GetComponent<TP_Controller>().enabled = false;
                TPA.enabled = false;
                TPM.enabled = false;
                NormalRun.SetActive(false);
                this.enabled = false;
            }
        }
        public void Die()
        {
            anim.SetBool("Dead", true);
            TPA.moveDirection = TP_Animator.Direction.Stationary;

        }
    }
}
