using Rimaethon.Scripts.Core.Enums;
using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
       

        [SerializeField] protected float moveSpeed = 2f;

        protected EnemyStates enemyStates;
        
        public EnemyStates EnemyStates => enemyStates;

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