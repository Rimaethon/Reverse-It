using Rimaethon.Runtime.Core;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Core.Interfaces;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.Player
{
    public class PlayerCombat : CharacterCombat,IHealAble
    {
        private void Start()
        {
            EventManager.Instance.Broadcast(GameEvents.OnHealthChange, Health);
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnPlayerRespawned, HandleRevive);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;

            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerRespawned, HandleRevive);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("EnemyHead"))
                if (other.gameObject.TryGetComponent(out IDamageAble damageable))
                {
                    if (damageable.TeamId == _teamId) return;

                    damageable.TakeDamage(DamageAmount);
                }
        }


        private void HandleRevive()
        {
            Health = characterConfigSo.maxHealth;
            EventManager.Instance.Broadcast(GameEvents.OnHealthChange, Health);
        }

        protected override void HandleDeath()
        {
            EventManager.Instance.Broadcast(GameEvents.OnPlayerDead);
            EventManager.Instance.Broadcast(GameEvents.OnHealthChange, Health);
        }

        protected override void HandleDamage(float contactNormal)
        {
            EventManager.Instance.Broadcast(GameEvents.OnPlayerDamaged, contactNormal);
            EventManager.Instance.Broadcast(GameEvents.OnHealthChange, Health);
        }

        public void ReceiveHealing(IGiveHeal healingSource)
        {
            if (Health >= characterConfigSo.maxHealth) return;

            Health += healingSource.HealAmount;
            EventManager.Instance.Broadcast(GameEvents.OnHealthChange, Health);
        }
    }
}