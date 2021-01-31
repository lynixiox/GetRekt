using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MPA
{
    public class TP_Animator : MonoBehaviour {

        private TP_Motor TPMotor;
        public enum Direction
        {
            Stationary, Forward, Backward, Left, Right, LeftForward, RightForward, LeftBackward, RightBackward
        }

        public Direction moveDirection { get; set; }

        public static TP_Animator Instance;

        private void Awake()
        {
            Instance = this;
            TPMotor = GetComponent<TP_Motor>();
        }
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void DetermineMoveDirection()
        {
            var forward = false;
            var backward = false;
            var left = false;
            var right = false;

       /*     if (TP_Motor.Instance.MoveVector.z > 0)
                forward = true;
            else if (TP_Motor.Instance.MoveVector.z < 0)
                backward = true;

            if (TP_Motor.Instance.MoveVector.x > 0)
                right = true;
            else if (TP_Motor.Instance.MoveVector.x < 0)
                left = true;
                */
            if (TPMotor.MoveVector.z > 0)
                forward = true;
            else if (TPMotor.MoveVector.z < 0)
                backward = true;

            if (TPMotor.MoveVector.x > 0)
                right = true;
            else if (TPMotor.MoveVector.x < 0)
                left = true;

            if (forward)
            {
                if (left)
                    moveDirection = Direction.LeftForward;
                else if (right)
                    moveDirection = Direction.RightForward;
                else
                    moveDirection = Direction.Forward;
            }
            else if(backward)
            {
                if (left)
                    moveDirection = Direction.LeftBackward;
                else if (right)
                    moveDirection = Direction.RightBackward;
                else
                    moveDirection = Direction.Backward;
            }
            else if(left)           
                moveDirection = Direction.Left;
            
            else if(right)           
                moveDirection = Direction.Right;
            
            else           
                moveDirection = Direction.Stationary;
            
    }
    }
}