using Rimaethon.Scripts.Environment;
using UnityEngine;

namespace Rimaethon.Runtime.Environment
{
    [RequireComponent(typeof(WaypointMover))]
    [RequireComponent(typeof(Animator))]
    public class PlatformAnimator : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private Animator _animator;
        private WaypointMover _mover;

        private void Awake()
        {
            _mover = GetComponent<WaypointMover>();
            _animator = GetComponent<Animator>();
        }


        private void FixedUpdate()
        {
            _animator.SetBool(IsMoving, !_mover.stopped);
        }
    }
}