using System;
using System.Collections;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rimaethon.Scripts.Player
{
    public class InputManager : Singleton<InputManager>,DefaultInputActions.IPlayerActions
    {
        private PlayerInputs _playerInputs;
        [SerializeField] private GameObject player;


      
        public Vector2 MovementVector => m_MovementVector;
        private Vector2 m_MovementVector;

        public bool jumpStarted;

        public bool jumpHeld;

        public float pauseButton;

        private PlayerController _playerscript;
        
    

        private void Awake()
        {
            _playerInputs = new PlayerInputs();
           
        }


        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();
            
        }

        public void OnMovement(InputAction.CallbackContext callbackContext)
        {
            m_MovementVector = callbackContext.ReadValue<Vector2>();

        }
        private void Start()
        {
            _playerscript = player.GetComponent<PlayerController>();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
    }
}