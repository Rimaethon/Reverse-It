using System;
using Rimaethon.Scripts.Core.Interfaces;
using Rimaethon.Scripts.Health_Damage;
using UnityEngine;

namespace Rimaethon.Scripts.Environment
{
    public class Acid : DamageBase
    {
        public int DamageAmount { get; }
        public void ApplyDamage(IDamageable target)
        {
            target.TakeDamage(this);

        }

        private void OnTriggerStay(Collider other)
        {
            other.gameObject.TryGetComponent(out IDamageable damageable);
            ApplyDamage(damageable);
        }
    }
}
