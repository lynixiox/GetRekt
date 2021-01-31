using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
namespace MPA
{
    public class TP_Motor : MonoBehaviour
    {
        public static TP_Motor Instance;
        public ParticleSystem PS;
        public ParticleSystem.EmissionModule PSEmission;
        public ParticleSystem.MainModule PSMain;
        public BoxCollider BX;
        public float ForwardSpeed = 10f;
        public float BackwardSpeed = 2f;
        public float StrafingSpeed = 5f;
        public float gravity = 21f;
        public float jumpSpeed = 6f;
        public float terminalVelocity = 20f;
        public Vector3 MoveVector { get; set; }
        public float VerticalVelocity { get; set; }
        public float slideThreshold = 0.6f;
        public float slideSpeed = 10f;
        public float maxControllableSlideMagnitude = 0.4f;
        public float dashSpeedMultiple = 2f;
        public float dashCooldown = 2f;
        public float dashLength = 0.8f;
        public bool dashing = false;
        public TP_Animator.Direction dashDirection;
        public bool attacking = false;
        public float attackLength = 0.2f;

        private float attackLength_ = 0f;
        private float dashLength_;
        private float dashCooldown_;
        private Vector3 slideDirection;
        public Camera cameraMain;
        private CharacterController TPC;
        private TP_Animator TPA;

        AudioSource Axing;

        private void Update()
        {
            dashCooldown_ -= Time.deltaTime;
            attackLength_ -= Time.deltaTime;
            if (attackLength_ <= 0)
            {
                attacking = false;
                BX.enabled = false;
                PSMain.startLifetime = 0.5f;
                PSEmission.rateOverTime = 70;
            }
        }
        void Awake()
        {
            Instance = this;
            if (cameraMain == null)
                cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
            TPC = GetComponent<CharacterController>();
            TPA = GetComponent<TP_Animator>();
            PSEmission = PS.emission;
            PSMain = PS.main;
        }
        void Start()
        {
            var audioS = this.GetComponents<AudioSource>();
            Axing = audioS[1];
        }

        public void UpdateMotor()
        {
            if (!dashing)
                SnapAlignCharacterWithCamera();
            ProcessMotion();
        }

        void ProcessMotion()
        {
            ApplyDash();

            //Transform MoveVector to World Space
            MoveVector = transform.TransformDirection(MoveVector);

            //Normalize MoveVector if Magnitude > 1
            if (!dashing)
                if (MoveVector.magnitude > 1)
                    MoveVector = Vector3.Normalize(MoveVector);
            //ApplySlide iF applicable
            ApplySlide();
            //Multiply MoveVector by MoveSpeed
            if (!dashing)
                MoveVector *= MoveSpeed();


            //Reappply vertical Velocity 
            MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);
            ApplyGravity();

            //Move the Character in World Space
            //CharacterController TPC = GetComponent<TP_Controller>().GetComponent<CharacterController>();
            TPC.Move(MoveVector * Time.deltaTime);


        }

        void SnapAlignCharacterWithCamera()
        {
            if (MoveVector.x != 0 || MoveVector.z != 0)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                     cameraMain.transform.eulerAngles.y,
                                     transform.eulerAngles.z);
            }
        }

        void ApplyGravity()
        {
            if (MoveVector.y > -terminalVelocity)
                MoveVector = new Vector3(MoveVector.x, MoveVector.y - gravity * Time.deltaTime, MoveVector.z);
            if (TPC.isGrounded && MoveVector.y < -1)
                MoveVector = new Vector3(MoveVector.x, -1, MoveVector.z);
        }
        public void Attack()
        {
            PSMain.startLifetime = 0.6f;
            PSEmission.rateOverTime = 2000;
            BX.enabled = true;
            if (!attacking)
            {
                Axing.Stop();
                attacking = true;
                attackLength_ = attackLength * 3.8f;
            }
            else
            {
                Axing.Play();
                attacking = true;
                if (attackLength_ < attackLength)
                    attackLength_ = attackLength;
            }
        }
        void ApplyDash()
        {
            if (dashing)
            {
                dashLength_ -= Time.deltaTime;
                if (dashLength_ <= 0)
                {
                    dashing = false;
                }
                if (dashDirection == TP_Animator.Direction.Forward || dashDirection == TP_Animator.Direction.Stationary)
                    MoveVector = new Vector3(MoveVector.x, MoveVector.y, ForwardSpeed * dashSpeedMultiple);
                else if (dashDirection == TP_Animator.Direction.Backward)
                    MoveVector = new Vector3(MoveVector.x, MoveVector.y, -BackwardSpeed * dashSpeedMultiple * 1.2f);
                else if (dashDirection == TP_Animator.Direction.Left)
                    MoveVector = new Vector3(-StrafingSpeed * dashSpeedMultiple * 1.2f, MoveVector.y, MoveVector.z);
                else if (dashDirection == TP_Animator.Direction.Right)
                    MoveVector = new Vector3(StrafingSpeed * dashSpeedMultiple * 1.2f, MoveVector.y, MoveVector.z);
                else if (dashDirection == TP_Animator.Direction.LeftBackward)
                    MoveVector = new Vector3(-StrafingSpeed * (dashSpeedMultiple * 0.7f), MoveVector.y, -StrafingSpeed * (dashSpeedMultiple * 0.7f));
                else if (dashDirection == TP_Animator.Direction.LeftForward)
                    MoveVector = new Vector3(-StrafingSpeed * (dashSpeedMultiple * 0.7f), MoveVector.y, StrafingSpeed * (dashSpeedMultiple * 0.7f));
                else if (dashDirection == TP_Animator.Direction.RightBackward)
                    MoveVector = new Vector3(StrafingSpeed * (dashSpeedMultiple * 0.7f), MoveVector.y, -StrafingSpeed * (dashSpeedMultiple * 0.7f));
                else if (dashDirection == TP_Animator.Direction.RightForward)
                    MoveVector = new Vector3(StrafingSpeed * (dashSpeedMultiple * 0.7f), MoveVector.y, StrafingSpeed * (dashSpeedMultiple * 0.7f));

            }
        }

        public void Jump()
        {
            if (TPC.isGrounded)
                VerticalVelocity = jumpSpeed;
        }
        public void Dash()
        {
            if (TPC.isGrounded)
            {
                if (dashCooldown_ <= 0)
                {
                    if (!dashing)
                    {
                        dashing = true;
                        dashCooldown_ = dashCooldown;
                        dashLength_ = dashLength;
                        dashDirection = TPA.moveDirection;

                    }
                }
            }
        }

        public void ApplySlide()
        {
            if (!TPC.isGrounded)
                return;

            slideDirection = Vector3.zero;
            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hitInfo))
            {
                if (hitInfo.normal.y < slideThreshold)
                {
                    slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
                }

                if (slideDirection.magnitude < maxControllableSlideMagnitude)
                {
                    MoveVector += slideDirection;
                }
                else
                    MoveVector = slideDirection;
            }
        }

        float MoveSpeed()
        {
            var moveSpeed = 0f;

            /* switch (TP_Animator.Instance.moveDirection)
             {
                 case TP_Animator.Direction.Stationary:
                     moveSpeed = 0f;
                     break;
                 case TP_Animator.Direction.Forward:
                     moveSpeed = ForwardSpeed;
                     break;
                 case TP_Animator.Direction.Backward:
                     moveSpeed = BackwardSpeed;
                     break;
                 case TP_Animator.Direction.Left:
                     moveSpeed = StrafingSpeed;
                     break;
                 case TP_Animator.Direction.Right:
                     moveSpeed = StrafingSpeed;
                     break;
                 case TP_Animator.Direction.LeftForward:
                     moveSpeed = ForwardSpeed;
                     break;
                 case TP_Animator.Direction.LeftBackward:
                     moveSpeed = BackwardSpeed;
                     break;
                 case TP_Animator.Direction.RightForward:
                     moveSpeed = ForwardSpeed;
                     break;
                 case TP_Animator.Direction.RightBackward:
                     moveSpeed = BackwardSpeed;
                     break;
             } */

            switch (TPA.moveDirection)
            {
                case TP_Animator.Direction.Stationary:
                    moveSpeed = 0f;
                    break;
                case TP_Animator.Direction.Forward:
                    moveSpeed = ForwardSpeed;
                    break;
                case TP_Animator.Direction.Backward:
                    moveSpeed = BackwardSpeed;
                    break;
                case TP_Animator.Direction.Left:
                    moveSpeed = StrafingSpeed;
                    break;
                case TP_Animator.Direction.Right:
                    moveSpeed = StrafingSpeed;
                    break;
                case TP_Animator.Direction.LeftForward:
                    moveSpeed = ForwardSpeed * 0.8f;
                    break;
                case TP_Animator.Direction.LeftBackward:
                    moveSpeed = BackwardSpeed * 1.2f;
                    break;
                case TP_Animator.Direction.RightForward:
                    moveSpeed = ForwardSpeed * 0.8f;
                    break;
                case TP_Animator.Direction.RightBackward:
                    moveSpeed = BackwardSpeed * 1.2f;
                    break;
            }

            if (slideDirection.magnitude > 0)
                moveSpeed = slideSpeed;
            return moveSpeed;
        }
    }
}
