using Rimaethon.Runtime.Checkpoint;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.Player
{
    public class PlayerRespawnHandler : MonoBehaviour
    {
        private Vector3 _spawnPosition;

        private void Awake()
        {
            _spawnPosition = transform.position;
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnPlayerRespawned, OnPlayerRespawn);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerRespawned, OnPlayerRespawn);
        }

        private void OnPlayerRespawn()
        {
            transform.position = CheckpointTracker.CurrentCheckpoint == null
                ? _spawnPosition
                : CheckpointTracker.CurrentCheckpoint.transform.position;
        }
    }
}