using Rimaethon.Scripts.Player;
using UnityEngine;

namespace Rimaethon.Player.Player_States
{
    public class PlayerDamagedState : IPlayerState
    {
        private readonly Animator _animator;
        private readonly PlayerController _playerController;
        private readonly PlayerStateManager _playerStateManager;
        private readonly Rigidbody2D _rb;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Transform _transform;
        private Vector2 _bumpForce;
        private int _oppositeX;
        private int _oppositeY;
        private float _oppositeZAngle;
        private Vector3 _previousRotation;

        public PlayerDamagedState(PlayerController playerController, Transform transform,
            PlayerStateManager playerStateManager, Rigidbody2D rb, Animator animator)
        {
            _transform = transform;
            _playerStateManager = playerStateManager;
            _playerController = playerController;
            _rb = rb;
            _animator = animator;
        }


        public void EnterState()
        {
            _animator.SetBool(AnimationParameters.IsDamaged, true);
            CalculateOppositeDirections();
            BendPlayerOnZAxis();
            DetermineBumpForce();
            _rb.velocity = Vector2.zero;
            BumpPlayer();
        }

        public void UpdateState()
        {
            CheckSwitchState();
        }

        public void ExitState()
        {
            _rb.velocity = Vector2.zero;
            _transform.rotation = Quaternion.Euler(_previousRotation);
            _playerController.isPlayerDamaged = false;
            _animator.SetBool(AnimationParameters.IsDamaged, false);
        }


        public void CheckSwitchState()
        {
            if (_playerController.isPlayerDamaged) return;

            if (_playerController.IsPlayerFalling()) _playerStateManager.ChangeState(_playerStateManager.AirborneState);
            if (_playerController.IsPlayerGrounded())
                _playerStateManager.ChangeState(_playerController.IsPlayerRunning()
                    ? _playerStateManager.RunningState
                    : _playerStateManager.GroundedState);
        }

        private void BumpPlayer()
        {
            _rb.AddForce(_bumpForce, ForceMode2D.Impulse);
        }

        private void BendPlayerOnZAxis()
        {
            _previousRotation = _transform.rotation.eulerAngles;
            _transform.Rotate(0, 0, _oppositeZAngle);
        }


        private void DetermineBumpForce()
        {
            float bumpForceX = 15;
            float bumpForceY = 30;
            bumpForceX *= _oppositeX;
            bumpForceY *= _oppositeY;
            _bumpForce = new Vector2(bumpForceX, bumpForceY);
        }


        private void CalculateOppositeDirections()
        {
            _oppositeX = 0;
            _oppositeY = 0;
            _oppositeZAngle = _transform.rotation.eulerAngles.z;

            if (_playerStateManager.DamageContactNormal == 0) return;

            _oppositeX = _playerStateManager.DamageContactNormal > 0 ? 1 : -1;
            _oppositeY = _playerController.isPlayerGravitated ? -1 : 1;
            // oppositeZAngle is for rotating player upward-back relative to damage area.
            _oppositeZAngle = _oppositeX == _oppositeY ? -13 : 13;
        }
    }
}