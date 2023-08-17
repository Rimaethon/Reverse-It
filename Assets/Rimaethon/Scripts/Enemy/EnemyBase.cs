using System;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IDamageSource
    {
        public int DamageAmount { get; }
        protected int m_DamageAmount;

        [SerializeField] protected float moveSpeed = 2f;

        protected EnemyStates enemyStates;
        
        public EnemyStates EnemyStates => enemyStates;

        protected virtual void Awake()
        {
            Setup();
        }

      

        protected virtual void Update()
        {
            var movement = GetMovement();
            MoveEnemy(movement);
        }


        protected virtual void Setup()
        {
        }


        protected virtual Vector3 GetMovement()
        {
            return Vector3.zero;
        }


        private void MoveEnemy(Vector3 movement)
        {
            transform.position += movement;
        }


        private void OnCollisionEnter(Collision other)
        {
          other.gameObject.TryGetComponent( out IDamageable damageable);
          if (damageable==null) return;
            
          ApplyDamage((IDamageable) damageable);
        }

        public void ApplyDamage(IDamageable target)
        {
            target.TakeDamage(this);
        }
    }
}