using Health_Damage;
using UnityEngine;

namespace Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform respawnLocation;
        [SerializeField] private Animator checkpointAnimator;
        [SerializeField] private string animatorActiveParameter = "isActive";
        [SerializeField] private GameObject checkpointActivationEffect;

   
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
