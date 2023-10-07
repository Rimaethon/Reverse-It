using Rimaethon.Player;
using UnityEngine;

namespace Rimaethon.Scripts.Player.Player_States
{
    public class PlayerGroundedState : IPlayerState
    {
        private readonly Animator _animator;

        private readonly PlayerController _playerController;
        private readonly PlayerStateManager _playerStateManager;

        public PlayerGroundedState(PlayerController playerController, Animator animator,
            PlayerStateManager playerStateManager)
        {
            _playerController = playerController;
            _animator = animator;
            _playerStateManager = playerStateManager;
        }

        public void EnterState()
        {
            _animator.SetBool(AnimationParameters.IsIdle, true);
        }

        public void UpdateState()
        {
            CheckSwitchState();
        }

        public void ExitState()
        {
            if (_animator==null)
                return;
            _animator.SetBool(AnimationParameters.IsIdle, false);
        }

        public void CheckSwitchState()
        {
            if (_playerController.isPlayerJumping) _playerStateManager.ChangeState(_playerStateManager.JumpingState);
            if (_playerController.IsPlayerRunning()) _playerStateManager.ChangeState(_playerStateManager.RunningState);


            if (_playerController.IsPlayerFalling()) _playerStateManager.ChangeState(_playerStateManager.AirborneState);
        }
    }
}