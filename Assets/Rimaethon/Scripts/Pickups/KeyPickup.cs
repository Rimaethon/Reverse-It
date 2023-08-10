using Health_Damage;
using Keys_Doors;
using UnityEngine;

namespace Pickups
{
    public class KeyPickup : Pickup
    {
        [Header("Key Settings")]
        [Tooltip("The ID of the key used to determine which doors it unlocks (unlocks doors with matching IDs)\n" +
                 "A key ID of 0 allows the player to open unlocked doors, and is therefore pointless.")]
        public int keyID;


        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Health>() != null)
                KeyRing.AddKey(keyID);
            base.DoOnPickup(collision);
        }
    }
}