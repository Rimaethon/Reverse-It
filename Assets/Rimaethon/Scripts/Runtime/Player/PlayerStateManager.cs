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

        public void OnPlayerDisabled()
        {
            GroundedState = null;
            AirborneState = null;
            JumpingState = null;
            RunningState = null;
            GravityChangeState = null;
            _currentState = null;
            _damagedState = null;
            _deadState = null;
            if(EventManager.Instance == null) return;
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerGravityChange, CanChangeGravity);
            EventManager.Instance.RemoveHandler<float>(GameEvents.OnPlayerDamaged, PlayerDamaged);
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerDead, PlayerDead);
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerRespawned, PlayerRespawned);
        }
        public float DamageContactNormal { get; private set; }

        public IPlayerState GroundedState { get; private set; }
        public IPlayerState AirborneState { get; private set; }
        public IPlayerState JumpingState { get; private set; }
        public IPlayerState RunningState { get; private set; }
        public IPlayerState GravityChangeState { get; private set; }

        private void InitializeStates()
        {
            GroundedState = new PlayerGroundedState(_playerController, _animator, this);
            AirborneState = new PlayerAirborneState(_playerController, _animator, this, _rb);
            JumpingState = new PlayerJumpingState(_playerController, _animator, this, _rb);
            RunningState = new PlayerRunningState(_playerController, _animator, this);

            _deadState = new PlayerDeadState(_playerController, _animator, this, _rb);
            _damagedState = new PlayerDamagedState(_playerController, _transform, this, _rb, _animator);
            GravityChangeState = new PlayerGravityChangeState(_playerController, _rb, this);

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
            AudioManager.Instance.PlaySFX(SFXClips.PlayerHurt);
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
