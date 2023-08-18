using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Environment;
using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    [RequireComponent(typeof(WaypointMover))]
    public sealed class FlyingEnemy : EnemyBase
    {
        private WaypointMover m_WaypointMover;

        private SpriteRenderer _spriteRenderer;
        private bool m_IsSpriteRendererNotNull;
        private bool m_IsmWaypointMoverNotNull;
        private bool m_WaypointMoverNotNull;


        private void Start()
        {
            m_WaypointMoverNotNull = m_WaypointMover != null;
            m_IsmWaypointMoverNotNull = m_WaypointMover != null;
            m_IsSpriteRendererNotNull = _spriteRenderer != null;
        }

        protected override void Update()
        {
            CheckFlipSprite();
            SetStateInformation();
        }


        protected override void Setup()
        {
            base.Setup();
            m_WaypointMover = GetComponent<WaypointMover>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void CheckFlipSprite()
        {
            if (m_IsmWaypointMoverNotNull && m_IsSpriteRendererNotNull)
                _spriteRenderer.flipX = Vector3.Dot(m_WaypointMover.travelDirection, Vector3.right) < 0;
        }


        private void SetStateInformation()
        {
            if (m_WaypointMoverNotNull)
                enemyStates = m_WaypointMover.stopped ? EnemyStates.Idle : EnemyStates.Walking;
            else
                enemyStates = EnemyStates.Idle;
        }
    }
}