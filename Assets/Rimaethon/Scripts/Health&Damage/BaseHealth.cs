using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Health_Damage
{
    public abstract class BaseHealth : MonoBehaviour, IDamageable
    {
        protected int m_TeamId;
        public int TeamId { get; }

        protected bool m_IsInvincible;
        private float m_InvincibilityTime = 3f;
        protected float _timeToBecomeDamagableAgain;

        public int Health
        {
            get => m_Health;
            protected set => m_Health = Mathf.Clamp(value, 0, 100);
        }
        private int m_Health=100;

        public void TakeDamage(IDamageSource source)
        {
            if (m_IsInvincible) return;
            Health -= source.DamageAmount;
        }

        protected virtual void Update()
        {
            InvincibilityCheck();
        }

        private void InvincibilityCheck()
        {
            if (_timeToBecomeDamagableAgain <= Time.time) m_IsInvincible = false;
        }
    }
}