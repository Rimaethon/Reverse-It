using Rimaethon.Scripts.Health_Damage;
using Rimaethon.Scripts.Keys_Doors;
using UnityEngine;

namespace Rimaethon.Scripts.Pickups
{
    public class KeyPickup : Pickup
    {
        [Header("Key Settings")]
        [Tooltip("The ID of the key used to determine which doors it unlocks (unlocks doors with matching IDs)\n" +
                 "A key ID of 0 allows the player to open unlocked doors, and is therefore pointless.")]
        public int keyID;


        protected override void DoOnPickup(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.gameObject.GetComponent<BaseHealth>() != null)
                KeyRing.AddKey(keyID);
            base.DoOnPickup(collision);
        }
    }
}