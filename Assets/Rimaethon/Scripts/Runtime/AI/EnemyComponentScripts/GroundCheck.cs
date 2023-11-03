using UnityEngine;

namespace Rimaethon.Runtime.AI.EnemyComponentScripts
{
    //Works with collision layer matrix 
    public class GroundCheck : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }

        protected virtual void Awake()
        {
            IsGrounded = true;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            IsGrounded = true;
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            IsGrounded = false;
        }
    }
}