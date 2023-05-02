using Player;
using UnityEngine;
using Utility;

namespace Health_Damage
{
    public class Head : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The health component associated with this head")]
        public Health associatedHealth;
        [Tooltip("The amount of damage to deal when jumped on")]
        public int damage = 1;

   
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Feet"))
            {
                associatedHealth.TakeDamage(damage);
                BouncePlayer();
            }
        }


        private void BouncePlayer()
        {
            PlayerController playerController = GameManager.Instance.player.GetComponentInChildren<PlayerController>();
            if (playerController != null)
            {
                playerController.Bounce();
            }
        }
    }
}
