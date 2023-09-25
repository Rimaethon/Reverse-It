using Rimaethon.Runtime.Core;
using Rimaethon.Scripts.Core.Interfaces;
using Rimaethon.Scripts.Player;
using UnityEngine;

namespace Rimaethon.Runtime.Enemy
{
    public class EnemyCombat : CharacterCombat
    {
        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.otherCollider.CompareTag("EnemyBody"))
                if (other.gameObject.TryGetComponent(out IDamageAble damageable))
                {
                    if (damageable.TeamId == _teamId) return;

                    ContactNormal = other.GetContact(0).point.x - transform.position.x;

                    damageable.TakeDamage(DamageAmount, ContactNormal);
                }
        }


        protected override void HandleDeath()
        {
            _animator.SetTrigger(AnimationParameters.IsDead);
        }
    }
}