using Rimaethon.Runtime.Pickups;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Scripts.Pickups
{
    public class GemPickup : Pickup
    {
        [SerializeField] private int scoreAmount = 1;


        protected override void DoOnPickup(Collider2D collision)
        {
            base.DoOnPickup(collision);
            AudioManager.Instance.PlaySFX(SFXClips.ScorePickUp);
            EventManager.Instance.Broadcast(GameEvents.OnGemCollected, scoreAmount);
        }
    }
}