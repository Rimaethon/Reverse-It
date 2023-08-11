using Environment;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Enemy;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(WaypointMover))]
    public sealed class FlyingEnemy : EnemyBase
    {
        [SerializeField] private WaypointMover waypointMover;

        private SpriteRenderer _spriteRenderer;


        protected override void Update()
        {
            CheckFlipSprite();
            SetStateInformation();
        }


        protected override void Setup()
        {
            base.Setup();
            waypointMover = GetComponent<WaypointMover>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void CheckFlipSprite()
        {
            if (waypointMover != null && _spriteRenderer != null)
                _spriteRenderer.flipX = Vector3.Dot(waypointMover.travelDirection, Vector3.right) < 0;
        }


        private void SetStateInformation()
        {
            if (waypointMover != null)
                enemyStates = waypointMover.stopped ? EnemyStates.Idle : EnemyStates.Walking;
            else
                enemyStates = EnemyStates.Idle;
        }
    }
}