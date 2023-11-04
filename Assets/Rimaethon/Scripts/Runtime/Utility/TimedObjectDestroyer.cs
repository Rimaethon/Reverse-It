using UnityEngine;

namespace Rimaethon.Scripts.Utility
{
    public class TimedObjectDestroyer : MonoBehaviour
    {
        private static bool _quitting;

        [Header("Settings:")] [Tooltip("The lifetime of this gameobject")]
        public float lifetime = 5.0f;

        [Tooltip("Whether or not to destroy child gameobjects when this gameobject is destroyed")]
        public bool destroyChildrenOnDeath = true;

        private float _timeAlive;


        private void Update()
        {
            if (_timeAlive > lifetime)
                Destroy(gameObject);
            else
                _timeAlive += Time.deltaTime;
        }


        private void OnDestroy()
        {
            if (destroyChildrenOnDeath && !_quitting && Application.isPlaying)
            {
                var childCount = transform.childCount;
                for (var i = childCount - 1; i >= 0; i--)
                {
                    var childObject = transform.GetChild(i).gameObject;
                    if (childObject != null) Destroy(childObject);
                }
            }

            transform.DetachChildren();
        }


        private void OnApplicationQuit()
        {
            _quitting = true;
            DestroyImmediate(gameObject);
        }
    }
}