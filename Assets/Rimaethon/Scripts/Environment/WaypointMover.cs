using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment
{
    public class WaypointMover : MonoBehaviour
    {
        [SerializeField] private List<Transform> waypoints;

        [SerializeField] private float moveSpeed = 1f;

        [SerializeField] private float waitTime = 3f;

        [HideInInspector] public bool stopped;

        [HideInInspector] public Vector3 travelDirection;

        private Vector3 _currentTarget;
        private int _currentTargetIndex;

        private Vector3 _previousTarget;

        private float _timeToStartMovingAgain;


        private void Start()
        {
            InitializeInformation();
        }


        public void Update()
        {
            ProcessMovementState();
        }


        private void ProcessMovementState()
        {
            if (stopped)
                StartCheck();
            else
                Travel();
        }


        private void StartCheck()
        {
            if (!(Time.time >= _timeToStartMovingAgain)) return;
            stopped = false;
            _previousTarget = _currentTarget;
            _currentTargetIndex += 1;
            if (_currentTargetIndex >= waypoints.Count) _currentTargetIndex = 0;
            _currentTarget = waypoints[_currentTargetIndex].position;
            CalculateTravelInformation();
        }


        private void InitializeInformation()
        {
            _previousTarget = transform.position;
            _currentTargetIndex = 0;
            if (waypoints.Count > 0)
            {
                _currentTarget = waypoints[0].position;
            }
            else
            {
                waypoints.Add(transform);
                _currentTarget = _previousTarget;
            }

            CalculateTravelInformation();
        }


        private void CalculateTravelInformation()
        {
            travelDirection = (_currentTarget - _previousTarget).normalized;
        }


        private void Travel()
        {
            Transform transform1;
            (transform1 = transform).Translate(travelDirection * (moveSpeed * Time.deltaTime));

            var directionFromCurrentPositionToTarget = _currentTarget - transform1.position;
            var overAxis = new bool[3];

            for (var i = 0; i < 3; i++)
            {
                if (directionFromCurrentPositionToTarget[i] != 0 &&
                    Math.Abs(Mathf.Sign(directionFromCurrentPositionToTarget[i]) - Mathf.Sign(travelDirection[i])) < Mathf.Epsilon) continue;
                overAxis[i] = true;
                directionFromCurrentPositionToTarget[i] = 0;
            }

            transform.position += directionFromCurrentPositionToTarget;

            // If we are over the x, y, and z of our target we need to stop
            if (overAxis.All(axis => axis)) BeginWait();
        }


        private void BeginWait()
        {
            stopped = true;
            _timeToStartMovingAgain = Time.time + waitTime;
        }
    }
}