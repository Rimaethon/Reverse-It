using Health_Damage;
using UnityEngine;

namespace Pickups
{
    public class ExtraLifePickup : Pickup
    {
        [SerializeField] private int extraLives = 1;


        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Health>() != null)
            {
                var playerHealth = collision.gameObject.GetComponent<Health>();
                playerHealth.AddLives(extraLives);
            }

            base.DoOnPickup(collision);
        }
    }
}