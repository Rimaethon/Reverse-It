using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance; 
        [SerializeField] GameObject player;
        private PlayerController _playerscript;

  
        private void Awake()
        {
            ResetValuesToDefault();
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            _playerscript=player.GetComponent<PlayerController>();
        }

        private void ResetValuesToDefault()
        {
            horizontalMovement = default;
            verticalMovement = default;

            horizontalLookAxis = default;
            verticalLookAxis = default;

            jumpStarted = default;
            jumpHeld = default;

            pauseButton = default;
        }

        [Header("Movement Input")]
        [Tooltip("The horizontal movement input of the player.")]
        public float horizontalMovement;
        [Tooltip("The vertical movement input of the player.")]
        public float verticalMovement;

 
        public void GetMovementInput(InputAction.CallbackContext callbackContext)
        {
            Vector2 movementVector = callbackContext.ReadValue<Vector2>();

            horizontalMovement = movementVector.x;
            verticalMovement = movementVector.y;
        }
    
        [Header("Jump Input")]
        [Tooltip("Whether a jump was started this frame.")]
        public bool jumpStarted;
        [Tooltip("Whether the jump button is being held.")]
        public bool jumpHeld;

   
        public void GetJumpInput(InputAction.CallbackContext callbackContext)
        {
            jumpStarted = !callbackContext.canceled;
            jumpHeld = !callbackContext.canceled;
            if (Instance.isActiveAndEnabled)
            {
                StartCoroutine(nameof(ResetJumpStart));
            } 
        }

 
        private IEnumerator ResetJumpStart()
        {
            yield return new WaitForEndOfFrame();
            jumpStarted = false;
        }
    
        [Header("Pause Input")]
        [Tooltip("The state of the pause button")]
        public float pauseButton;

    
        public void GetPauseInput(InputAction.CallbackContext callbackContext)
        {
            pauseButton = callbackContext.ReadValue<float>();
        }
    
        [Header("Mouse Input")]
        [Tooltip("The horizontal mouse input of the player.")]
        public float horizontalLookAxis;
        [Tooltip("The vertical mouse input of the player.")]
        public float verticalLookAxis;

        public InputManager()
        {
            jumpStarted = false;
        }


        public void GetMouseLookInput(InputAction.CallbackContext callbackContext)
        {
            Vector2 mouseLookVector = callbackContext.ReadValue<Vector2>();
            horizontalLookAxis = mouseLookVector.x;
            verticalLookAxis = mouseLookVector.y;
        }
        public void GetGravityInput(InputAction.CallbackContext callbackContext)
        {
           
            _playerscript.GravitatePlayer();
        
        }
    }
}