using Rimaethon.Player;
using UnityEngine;

namespace Rimaethon.Scripts.Player.Player_States
{
    public class PlayerJumpingState : IPlayerState
    {
        private readonly Animator _animator;
        private readonly PlayerController _playerController;
        private readonly PlayerStateManager _playerStateManager;
        private readonly Rigidbody2D _rb;

        public PlayerJumpingState(PlayerController playerController, Animator animator,
            PlayerStateManager playerStateManager, Rigidbody2D rb)
        {
            _playerController = playerController;
            _animator = animator;
            _playerStateManager = playerStateManager;
        }

        public void EnterState()
        {
            _animator.SetBool(AnimationParameters.IsJumping, true);
        }

        public void UpdateState()
        {
            CheckSwitchState();
        }

        public void ExitState()
        {
            _animator.SetBool(AnimationParameters.IsJumping, false);
            _playerController.isPlayerJumping = false;
        }

        public void CheckSwitchState()
        {
            if (_playerController.isPlayerJumping && !_playerController.IsPlayerGrounded()) return;

            if (_playerController.IsPlayerFalling()) _playerStateManager.ChangeState(_playerStateManager.AirborneState);

            if (_playerController.IsPlayerGrounded())
                _playerStateManager.ChangeState(_playerController.IsPlayerRunning()
                    ? _playerStateManager.RunningState
                    : _playerStateManager.GroundedState);
        }
    }
}