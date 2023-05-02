using System.Collections;
using System.Collections.Generic;
using Health_Damage;
using UnityEngine;

namespace Player
{
  
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        #region GameObject references
        [Header("Game Object and Component References")]
        [Tooltip("The Input Manager component used to gather player input.")]
        public InputManager inputManager;
        [Tooltip("The Ground Check component used to check whether this player is grounded currently.")]
        public GroundCheck groundCheck;
        [Tooltip("The sprite renderer that represents the player.")]
        public SpriteRenderer spriteRenderer;
        [Tooltip("The health component attached to the player.")]
        public Health playerHealth;
        private bool _gravityRunning;
        private bool _gravityJumping=true;
    

        private Rigidbody2D _playerRigidbody;
        #endregion

        #region Getters (primarily from other components)
        #region Directional facing

        private enum PlayerDirection
        {
            Right,
            Left
        }

        private PlayerDirection Facing
        {
            get
            {
                switch (HorizontalMovementInput)
                {
                    case > 0:
                        return PlayerDirection.Right;
                    case < 0:
                        return PlayerDirection.Left;
                }

                if (spriteRenderer != null && spriteRenderer.flipX&& _playerRigidbody.gravityScale>0)
                    return PlayerDirection.Left;
                return PlayerDirection.Right;
            }
        }
        #endregion

        private bool Grounded => groundCheck != null && groundCheck.CheckGrounded();

        #region Input from Input Manager
        private float HorizontalMovementInput
        {
            get
            {
                if (inputManager != null)
                    return inputManager.horizontalMovement;
                return 0;
            }
        }
        private bool JumpInput => inputManager != null && inputManager.jumpStarted;

        #endregion
        #endregion

        #region Movement Variables
        [Header("Movement Settings")]
        [Tooltip("The speed at which to move the player horizontally")]
        public float movementSpeed = 4.0f;

        [Header("Jump Settings")]
        [Tooltip("The force with which the player jumps.")]
        public float jumpPower = 10.0f;
        [Tooltip("The number of jumps that the player is alowed to make.")]
        public int allowedJumps = 1;
        [Tooltip("The duration that the player spends in the \"jump\" state")]
        public float jumpDuration = 0.1f;
        [Tooltip("The effect to spawn when the player jumps")]
        public GameObject jumpEffect;
        [Tooltip("Layers to pass through when moving upwards")]
        public List<string> passThroughLayers = new();

        // The number of times this player has jumped since being grounded
        private int _timesJumped;
        // Whether the player is in the middle of a jump right now
        private bool _jumping;
        #endregion

        #region Player State Variables
    
        public enum PlayerState
        {
            Idle,
            Walk,
            Jump,
            Fall,
            Dead
        }

        public PlayerState state = PlayerState.Idle;
        #endregion
        #endregion

        #region Functions
        #region GameObject Functions
      
        private void Start()
        {
            SetupRigidbody();
            SetUpInputManager();
        }

  
        private void LateUpdate()
        {
            ProcessInput();
            HandleSpriteDirection();
            DetermineState();
        
            
        
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
            Vector2 movementForce = Vector2.zero;
            if (Mathf.Abs(HorizontalMovementInput) > 0 && state != PlayerState.Dead)
            {
                movementForce = Vector3.right * (movementSpeed * HorizontalMovementInput);
            }
            MovePlayer(movementForce);
        }

       
        private void MovePlayer(Vector2 movementForce)
        {
            if (Grounded && !_jumping)
            {
                float horizontalVelocity = movementForce.x;
                float verticalVelocity = 0;
                _playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            }
            else
            {
                float horizontalVelocity = movementForce.x;
                float verticalVelocity = _playerRigidbody.velocity.y;
                _playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            }
            if (_playerRigidbody.velocity.y > 0)
            {
                foreach (string layerName in passThroughLayers)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName), true);
                } 
            }
            else
            {
                foreach (string layerName in passThroughLayers)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName), false);
                }
            }
        }
        public void GravitatePlayer()
        {

            if (!_gravityRunning)

            {
                StartCoroutine(GravitationOn());
            }
        }
        public IEnumerator GravitationOn()
        {   
            _gravityRunning = true;
        

            _playerRigidbody.gravityScale *= -1;
       

            transform.Rotate(new Vector3(0, 0, 180));
            if (_playerRigidbody.gravityScale < 0)
            {
                _gravityJumping = false;
            }
            yield return new WaitForSeconds(1);
            _gravityJumping=true;

            yield return new WaitForSeconds(2);

            _gravityRunning = false;

        }

   
        private void HandleJumpInput()
        {
            if (JumpInput&&_gravityJumping)
            {
                StartCoroutine(nameof(Jump), 1.0f);
            }
        }

       
        private IEnumerator Jump(float powerMultiplier = 1.0f)
        {
            if (_timesJumped >= allowedJumps || state == PlayerState.Dead) yield break;
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
            StartCoroutine(nameof(Jump), inputManager.jumpHeld ? 1.5f : 1.0f);
        }

      
        private void HandleSpriteDirection()
        {
            if (spriteRenderer != null && _playerRigidbody.gravityScale < 0)
            {
                spriteRenderer.flipX = Facing != PlayerDirection.Left;
            }

            if (spriteRenderer == null || !(_playerRigidbody.gravityScale > 0)) return;
            spriteRenderer.flipX = Facing != PlayerDirection.Right;
        }

   
        #endregion

        #region State Functions

      
        private void SetState(PlayerState newState)
        {
            state = newState;
        }

   
        private void DetermineState()
        {
            if (playerHealth.currentHealth <= 0)
            {
                SetState(PlayerState.Dead);
            }
            else if (Grounded)
            {
                SetState(_playerRigidbody.velocity.magnitude > 0 ? PlayerState.Walk : PlayerState.Idle);
                if (!_jumping)
                {
                    _timesJumped = 0;
                }
            }
            else
            {
                SetState(_jumping ? PlayerState.Jump : PlayerState.Fall);
            }
        }
        #endregion

     
        private void SetupRigidbody()
        {
            if (_playerRigidbody == null)
            {
                _playerRigidbody = GetComponent<Rigidbody2D>();
            }
        }

       
        private void SetUpInputManager()
        {
            inputManager = InputManager.Instance;
            if (inputManager == null)
            {
                Debug.LogError("There is no InputManager set up in the scene for the PlayerController to read from");
            }
        }

        #endregion
    }
}
