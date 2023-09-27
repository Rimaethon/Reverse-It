using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.Pickups
{
    public class GoalPickup : Pickup
    {
        [SerializeField] private int sceneIndex;

        protected override void DoOnPickup(Collider2D collision)
        {
            AudioManager.Instance.PlaySFX(SFXClips.GoalPickup);

            EventManager.Instance.Broadcast(GameEvents.OnSceneChange, sceneIndex);


            base.DoOnPickup(collision);
        }
    }
}