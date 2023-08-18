using Rimaethon.Scripts.Core.Interfaces;
using Rimaethon.Scripts.Health_Damage;
using UnityEngine;

namespace Rimaethon.Scripts.Pickups
{
    public class HealthPickup : Pickup,IGiveHeal
    {
        private int m_HealingAmount = 1;
        public int HealAmount => m_HealingAmount;


        protected override void DoOnPickup(Collider2D collision)
        {
            collision.gameObject.TryGetComponent(out IHealAble health);
            if(health == null) return;
            
            GiveHeal(health);
            
        }


        public void GiveHeal(IHealAble target)
        {
            target.ReceiveHealing(this);
        }
    }
}