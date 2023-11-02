using Rimaethon.Player;
using Rimaethon.Scripts.Player;
using UnityEngine;

namespace Rimaethon.Runtime.Player
{
    public class PlayerAirborneState : IPlayerState
    {
        private const float MaxFallingSpeed = 14f;
        private readonly Animator _animator;

        private readonly PlayerController _playerController;
        private readonly PlayerStateManager _playerStateManager;
        private readonly Rigidbody2D _rb;

        public PlayerAirborneState(PlayerController playerController, Animator animator,
            PlayerStateManager playerStateManager, Rigidbody2D rb)
        {
            _playerController = playerController;
            _animator = animator;
            _playerStateManager = playerStateManager;
            _rb = rb;
        }

        public void EnterState()
        {
            _animator.SetBool(AnimationParameters.IsFalling, true);
        }

        public void UpdateState()
        {
            CheckSwitchState();
            ClampPlayerFallingVelocity();
        }

        public void ExitState()
        {
            _animator.SetBool(AnimationParameters.IsFalling, false);
        }

        public void CheckSwitchState()
        {
            if (_playerController.IsPlayerGrounded())
                _playerStateManager.ChangeState(_playerStateManager.GroundedState);
        }

        private void ClampPlayerFallingVelocity()
        {
            var clampedYVelocity = Mathf.Clamp(_rb.velocity.y, -MaxFallingSpeed, MaxFallingSpeed);
            _rb.velocity = new Vector2(_rb.velocity.x, clampedYVelocity);
        }
    }
}