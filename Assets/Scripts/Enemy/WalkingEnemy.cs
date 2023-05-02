using System;
using Player;
using UnityEngine;

namespace Enemy
{
    
    public sealed class WalkingEnemy : EnemyBase
    {
   
        public enum WalkDirections { Right, Left, None }
    


        [Header("References")]
        [Tooltip("The ground check component used to test whether this enemy has hit a wall to the left.")]
        public GroundCheck wallTestLeft;
        [Tooltip("The ground check component used to test whether this enemy has hit a wall to the right.")]
        public GroundCheck wallTestRight;
        [Tooltip("Left ground check component used to determine when the enemy has reached an edge on its left.")]
        public GroundCheck leftEdge;
        [Tooltip("Right ground check component used to determine when the enemy has reached an edge on its right.")]
        public GroundCheck rightEdge;

        [Header("Settings")]
        [Tooltip("The direction that this enemy walks in until it hits a wall")]
        public WalkDirections walkDirection = WalkDirections.None;
        [Tooltip("Whether this enemy will turn around if it detects an edge.")]
        public bool willTurnAroundAtEdge;
        private Rigidbody2D _enemyRb;

        private SpriteRenderer _spriteRenderer;

     
        protected override void Setup()
        {
            base.Setup();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

    
        protected override void Update()
        {
            DetermineWalkDirection();
            base.Update();
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
            if (TestWall() || GetIsNearEdge())
            {
                TurnAround();
            }
            if (_spriteRenderer != null)
            {
                _spriteRenderer.flipX = (walkDirection == WalkDirections.Right);
            }
        }

        private void TurnAround()
        {
            if (walkDirection == WalkDirections.Left)
            {
                walkDirection = WalkDirections.Right;
            }
            else if (walkDirection == WalkDirections.Right)
            {
                walkDirection = WalkDirections.Left;
            }
        }

       
        private bool TestWall()
        {
            switch (walkDirection)
            {
                case WalkDirections.Left:
                    if (wallTestLeft != null)
                    {
                        return wallTestLeft.CheckGrounded();
                    }
                    break;
                case WalkDirections.Right:
                    if (wallTestRight != null)
                    {
                        return wallTestRight.CheckGrounded();
                    }
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
            GroundCheck check = walkDirection switch
            {
                WalkDirections.Left => leftEdge,
                WalkDirections.Right => rightEdge,
                _ => null
            };
            if (check != null && !check.CheckGrounded())
            {
                return willTurnAroundAtEdge;
            }
            return false;
        }
    }
}
