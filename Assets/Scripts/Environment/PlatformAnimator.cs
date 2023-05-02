using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(WaypointMover))]
    [RequireComponent(typeof(Animator))]
    public class PlatformAnimator : MonoBehaviour
    {
        [SerializeField] private WaypointMover mover;
        private Animator _animator;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");


        private void Awake()
        {
            mover = GetComponent<WaypointMover>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (mover != null && _animator != null)
            {
                _animator.SetBool(IsMoving, !mover.stopped);
            }
        }
    }
}
