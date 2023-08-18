using Rimaethon.Scripts.Health_Damage;
using UnityEngine;

namespace Rimaethon.Scripts.Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        private Vector3 m_RespawnLocation;
        private Animator m_CheckpointAnimator;
        private readonly int m_AnimatorActiveHash=Animator.StringToHash("isActive");

        [SerializeField] private GameObject checkpointActivationEffect;

        private void Awake()
        {
            m_RespawnLocation = gameObject.transform.position;
        }
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || collision.gameObject.GetComponent<BaseHealth>() == null) return;

            var playerHealth = collision.gameObject.GetComponent<BaseHealth>();

            if (CheckpointTracker.CurrentCheckpoint != null)
                CheckpointTracker.CurrentCheckpoint.m_CheckpointAnimator.SetBool(m_AnimatorActiveHash, false);

            if (CheckpointTracker.CurrentCheckpoint != this && checkpointActivationEffect != null)
                Instantiate(checkpointActivationEffect, transform.position, Quaternion.identity, null);

            CheckpointTracker.CurrentCheckpoint = this;
            m_CheckpointAnimator.SetBool(m_AnimatorActiveHash, true);
        }
    }
}