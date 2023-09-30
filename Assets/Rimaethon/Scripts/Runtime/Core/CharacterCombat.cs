using Rimaethon.Runtime.Scriptable_Objects;
using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Runtime.Core
{
    public class CharacterCombat : MonoBehaviour, IDamageAble
    {
        [SerializeField] protected CharacterConfigSO characterConfigSo;
        private float _invincibilityDuration;

        private float _lastDamageTime;
        protected int _teamId;
        protected float ContactNormal;
        protected int DamageAmount;

        protected int Health;

        protected virtual void Awake()
        {
            _teamId = characterConfigSo.teamId;
            DamageAmount = characterConfigSo.damageAmount;
            Health = characterConfigSo.maxHealth;
            _invincibilityDuration = characterConfigSo.invincibilityDuration;
        }

        public int TeamId => _teamId;


        public void TakeDamage(int damageAmount, float contactNormal)
        {
            if (IsInvincible() || Health <= 0) return;

            if (Health - damageAmount <= 0)
            {
                Health = 0;
                HandleDeath();
            }
            else
            {
                Health -= damageAmount;
                HandleDamage(contactNormal);
            }
        }

        protected virtual void HandleDeath()
        {
        }

        protected virtual void HandleDamage()
        {
        }

        protected virtual void HandleDamage(float contactNormal)
        {
        }

        protected virtual bool IsInvincible()
        {
            var currentTime = Time.time;

            if (_lastDamageTime == 0)
            {
                _lastDamageTime = currentTime;
                return false;
            }

            if (currentTime - _lastDamageTime < _invincibilityDuration) return true;

            _lastDamageTime = currentTime;
            return false;
        }
    }
}