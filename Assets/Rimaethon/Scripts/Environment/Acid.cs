using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Environment
{
    public class Acid : MonoBehaviour,IDamageSource
    {
        public int DamageAmount { get; }

        public void ApplyDamage(IDamageable target)
        {
            target.TakeDamage(this);

        }
        
        
    }
}
