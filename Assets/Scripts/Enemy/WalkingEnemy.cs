using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public sealed class WalkingEnemy : EnemyBase
    {
        public enum WalkDirections
        {
            Right,
            Left,
            None
        }

      
        [SerializeField] private GroundCheck wallTestLeft;

        [SerializeField] private GroundCheck wallTestRight;

        [SerializeField] private GroundCheck leftEdge;

        [SerializeField] private GroundCheck rightEdge;

        [SerializeField] private WalkDirections walkDirection = WalkDirections.None;

        [SerializeField] private bool willTurnAroundAtEdge;

        private Rigidbody2D _enemyRb;

        private SpriteRenderer _spriteRenderer;


        protected override void Update()
        {
            DetermineWalkDirection();
            base.Update();
        }


        protected override void Setup()
        {
            base.Setup();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        protected override Vector3 GetMovement()
        {
            switch (walkDirection)
            {
                case WalkDirections.None:
                    enemyState = EnemyState.Idle;
                    return Vector3.zero;
                case WalkDirections.Left:
                    enemyState = EnemyState.Walking;
                    return Vector3.left * (moveSpeed * Time.deltaTime);
                case WalkDirections.Right:
                    enemyState = EnemyState.Walking;
                    return Vector3.right * (moveSpeed * Time.deltaTime);
                default:
                    return base.GetMovement();
            }
        }


        private void DetermineWalkDirection()
        {
            if (TestWall() || GetIsNearEdge()) TurnAround();
            if (_spriteRenderer != null) _spriteRenderer.flipX = walkDirection == WalkDirections.Right;
        }

        private void TurnAround()
        {
            if (walkDirection == WalkDirections.Left)
                walkDirection = WalkDirections.Right;
            else if (walkDirection == WalkDirections.Right) walkDirection = WalkDirections.Left;
        }


        private bool TestWall()
        {
            switch (walkDirection)
            {
                case WalkDirections.Left:
                    if (wallTestLeft != null) return wallTestLeft.CheckGrounded();
                    break;
                case WalkDirections.Right:
                    if (wallTestRight != null) return wallTestRight.CheckGrounded();
                    break;
                case WalkDirections.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }


        private bool GetIsNearEdge()
        {
            var check = walkDirection switch
            {
                WalkDirections.Left => leftEdge,
                WalkDirections.Right => rightEdge,
                _ => null
            };
            if (check != null && !check.CheckGrounded()) return willTurnAroundAtEdge;
            return false;
        }
    }
}