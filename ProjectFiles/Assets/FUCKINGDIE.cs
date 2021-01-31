using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPA
{
    public class FUCKINGDIE : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
        public void JustDieBunny()
        {
            GetComponentInParent<EnemyHealth>().SuperDie();
        }
    }
}