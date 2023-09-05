using System;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rimaethon.Scripts.Player
{
    public class InputManager : StaticInstance<InputManager>,PlayerInputs.IPlayerActions
    {
        private PlayerInputs _playerInputs;
        public Vector2 MovementVector => m_MovementVector;
        private Vector2 m_MovementVector;
        public bool jumpStarted;
        public bool jumpHeld;
        public float pauseButton;
        private PlayerController _playerscript;
        
    

        protected override void Awake()
        {
            base.Awake();
            _playerInputs = new PlayerInputs();
            _playerInputs.Player.Movement.performed += OnMovement;
            _playerInputs.Player.Movement.canceled += OnMovement;
            _playerInputs.Player.Jump.performed += OnJump;
            _playerInputs.Player.Jump.canceled += OnJump;
            
           
        }

        private void Start()
        {
            _playerscript = gameObject.GetComponent<PlayerController>();
        }


        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();

            _playerInputs.Player.Movement.performed -= OnMovement;
            _playerInputs.Player.Movement.canceled -= OnMovement;
            _playerInputs.Player.Jump.performed -= OnJump;
            _playerInputs.Player.Jump.canceled -= OnJump;

        }

   

        public void OnMovement(InputAction.CallbackContext callbackContext)
        {
            m_MovementVector = callbackContext.ReadValue<Vector2>();

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            jumpStarted = context.performed;
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnGravity(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }



     
    }
}