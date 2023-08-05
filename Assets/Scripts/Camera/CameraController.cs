using System;
using Player;
using UnityEngine;

namespace Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraController : MonoBehaviour
    {
        public enum CameraStyles
        {
            Locked,
            Overhead,
            DistanceFollow,
            OffsetFollow,
            BetweenTargetAndMouse
        }

        public Transform target;

      
        [SerializeField] private CameraStyles cameraMovementStyle = CameraStyles.Locked;

        [SerializeField] private float maxDistanceFromTarget = 5.0f;

        [SerializeField] private Vector2 cameraOffset = Vector2.zero;

        [SerializeField] private float cameraZCoordinate = -10.0f;

        [SerializeField] private float mouseTracking = 0.5f;

        [SerializeField] private InputManager inputManager;

        private UnityEngine.Camera _playerCamera;


        private void Start()
        {
            InitialSetup();
        }


        public void Update()
        {
            SetCameraPosition();
        }

        private void InitialSetup()
        {
            _playerCamera = GetComponent<UnityEngine.Camera>();
            SetUpInputManager();
        }


        private void SetUpInputManager()
        {
            inputManager = InputManager.Instance;
            if (inputManager == null)
                Debug.LogError("There is no InputManager set up in the scene for the CamaeraController to read from");
        }


        private void SetCameraPosition()
        {
            if (target != null)
            {
                var targetPosition = GetTargetPosition();
                var mousePosition = GetPlayerMousePosition();
                var desiredCameraPosition = ComputeCameraPosition(targetPosition, mousePosition);

                transform.position = desiredCameraPosition;
            }
        }


        private Vector3 GetTargetPosition()
        {
            if (target != null) return target.position;
            return transform.position;
        }


        private Vector3 GetPlayerMousePosition()
        {
            return _playerCamera.ScreenToWorldPoint(new Vector2(inputManager.horizontalLookAxis,
                inputManager.verticalLookAxis));
        }


        private Vector3 ComputeCameraPosition(Vector3 targetPosition, Vector3 mousePosition)
        {
            var result = Vector3.zero;
            switch (cameraMovementStyle)
            {
                case CameraStyles.Locked:
                    result = transform.position;
                    break;
                case CameraStyles.Overhead:
                    result = targetPosition;
                    break;
                case CameraStyles.DistanceFollow:
                    result = transform.position;
                    if ((targetPosition - result).magnitude > maxDistanceFromTarget)
                        result = targetPosition + (result - targetPosition).normalized * maxDistanceFromTarget;
                    break;
                case CameraStyles.OffsetFollow:
                    result = targetPosition + (Vector3)cameraOffset;
                    break;
                case CameraStyles.BetweenTargetAndMouse:
                    var desiredPosition = Vector3.Lerp(targetPosition, mousePosition, mouseTracking);
                    var difference = desiredPosition - targetPosition;
                    difference = Vector3.ClampMagnitude(difference, maxDistanceFromTarget);
                    result = targetPosition + difference;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            result.z = cameraZCoordinate;
            return result;
        }
    }
}