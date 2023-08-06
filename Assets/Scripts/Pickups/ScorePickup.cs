using Health_Damage;
using UnityEngine;
using Utility;

namespace Pickups
{
    public class ScorePickup : Pickup
    {
        [SerializeField] int scoreAmount = 1;


        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Health>() != null)
                GameManager.AddScore(scoreAmount);
            base.DoOnPickup(collision);
        }
    }
}