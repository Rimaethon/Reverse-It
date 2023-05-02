using Health_Damage;
using UnityEngine;

namespace Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The location this checkpoint will respawn the player at")]
        public Transform respawnLocation;
        [Tooltip("The animator for this checkpoint")]
        public Animator checkpointAnimator;
        [Tooltip("The name of the parameter in the animator which determines if this checkpoint displays as active")]
        public string animatorActiveParameter = "isActive";
        [Tooltip("The effect to create when activating the checkpoint")]
        public GameObject checkpointActivationEffect;

   
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || collision.gameObject.GetComponent<Health>() == null) return;
            
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.SetRespawnPoint(respawnLocation.position);

            if (CheckpointTracker.CurrentCheckpoint != null)
            {
                CheckpointTracker.CurrentCheckpoint.checkpointAnimator.SetBool(animatorActiveParameter, false);
            }

            if (CheckpointTracker.CurrentCheckpoint != this && checkpointActivationEffect != null)
            {
                Instantiate(checkpointActivationEffect, transform.position, Quaternion.identity, null);
            }

            CheckpointTracker.CurrentCheckpoint = this;
            checkpointAnimator.SetBool(animatorActiveParameter, true);
        }
    }
}
