using Health_Damage;
using UnityEngine;

namespace Pickups
{
    public class ExtraLifePickup : Pickup
    {
        [Header("Extra Life Settings")]
        [Tooltip("How many Lives to give")]
        public int extraLives = 1;


        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Health>() != null)
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                playerHealth.AddLives(extraLives);
            }
            base.DoOnPickup(collision);
        }
    }
}
