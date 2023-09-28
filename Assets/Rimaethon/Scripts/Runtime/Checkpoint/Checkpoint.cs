using UnityEngine;

namespace Rimaethon.Runtime.Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        private readonly int _isActive = Animator.StringToHash("isActive");
        private Animator _checkpointAnimator;
        private Vector3 _checkpointLocation;

        private void Awake()
        {
            _checkpointLocation = gameObject.transform.position;
            _checkpointAnimator = GetComponent<Animator>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CheckpointTracker.CurrentCheckpoint == null)
            {
                _checkpointAnimator.SetBool(_isActive, true);
                CheckpointTracker.CurrentCheckpoint = this;

                //  AudioManager.Instance.PlaySFX(5);
                return;
            }

            if (CheckpointTracker.CurrentCheckpoint == this) return;
            CheckpointTracker.CurrentCheckpoint._checkpointAnimator.SetBool(_isActive, false);
        }
    }
}