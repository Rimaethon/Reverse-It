using Health_Damage;
using UnityEngine;

namespace Pickups
{
    public class HealthPickup : Pickup
    {
        [Header("Healing Settings")] [Tooltip("The healing to apply")]
        public int healingAmount = 1;


        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Health>() != null)
            {
                var playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth.currentHealth < playerHealth.maximumHealth)
                {
                    playerHealth.ReceiveHealing(healingAmount);
                    base.DoOnPickup(collision);
                }
            }
        }
    }
}