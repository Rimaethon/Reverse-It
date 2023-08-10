using Player;
using UnityEngine;
using Utility;

namespace Health_Damage
{
    public class Head : MonoBehaviour
    {
        [SerializeField] private Health associatedHealth;
        [SerializeField] private int damage = 1;


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
            var playerController = GameManager.Instance.player.GetComponentInChildren<PlayerController>();
            if (playerController != null) playerController.Bounce();
        }
    }
}