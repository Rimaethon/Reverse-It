using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.AI.EnemyComponentScripts
{
    public class EnemyCommonData : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        private bool _isPlayerAlive = true;


        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnPlayerDead, PlayerDead);
            EventManager.Instance.AddHandler(GameEvents.OnPlayerRespawn, PlayerAlive);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;

            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerDead, PlayerDead);
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerRespawn, PlayerAlive);
        }


        public Vector3 GetPlayerPosition()
        {
            return playerTransform.position;
        }

        public bool IsPlayerAlive()
        {
            return _isPlayerAlive;
        }

        private void PlayerDead()
        {
            _isPlayerAlive = false;
        }

        private void PlayerAlive()
        {
            _isPlayerAlive = true;
        }
    }
}