using UnityEngine;

namespace Utility
{
    public class TimedObjectDestroyer : MonoBehaviour
    {
        [Header("Settings:")]
        [Tooltip("The lifetime of this gameobject")]
        public float lifetime = 5.0f;

        private float _timeAlive;

        [Tooltip("Whether or not to destroy child gameobjects when this gameobject is destroyed")]
        public bool destroyChildrenOnDeath = true;


        private void Update()
        {
            if (_timeAlive > lifetime)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _timeAlive += Time.deltaTime;
            }
        }

        private static bool _quitting;

       
        private void OnApplicationQuit()
        {
            _quitting = true;
            DestroyImmediate(this.gameObject);
        }

      
        private void OnDestroy()
        {
            if (destroyChildrenOnDeath && !_quitting && Application.isPlaying)
            {
                int childCount = transform.childCount;
                for (int i = childCount - 1; i >= 0; i--)
                {
                    GameObject childObject = transform.GetChild(i).gameObject;
                    if (childObject != null)
                    {
                        Destroy(childObject);
                    }
                }
            }
            transform.DetachChildren();
        }
    }
}
