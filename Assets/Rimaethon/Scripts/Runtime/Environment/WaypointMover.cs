using System.Collections.Generic;
using UnityEngine;

namespace Rimaethon.Scripts.Environment
{
    public class WaypointMover : MonoBehaviour
    {
        [SerializeField] private List<Transform> waypoints;

        [Tooltip("In seconds")] [SerializeField]
        private float movingTime = 1f;

        [SerializeField] private float waitingTime = 3f;
        [HideInInspector] public bool stopped;
        private float _clock;
        private Vector3 _currentPosition;
        private int _currentWaypointIndex;

        private float _interpolateValue;
        private Vector3 _nextPosition;

        private void Awake()
        {
            transform.position = waypoints[0].position;
            _currentWaypointIndex = 0;
            _currentPosition = transform.position;
            _nextPosition = waypoints[1].position;
        }

        private void FixedUpdate()
        {
            if (stopped)
            {
                if (_clock >= waitingTime)
                {
                    stopped = false;
                    _clock = 0;
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
                    _currentPosition = transform.position;
                    _nextPosition = waypoints[_currentWaypointIndex].position;
                    return;
                }

                _clock += Time.fixedDeltaTime;
                return;
            }

            LerpObject();
        }

        private void LerpObject()
        {
            _interpolateValue += Time.fixedDeltaTime / movingTime;
            var nextLerpPosition = Vector3.Lerp(_currentPosition, _nextPosition, _interpolateValue);
            transform.position = nextLerpPosition;
            if (!(Vector3.Distance(transform.position, _nextPosition) < 0.01f)) return;

            _interpolateValue = 0;
            transform.position = _nextPosition;
            stopped = true;
        }
    }
}