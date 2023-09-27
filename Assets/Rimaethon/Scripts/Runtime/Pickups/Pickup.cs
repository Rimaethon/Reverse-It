using UnityEngine;

namespace Rimaethon.Runtime.Pickups
{
    public class Pickup : MonoBehaviour
    {


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            DoOnPickup(collision);
        }

        protected virtual void DoOnPickup(Collider2D collision)
        {
            Destroy(gameObject);
        }
    }
}