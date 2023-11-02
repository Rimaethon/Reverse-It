using Rimaethon.Player;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Player;
using UnityEngine;

namespace Rimaethon.Runtime.Player
{
    public class PlayerGravityChangeState : IPlayerState
    {
        private readonly PlayerController _playerController;
        private readonly PlayerStateManager _playerStateManager;
        private readonly Rigidbody2D _rb;

        public PlayerGravityChangeState(PlayerController playerController,
            Rigidbody2D rb,
            PlayerStateManager playerStateManager)
        {
            _playerController = playerController;
            _rb = rb;
            _playerStateManager = playerStateManager;
        }


        public void EnterState()
        {
            ReversePlayerRotationOnZ();
            ReversePlayerGravity();
            _playerController.isPlayerGravitated = !_playerController.isPlayerGravitated;
            EventManager.Instance.Broadcast<bool>(GameEvents.OnCameraOffset,_playerController.isPlayerGravitated);
            _playerController.FlipPlayerSpriteOnX();
            _playerStateManager.ChangeState(_playerStateManager.AirborneState);
        }

        public void UpdateState()
        {
        }

        public void ExitState()
        {
        }

        public void CheckSwitchState()
        {
        }


        private void ReversePlayerGravity()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.gravityScale = -_rb.gravityScale;
        }

        private void ReversePlayerRotationOnZ()
        {
            _playerController.transform.Rotate(0, 0, 180);
        }
    }
}