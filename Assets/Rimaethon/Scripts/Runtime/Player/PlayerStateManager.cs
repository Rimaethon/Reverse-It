using Rimaethon.Player;
using Rimaethon.Player.Player_States;
using Rimaethon.Runtime.Player;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Player.Player_States;
using Rimaethon.Scripts.Player.PlayerState;
using UnityEngine;

namespace Rimaethon.Scripts.Player
{
    public class PlayerStateManager
    {
        private readonly Animator _animator;

        private readonly float _gravityChangeCooldown = 1f;
        private readonly PlayerController _playerController;
        private readonly Rigidbody2D _rb;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Transform _transform;

        private IPlayerState _currentState;
        private IPlayerState _damagedState;
        private IPlayerState _deadState;
        private float _lastGravityChangeTime;

        public PlayerStateManager(PlayerController playerController,
            Animator animator,
            SpriteRenderer spriteRenderer,
            Rigidbody2D rb,
            Transform transform)
        {
            _playerController = playerController;
            _animator = animator;
            _spriteRenderer = spriteRenderer;
            _rb = rb;
            _transform = transform;

            InitializeStates();
        }

        public float DamageContactNormal { get; private set; }

        public IPlayerState GroundedState { get; private set; }
        public IPlayerState AirborneState { get; private set; }
        public IPlayerState JumpingState { get; private set; }
        public IPlayerState RunningState { get; private set; }
        public IPlayerState GravityChangeState { get; private set; }

        private void InitializeStates()
        {
         

            EventManager.Instance.AddHandler(GameEvents.OnPlayerGravityChange, CanChangeGravity);
            EventManager.Instance.AddHandler<float>(GameEvents.OnPlayerDamaged, PlayerDamaged);
            EventManager.Instance.AddHandler(GameEvents.OnPlayerDead, PlayerDead);
            EventManager.Instance.AddHandler(GameEvents.OnPlayerRespawned, PlayerRespawned);

            ChangeState(GroundedState);
        }

        private void PlayerRespawned()
        {
            if (_playerController.isPlayerGravitated) ChangeState(GravityChangeState);
            ChangeState(GroundedState);
        }

        private void PlayerDead()
        {
            _playerController.isPlayerDead = true;
            ChangeState(_deadState);
        }

        public void ChangeState(IPlayerState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        private void CanChangeGravity()
        {
            var currentTime = Time.time;
            if (currentTime - _lastGravityChangeTime >= _gravityChangeCooldown)
            {
                _lastGravityChangeTime = currentTime;
                ChangeState(GravityChangeState);
            }
        }

        private void PlayerDamaged(float contactNormal)
        {
            _playerController.isPlayerDamaged = true;
            DamageContactNormal = contactNormal;
            ChangeState(_damagedState);
            if (contactNormal == 0) return;
            //So that player cannot use gravity to escape damage animation except acid
            _lastGravityChangeTime = Time.time;
        }

        public void UpdateCurrentState()
        {
            _currentState.UpdateState();
        }
    }
}