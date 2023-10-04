using Rimaethon.Runtime.Pickups;
using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Pickups
{
    public class HealthPickup : Pickup, IGiveHeal
    {
        public int HealAmount => 1;


        public void GiveHeal(IHealAble target)
        {
            target.ReceiveHealing(this);
        }


        protected override void DoOnPickup(Collider2D collision)
        {
            collision.gameObject.TryGetComponent(out IHealAble health);
            if (health == null) return;

            GiveHeal(health);
        }
    }
}