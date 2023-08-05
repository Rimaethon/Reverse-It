using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public enum EnemyState
        {
            Walking,
            Dead,
            Idle
        }

        public float moveSpeed = 2f;

        public EnemyState enemyState;


        protected virtual void Start()
        {
            Setup();
        }


        protected virtual void Update()
        {
            var movement = GetMovement();
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