using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class SceneChangingArea : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) EventManager.Instance.Broadcast(GameEvents.OnSceneChange, sceneIndex);
        }
    }
}