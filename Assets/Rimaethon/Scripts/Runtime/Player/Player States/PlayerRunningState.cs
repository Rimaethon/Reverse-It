using Rimaethon.Player;
using UnityEngine;

namespace Rimaethon.Scripts.Player.PlayerState
{
    public class PlayerRunningState : IPlayerState
    {
        private readonly Animator _animator;
        private readonly PlayerController _playerController;
        private readonly PlayerStateManager _playerStateManager;

        public PlayerRunningState(PlayerController playerController, Animator animator,
            PlayerStateManager playerStateManager)
        {
            _playerController = playerController;
            _animator = animator;
            _playerStateManager = playerStateManager;
        }

        public void EnterState()
        {
            _animator.SetBool(AnimationParameters.IsRunning, true);
        }

        public void UpdateState()
        {
            CheckSwitchState();
        }

        public void ExitState()
        {
            _animator.SetBool(AnimationParameters.IsRunning, false);
        }

        public void CheckSwitchState()
        {
            if (_playerController.isPlayerJumping) _playerStateManager.ChangeState(_playerStateManager.JumpingState);
            if (_playerController.IsPlayerFalling()) _playerStateManager.ChangeState(_playerStateManager.AirborneState);
            if (!_playerController.IsPlayerRunning())
                _playerStateManager.ChangeState(_playerStateManager.GroundedState);
        }
    }
}