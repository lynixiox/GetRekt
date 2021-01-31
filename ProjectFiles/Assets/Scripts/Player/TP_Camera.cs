using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MPA
{


    public class TP_Camera : MonoBehaviour
    {

        public static TP_Camera Instance;
        public static Camera mainCamera;
        public Transform targetLookAt;
        public Transform target;
        public Transform player;
        public float Distance = 5f;
        public float DistanceMin = 3f;
        public float DistanceMax = 10f;
        public float DistanceSmooth = 0.05f;
        public float DistanceResumeSmooth = 5f;
        public float X_MouseSensitivity = 5f;
        public float Y_MouseSensitivity = 5f;
        public float MouseWheelSensitivity = 5f;
        public float Y_MinLimit = -40;
        public float Y_MaxLimit = 80;
        public float X_Smooth = 0.05f;
        public float Y_Smooth = 0.1f;

        public float OcclusionDistanceStep = 0.5f;
        public int maxOcclusionChecks = 10;


        private Vector3 desiredPosition = Vector3.zero;
        private Vector3 position = Vector3.zero;
        private float mouseX = 0f;
        private float mouseY = 0f;
        private float velX = 0f;
        private float velY = 0f;
        private float velZ = 0f;

        private float startDistance = 0f;
        private float velDistance = 0f;
        private float desiredDistance = 0f;

        private float distanceSmooth = 0f;
        private float preOccludedDistance = 0f;

        void Awake()
        {
            Cursor.visible = false;
            Instance = this;
            mainCamera = GetComponent<Camera>();
           // mainCamera = GetComponentInChildren<Camera>();
          //  UseExistingOrCreateNewMainCamera();
        }

        void Start()
        {
            Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
            startDistance = Distance;
            Reset();
        }

        // Update is called once per frame
        void Update()
        {
            if(targetLookAt == null)
            {
                UseExistingOrCreateNewMainCamera();
            }
        }

        private void LateUpdate()
        {
            if (target == null)
                return;
            
            HandlePlayerInput();

            var count = 0;
            do
            {
                CalculateDesiredPosition();
                count++;

            } while (CheckIfOccluded(count));

            CheckCameraPoints(target.position, desiredPosition);

            UpdatePosition();
        }

        public void Reset()
        {
            mouseX = 0f;
            mouseY = 10f;
            Distance = startDistance;
            desiredDistance = Distance;
            preOccludedDistance = Distance;

        }

        void HandlePlayerInput()
        {
            var deadZone = 0.01f;
           // if (Input.GetMouseButton(1))
          //  {
                mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
                mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;
        //    }

            mouseY = Helper.ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);

            if (Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
            {

                desiredDistance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity,
                                              DistanceMin, DistanceMax);

                preOccludedDistance = desiredDistance;
                distanceSmooth = DistanceSmooth;
            }
        }

        void CalculateDesiredPosition()
        {
            ResetDesiredDistance();
            Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velDistance, distanceSmooth);

            desiredPosition = CalculatePosition(mouseY, mouseX, Distance);


        }

        Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
        {
            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            return target.position + rotation * direction;
            
        }

        bool CheckIfOccluded(int count)
        {
            var isOccluded = false;

            var nearestDistance = CheckCameraPoints(target.position, desiredPosition);

            if(nearestDistance != -1)
            {
                if (count < maxOcclusionChecks)
                {
                    isOccluded = true;
                    Distance -= OcclusionDistanceStep;

                    if (Distance < 0.25f)
                        Distance = 0.25f;
                }
                else
                    Distance = nearestDistance - mainCamera.nearClipPlane;

                desiredDistance = Distance;
                distanceSmooth = DistanceResumeSmooth;
            }

            return isOccluded;
        }

        float CheckCameraPoints(Vector3 from, Vector3 to)
        {
            var nearestDistance = -1f;

            RaycastHit hitInfo;

            Helper.ClipPlanePoints clipPlanePoints = Helper.ClipPlaneAtNear(to);

            // Draw Lines in the editor to make it easier to visualize

            Debug.DrawLine(from, to + transform.forward * -mainCamera.nearClipPlane, Color.red);
            Debug.DrawLine(from, clipPlanePoints.UpperLeft);
            Debug.DrawLine(from, clipPlanePoints.LowerLeft); 
            Debug.DrawLine(from, clipPlanePoints.UpperRight);
            Debug.DrawLine(from, clipPlanePoints.LowerRight);

            Debug.DrawLine((clipPlanePoints.UpperLeft), clipPlanePoints.UpperRight);
            Debug.DrawLine((clipPlanePoints.UpperRight), clipPlanePoints.LowerRight);
            Debug.DrawLine((clipPlanePoints.LowerRight), clipPlanePoints.LowerLeft);
            Debug.DrawLine((clipPlanePoints.LowerLeft), clipPlanePoints.UpperLeft);

            if (Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "PlayerWeapon" && hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "CameraIgnore")
                nearestDistance = hitInfo.distance;

            if (Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "PlayerWeapon" && hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "CameraIgnore")
            {
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;
            }

            if (Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "PlayerWeapon" && hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "CameraIgnore")
            {
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;
            }

            if (Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "PlayerWeapon" && hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "CameraIgnore")
            {
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;
            }

            if (Physics.Linecast(from, to + transform.forward * -mainCamera.nearClipPlane , out hitInfo) && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "PlayerWeapon" && hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "CameraIgnore")
            {
                if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                    nearestDistance = hitInfo.distance;
            }


            return nearestDistance;
        }

        void ResetDesiredDistance()
        {
            if(desiredDistance < preOccludedDistance)
            {
                var pos = CalculatePosition(mouseY, mouseX, preOccludedDistance);

                var nearestDistance = CheckCameraPoints(target.position, pos);

                if(nearestDistance == -1 || nearestDistance > preOccludedDistance)
                {
                    desiredDistance = preOccludedDistance;
                }
            }
        }

        void UpdatePosition()
        {
            
            var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
            var posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
            var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);

            position = new Vector3(posX, posY, posZ);

            transform.position = position;
            transform.LookAt(target);

        }

        public void UseExistingOrCreateNewMainCamera()
        {

            Camera tempCamera;
            
            TP_Camera myCamera;

            tempCamera = mainCamera;
        //    myCamera = tempCamera.GetComponent<TP_Camera>() as TP_Camera;
            

            if (target == null)
            {
                Debug.LogWarning("Problem, no targetLookAtr attached to the TP_Camera object, this is only a problem if this warning is called more than once");
            }



        }
    }
}
