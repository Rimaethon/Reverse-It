using Environment;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(WaypointMover))]
    public sealed class FlyingEnemy : EnemyBase
    {
        [Header("References")]
        [Tooltip("The waypoint mover component which does the work of moving this enemy")]
        public WaypointMover waypointMover;

        // The sprite renderer associated with this enemy
        private SpriteRenderer _spriteRenderer;

  
        protected override void Setup()
        {
            base.Setup();
            waypointMover = GetComponent<WaypointMover>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

    
        protected override void Update()
        {
            CheckFlipSprite();
            SetStateInformation();
        }

   
        private void CheckFlipSprite()
        {
            if (waypointMover != null && _spriteRenderer != null)
            {
                _spriteRenderer.flipX = (Vector3.Dot(waypointMover.travelDirection, Vector3.right) < 0);
            }
        }


        private void SetStateInformation()
        {
            if (waypointMover != null)
            {
                enemyState = waypointMover.stopped ? EnemyState.Idle : EnemyState.Walking;
            }
            else
            {
                enemyState = EnemyState.Idle;
            }
        }
    }
}
