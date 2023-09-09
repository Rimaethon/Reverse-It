using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Core.Interfaces;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Scripts.Health_Damage
{
    public abstract class BaseHealth : MonoBehaviour, IDamageable
    {
        protected int m_TeamId;
        public int TeamId { get; }
        private bool m_IsInvincible;
        private float m_InvincibilityTime = 3f;
        protected float _timeToBecomeDamagableAgain;
        private int m_Health=100;

        public int Health
        {
            get => m_Health;
            protected set => m_Health = Mathf.Clamp(value, 0, 100);
        }

        public void TakeDamage(IDamageSource source)
        {
            if (m_IsInvincible) return;
            Health -= source.DamageAmount;
            EventManager.Instance.Broadcast(GameEvents.OnHealthChange, Health);
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