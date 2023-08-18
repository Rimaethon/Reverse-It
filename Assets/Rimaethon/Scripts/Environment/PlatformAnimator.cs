using UnityEngine;

namespace Rimaethon.Scripts.Environment
{
    [RequireComponent(typeof(WaypointMover))]
    [RequireComponent(typeof(Animator))]
    public class PlatformAnimator : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private WaypointMover m_Mover;
        private Animator _animator;
        private bool m_IsmMoverNotNull;
        private bool m_IsAnimatorNotNull;


        private void Start()
        {
            m_IsAnimatorNotNull = _animator != null;
            m_IsmMoverNotNull = m_Mover != null;
        }

        private void Awake()
        {
            m_Mover = GetComponent<WaypointMover>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (m_IsmMoverNotNull && m_IsAnimatorNotNull) _animator.SetBool(IsMoving, !m_Mover.stopped);
        }
    }
}