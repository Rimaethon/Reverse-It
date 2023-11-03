using UnityEngine;

namespace Rimaethon.Runtime.AI.EnemyComponentScripts
{
    public class EnemyGroundCheck : GroundCheck
    {
        private Rigidbody2D _rb;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponentInParent<Rigidbody2D>();
        }


        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            _rb.velocity *= -1f;
        }
    }
}