using System;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputs;

namespace Rimaethon.Runtime.Managers
{
    public class InputManager : PrivatePersistentSingleton<InputManager>, IPlayerActions
    {
        private bool _isPlayerDead;
        private float _movementDirection;
        private PlayerInputs _playerInputs;


        protected override void Awake()
        {
            base.Awake();
            _playerInputs = new PlayerInputs(); 
        }

        private void OnEnable()
        {
            _playerInputs.Enable();
            EnableInputs();


            EventManager.Instance.AddHandler(GameEvents.OnPlayerDead, HandlePlayerDead);
            EventManager.Instance.AddHandler(GameEvents.OnPlayerRespawned, HandlePlayerRevive);

            EventManager.Instance.AddHandler(GameEvents.OnPause, DisableMovement);
            EventManager.Instance.AddHandler(GameEvents.OnResume, EnableInputs);
        }

        private void OnDisable()
        {
            _playerInputs.Disable();

            if (EventManager.Instance == null) return;
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerDead, HandlePlayerDead);
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerRespawned, HandlePlayerRevive);

            EventManager.Instance.RemoveHandler(GameEvents.OnPause, DisableMovement);
            EventManager.Instance.RemoveHandler(GameEvents.OnResume, EnableInputs);
        }


    


        public void OnMovement(InputAction.CallbackContext context)
        {
            var movementVector = context.ReadValue<Vector2>();
            _movementDirection = movementVector.x;
            EventManager.Instance.Broadcast(GameEvents.OnPlayerMovement, _movementDirection);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            EventManager.Instance.Broadcast(GameEvents.OnPlayerJump);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            EventManager.Instance.Broadcast(GameEvents.OnUIBack);
        }

        public void OnGravityChange(InputAction.CallbackContext context)
        {
            EventManager.Instance.Broadcast(GameEvents.OnPlayerGravityChange);
        }

        public void OnScreenshot(InputAction.CallbackContext context)
        {
            EventManager.Instance.Broadcast(GameEvents.OnScreenshot);
        }

        private void HandlePlayerDead()
        {
            _isPlayerDead = true;
            DisableMovement();
        }

        private void HandlePlayerRevive()
        {
            _isPlayerDead = false;
            EnableInputs();
        }

        private void DisableMovement()
        {
            _playerInputs.Player.Movement.performed -= OnMovement;
            _playerInputs.Player.Jump.performed -= OnJump;
            _playerInputs.Player.GravityChange.performed -= OnGravityChange;
        }


        private void EnableInputs()
        {
            if (_isPlayerDead) return;

            _playerInputs.Player.Movement.performed += OnMovement;
            _playerInputs.Player.Movement.canceled += OnMovement;
            _playerInputs.Player.Jump.performed += OnJump;
            _playerInputs.Player.GravityChange.performed += OnGravityChange;
            _playerInputs.Player.Pause.performed += OnPause;
            _playerInputs.Player.Screenshot.performed += OnScreenshot;
        }
    }
}