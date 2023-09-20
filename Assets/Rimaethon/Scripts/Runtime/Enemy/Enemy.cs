using Rimaethon.Scripts.Enemy.Enemy_State_Machine;
using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public IState _currentState;
        public IState _previousState;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private IStateMachine _stateMachine;
        protected int m_DamageAmount;

        protected float moveSpeed = 2f;


        protected void Awake()
        {
            Initialize();
            //     _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            //   _rigidbody2D.velocity = Vector2.zero;
        }


        public void SetStateMachine(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Initialize()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void TurnAround()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            moveSpeed = -moveSpeed;
        }

        public void MoveEnemy()
        {
            gameObject.transform.Translate(0.002f * moveSpeed, 0, 0);
        }


        public void HandleWallCollision()
        {
            TurnAround();
        }
    }
}