using Rimaethon.Runtime.Pickups;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Scripts.Pickups
{
    public class RedGemPickup : GemPickup
    {



        protected override void DoOnPickup(Collider2D collision)
        {
            base.DoOnPickup(collision);
            EventManager.Instance.Broadcast(GameEvents.OnRedGemCollected);
        }
    }
}