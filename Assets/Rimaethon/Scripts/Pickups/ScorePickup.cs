using Rimaethon.Scripts.Health_Damage;
using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Pickups
{
    public class ScorePickup : Pickup
    {
        [SerializeField] int scoreAmount = 1;


        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<BaseHealth>() != null)
                GameManager.AddScore(scoreAmount);
            base.DoOnPickup(collision);
        }
    }
}