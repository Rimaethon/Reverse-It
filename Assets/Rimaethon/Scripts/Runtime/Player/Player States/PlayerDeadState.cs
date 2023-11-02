using Rimaethon.Player;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Player;
using UnityEngine;

namespace Rimaethon.Runtime.Player
{
    public class PlayerDeadState : IPlayerState
    {
        private readonly Animator _animator;
        private readonly PlayerController _playerController;
        private readonly PlayerStateManager _playerStateManager;
        private readonly Rigidbody2D _rb;
        private readonly SpriteRenderer _spriteRenderer;
        private float _lastGravityScale;

        public PlayerDeadState(PlayerController playerController, Animator animator,
            PlayerStateManager playerStateManager, Rigidbody2D rb)
        {
            _playerController = playerController;
            _animator = animator;
            _playerStateManager = playerStateManager;
            _rb = rb;
        }

        public void EnterState()
        {
            _animator.SetBool(AnimationParameters.IsDead, true);
            if (_playerController.isPlayerMelted)
            {
                _lastGravityScale = _rb.gravityScale;
                _rb.gravityScale = 0;
            }

            AudioManager.Instance.PlaySFX(SFXClips.PlayerDeath);
            Physics2D.IgnoreLayerCollision(6, 8, true);
            _playerController.walkSpeed = 0;
        }

        public void UpdateState()
        {
            CheckSwitchState();
        }

        public void ExitState()
        {
            if (_playerController.isPlayerMelted)
            {
                _rb.gravityScale = _lastGravityScale;
                _playerController.isPlayerMelted = false;
            }

            _animator.SetBool(AnimationParameters.IsDead, false);
            Physics2D.IgnoreLayerCollision(6, 8, false);
            _playerController.walkSpeed = 5;
        }

        public void CheckSwitchState()
        {
            if (_playerController.isPlayerDead) return;
            EventManager.Instance.Broadcast(GameEvents.OnPlayerRespawn);
        }
    }
}