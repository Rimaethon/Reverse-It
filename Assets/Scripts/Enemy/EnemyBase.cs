using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("How fast this enemy moves")]
        public float moveSpeed = 2f;

   
        public enum EnemyState { Walking, Dead, Idle }

        [Tooltip("The state the enemy is in for animation playback")]
        public EnemyState enemyState;

  
        protected virtual void Start()
        {
            Setup();
        }

   
        protected virtual void Update()
        {
            // Every frame, get the desired movement of this enemy, then move it.
            Vector3 movement = GetMovement();
            MoveEnemy(movement);
        }

   
        protected virtual void Setup()
        {

        }

      
        protected virtual Vector3 GetMovement()
        {
            return Vector3.zero;
        }


        private void MoveEnemy(Vector3 movement)
        {
            transform.position += movement;
        }
    }
}
