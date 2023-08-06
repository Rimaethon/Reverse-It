using UnityEngine;

namespace Pickups
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private GameObject pickUpEffect;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            DoOnPickup(collision);
        }

        protected virtual void DoOnPickup(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (pickUpEffect != null) Instantiate(pickUpEffect, transform.position, Quaternion.identity, null);
            Destroy(gameObject);
        }
    }
}