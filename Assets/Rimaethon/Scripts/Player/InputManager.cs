using System;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rimaethon.Scripts.Player
{
    public class InputManager : StaticInstance<InputManager>,PlayerInputs.IPlayerActions
    {
        private PlayerInputs _playerInputs;
        private Vector2 m_MovementVector;
        private bool _jumpStarted;
        private bool _jumpHeld;
        private bool _isPaused;
        private PlayerController _playerController;

        #region Properties
        public Vector2 MovementVector => m_MovementVector;
        public bool JumpStarted => _jumpStarted;
        public bool JumpHeld => _jumpHeld;
        public bool IsPaused => _isPaused;
        
        #endregion 
    

        protected override void Awake()
        {
            base.Awake();
            _playerInputs = new PlayerInputs();
            _playerInputs.Enable();

            _playerInputs.Player.Movement.performed += OnMovement;
            _playerInputs.Player.Movement.canceled += OnMovement;
            _playerInputs.Player.Pause.performed+= OnPause;
            _playerInputs.Player.Pause.canceled += OnPause;
            _playerInputs.Player.Jump.performed += OnJump;
            _playerInputs.Player.Jump.canceled += OnJump;
            
        }

        private void Start()
        {
            _playerController = gameObject.GetComponent<PlayerController>();
        }


     

        private void OnDisable()
        {
            _playerInputs.Disable();

            _playerInputs.Player.Movement.performed -= OnMovement;
            _playerInputs.Player.Movement.canceled -= OnMovement;
            _playerInputs.Player.Jump.performed -= OnJump;
            _playerInputs.Player.Jump.canceled -= OnJump;
            _playerInputs.Player.Pause.performed-= OnPause;
            _playerInputs.Player.Pause.canceled -= OnPause;
        }

   

        public void OnMovement(InputAction.CallbackContext callbackContext)
        {
            m_MovementVector = callbackContext.ReadValue<Vector2>();

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _jumpStarted = context.performed;
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventManager.Instance.Broadcast(GameEvents.OnTogglePause);
            }
        }

        public void OnGravity(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }



     
    }
}