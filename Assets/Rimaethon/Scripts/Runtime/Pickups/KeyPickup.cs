using Rimaethon.Runtime.Keys_Doors;
using UnityEngine;

namespace Rimaethon.Runtime.Pickups
{
    public class KeyPickup : Pickup
    {
        [SerializeField] private int keyID;


        protected override void DoOnPickup(Collider2D collision)
        {
            base.DoOnPickup(collision);
            PlayerKeyHolder.AddKey(keyID);
        }
    }
}