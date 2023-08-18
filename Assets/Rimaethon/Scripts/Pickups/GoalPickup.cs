using Rimaethon.Scripts.Health_Damage;
using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Pickups
{
    public class GoalPickup : Pickup
    {
        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<BaseHealth>() != null)
                if (GameManager.Instance != null)
                    GameManager.Instance.LevelCleared();
            base.DoOnPickup(collision);
        }
    }
}