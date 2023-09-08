using System.Collections;
using System.Collections.Generic;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Health_Damage;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        private InputManager InputManager => InputManager.Instance;
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private SpriteRenderer spriteRenderer;
         [SerializeField] private BaseHealth playerBaseHealth;
        private bool _gravityRunning;
        private bool _gravityJumping = true;
        private Rigidbody2D _playerRigidbody;
        private int _timesJumped;
        private bool _jumping;
        [SerializeField] private float groundCheckInterval = 0.1f;
        private bool _isGroundedCache;
        private Coroutine _groundCheckCoroutine;
        
        [SerializeField] private float movementSpeed = 4.0f;

        [SerializeField] private float jumpPower = 10.0f;
        [SerializeField] private int allowedJumps = 1;
        [SerializeField] private float jumpDuration = 0.1f;

        [SerializeField] private GameObject jumpEffect;
        [SerializeField] private List<string> passThroughLayers = new();

        public PlayerStates state = PlayerStates.Idle;

        #endregion
        
        #region Properties
        
        private PlayerDirection Facing
        {
            get
            {
                switch (MovementVector.x)
                {
                    case > 0:
                        return PlayerDirection.Right;
                    case < 0:
                        return PlayerDirection.Left;
                }

                if (spriteRenderer != null && spriteRenderer.flipX && _playerRigidbody.gravityScale > 0)
                    return PlayerDirection.Left;
                return PlayerDirection.Right;
            }
        }


        private IEnumerator GroundCheckLoop()
        {
            while (true)
            {
                _isGroundedCache = groundCheck != null && groundCheck.CheckGrounded();
                yield return new WaitForSeconds(groundCheckInterval);
            }
        }

        private Vector2 MovementVector => InputManager != null ? InputManager.MovementVector : Vector2.zero;
           

        private enum PlayerDirection
        {
            Right,
            Left
        }

        
        private bool JumpInput => InputManager != null && InputManager.JumpStarted;


        private bool Grounded => groundCheck != null && groundCheck.CheckGrounded();

        #endregion



       



        #region Functions

        #region GameObject Functions

        private void Start()
        {
            if (InputManager==null)
            {
                Debug.Log("InputManager is null");
            }
            SetupRigidbody();
            _groundCheckCoroutine = StartCoroutine(GroundCheckLoop());
        }


        private void LateUpdate()
        {
            ProcessInput();
            HandleSpriteDirection();
            DetermineState();
            Debug.Log(MovementVector);
            Debug.Log(JumpInput);


        }

        #endregion

        #region Input Handling and Movement Functions

        private void ProcessInput()
        {
            HandleMovementInput();
            HandleJumpInput();
        }


        private void HandleMovementInput()
        {
            var movementForce = Vector2.zero;
            if (Mathf.Abs(MovementVector.x) > 0 )
                movementForce = Vector3.right * (movementSpeed * MovementVector.x);
            MovePlayer(movementForce);
        }


        private void MovePlayer(Vector2 movementForce)
        {
            if (Grounded && !_jumping)
            {
                var horizontalVelocity = movementForce.x;
                float verticalVelocity = 0;
                _playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            }
            else
            {
                var horizontalVelocity = movementForce.x;
                var verticalVelocity = _playerRigidbody.velocity.y;
                _playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            }

            if (_playerRigidbody.velocity.y > 0)
                foreach (var layerName in passThroughLayers)
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName),
                        true);
            else
                foreach (var layerName in passThroughLayers)
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName),
                        false);
        }

        public void GravitatePlayer()
        {
            if (!_gravityRunning) StartCoroutine(GravitationOn());
        }

        public IEnumerator GravitationOn()
        {
            _gravityRunning = true;


            _playerRigidbody.gravityScale *= -1;


            transform.Rotate(new Vector3(0, 0, 180));
            if (_playerRigidbody.gravityScale < 0) _gravityJumping = false;
            yield return new WaitForSeconds(1);
            _gravityJumping = true;

            yield return new WaitForSeconds(2);

            _gravityRunning = false;
        }


        private void HandleJumpInput()
        {
            Debug.Log("JumpInput: " + JumpInput + " _gravityJumping: " + _gravityJumping + " _jumping: " + _jumping + " Grounded: " + Grounded + " _timesJumped: " + _timesJumped + " state: " + state + " Dead: " + PlayerStates.Dead);
            if (JumpInput && _gravityJumping) StartCoroutine(nameof(Jump), 1.0f);
        }


        private IEnumerator Jump(float powerMultiplier = 1.0f)
        {
            if (_timesJumped >= allowedJumps || state == PlayerStates.Dead) yield break;
            _jumping = true;
            float time = 0;
            SpawnJumpEffect();
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0);
            _playerRigidbody.AddForce(transform.up * (jumpPower * powerMultiplier), ForceMode2D.Impulse);
            _timesJumped++;
            while (time < jumpDuration)
            {
                yield return null;
                time += Time.deltaTime;
            }

            _jumping = false;
        }


        private void SpawnJumpEffect()
        {
            if (jumpEffect == null) return;
            var transform1 = transform;
            Instantiate(jumpEffect, transform1.position, transform1.rotation, null);
        }


        public void Bounce()
        {
            _timesJumped = 0;
            //StartCoroutine(nameof(Jump), inputManager.jumpHeld ? 1.5f : 1.0f);
        }


        private void HandleSpriteDirection()
        {
            if (spriteRenderer != null && _playerRigidbody.gravityScale < 0)
                spriteRenderer.flipX = Facing != PlayerDirection.Left;

            if (spriteRenderer == null || !(_playerRigidbody.gravityScale > 0)) return;
            spriteRenderer.flipX = Facing != PlayerDirection.Right;
        }

        #endregion

        #region State Functions

        private void SetState(PlayerStates newState)
        {
            state = newState;
        }


        private void DetermineState()
        {
            if (Grounded)
            {
                SetState(_playerRigidbody.velocity.magnitude > 0 ? PlayerStates.Walk : PlayerStates.Idle);
                if (!_jumping) _timesJumped = 0;
            }
            else
            {
                SetState(_jumping ? PlayerStates.Jump : PlayerStates.Fall);
            }
        }

        #endregion


        private void SetupRigidbody()
        {
            if (_playerRigidbody == null) _playerRigidbody = GetComponent<Rigidbody2D>();
        }




        #endregion

        
    }
}