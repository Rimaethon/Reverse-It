using System;
using System.Threading.Tasks;
using Rimaethon.Runtime.AI.EnemyComponentScripts;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Player;
using Rimaethon.Utility;
using UnityEngine;

namespace Rimaethon.Player
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public bool isPlayerGravitated;
        [HideInInspector] public bool isPlayerJumping;
        [HideInInspector] public bool isPlayerDamaged;
        [HideInInspector] public bool isPlayerDead;
        [HideInInspector] public bool isPlayerMelted;
        public float walkSpeed = 5f;

        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private Logging logging;
        [SerializeField] private LayerMask groundLayer;
        
        private Animator _animator;
        private Vector2 _jumpDirectıon;
        private Vector2 _movement;
        private float _movementDirection;
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private PlayerStateManager _stateManager;
        private Transform _transform;
        private const float JumpForce = 55f;


        private void Awake()
        {
            //logging.sender = this;
            _rb = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _stateManager = new PlayerStateManager(this, _animator, _spriteRenderer, _rb, _transform);
        }


        private void Update()
        {
            _stateManager.UpdateCurrentState();
        }

        private void FixedUpdate()
        {
            MovePlayerHorizontally();
            CheckSpriteDirection();
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnPlayerJump, JumpPlayer);
            EventManager.Instance.AddHandler<float>(GameEvents.OnPlayerMovement, GetPlayerMovementDirection);
            EventManager.Instance.AddHandler(GameEvents.OnPlayerMelted, () => isPlayerMelted = !isPlayerMelted);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;

            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerJump, JumpPlayer);
            EventManager.Instance.RemoveHandler<float>(GameEvents.OnPlayerMovement, GetPlayerMovementDirection);
        }

        private void MovePlayerHorizontally()
        {
            if (isPlayerDamaged) return;

            _movement.x = _movementDirection * walkSpeed;
            _movement.y = _rb.velocity.y;
            _rb.velocity = _movement;
        }

        private void GetPlayerMovementDirection(float direction)
        {
            _movementDirection = direction;
        }


        private void CheckSpriteDirection()
        {
            if (!IsPlayerRunning() || isPlayerDamaged) return;

            FlipPlayerSpriteOnX(_movementDirection > 0 == isPlayerGravitated);
        }

        public void FlipPlayerSpriteOnX(bool? isFacingRight = null)
        {
            if (isFacingRight == null)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
                return;
            }

            _spriteRenderer.flipX = (bool)isFacingRight;
            EventManager.Instance.Broadcast(GameEvents.OnPlayerDirectionChange, _spriteRenderer.flipX);
        }

        private void JumpPlayer()
        {
            if (!IsPlayerGrounded() || isPlayerJumping || isPlayerDamaged) return;
            _jumpDirectıon.y = isPlayerGravitated ? -0.9f : 1;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(JumpForce * _jumpDirectıon, ForceMode2D.Impulse);
            isPlayerJumping = true;
            EventManager.Instance.Broadcast(GameEvents.OnPlayerInSight);
        }


        public bool IsPlayerRunning()
        {
            return _movementDirection != 0;
        }

        public bool IsPlayerFalling()
        {
            return IsPlayerRelativeVelocityDownward() && IsPlayerOnAir();
        }

        public bool IsPlayerGrounded()
        {
            return groundCheck.IsGrounded;
        }

        private bool IsPlayerRelativeVelocityDownward()
        {
            return Math.Abs(_rb.velocity.y) > 1f && Mathf.Sign(_rb.velocity.y) != Mathf.Sign(_rb.gravityScale);
        }

        private bool IsPlayerOnAir()
        {
            return !Physics2D.Raycast(_transform.position, _rb.gravityScale * Vector2.down, 1.5f, groundLayer);
        }

        #region Animation Events

        //Renaming these will break the animation events
        private void OnJumpAnimationComplete()
        {
            isPlayerJumping = false;
        }

        private void OnDamagedAnimationComplete()
        {
            isPlayerDamaged = false;
        }

        private void OnDeadAnimationComplete()
        {
            isPlayerDead = false;
        }

        #endregion
    }
}