using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace MPA
{
    public class TP_Controller : MonoBehaviour
    {
        public bool dead = false;
        public static CharacterController characterController;
        public static TP_Controller Instance;

        private TP_Motor TPMotor;
        private TP_Animator TPAnim;


        void Awake()
        {
            Instance = this;
            characterController = GetComponent<CharacterController>();
            if (TP_Camera.Instance.GetComponentInChildren<Camera>() != null)
            {
                TP_Camera.Instance.player = this.transform;
                TP_Camera.Instance.targetLookAt = this.transform.Find("TargetLookAt");
            }

            TPMotor = GetComponent<TP_Motor>();
            TPAnim = GetComponent<TP_Animator>();

        }

        void Start()
        {

        }

        void Update()
        {
            if (TP_Camera.Instance.GetComponentInChildren<Camera>() == null)
                return;
            if (TP_Camera.Instance.targetLookAt != this.transform.Find("TargetLookAt"))
                TP_Camera.Instance.targetLookAt = this.transform.Find("TargetLookAt");

            GetLocomotionInput();
            HandleActionInput();

            TPMotor.UpdateMotor();
            //TP_Motor.Instance.UpdateMotor();
        }


        void GetLocomotionInput()
        {
            var deadZone = 0.1f;


            //TP_Motor.Instance.VerticalVelocity = TP_Motor.Instance.MoveVector.y;
            TPMotor.VerticalVelocity = TPMotor.MoveVector.y;
            TPMotor.MoveVector = Vector3.zero;
            // TP_Motor.Instance.MoveVector = Vector3.zero;

            if (dead)
            {

            }
            else
            {
                if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone || true)
                {

                    TPMotor.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

                    //TP_Motor.Instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

                }


                  if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
                 {
                 TP_Motor.Instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                    TPMotor.MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);

                }
                TPAnim.DetermineMoveDirection();
                //TP_Animator.Instance.DetermineMoveDirection();
            }
        }

        private void HandleActionInput()
        {
            if (Input.GetButton("Jump"))
                Jump();
            if (Input.GetButton("Dash"))
                Dash();
            if (Input.GetButton("Fire1"))
                Attack();
        }

        void Jump()
        {
            TPMotor.Jump();
            // TP_Motor.Instance.Jump();   
        }
        void Dash()
        {
            TPMotor.Dash();
        }
        void Attack()
        {
            TPMotor.Attack();
        }
    }
}
