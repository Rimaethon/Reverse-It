using Health_Damage;
using UnityEngine;
using Utility;

namespace Pickups
{
    public class GoalPickup : Pickup
    {
        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Health>() != null)
                if (GameManager.Instance != null)
                    GameManager.Instance.LevelCleared();
            base.DoOnPickup(collision);
        }
    }
}