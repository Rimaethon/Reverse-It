using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    public class WallColliderCheck : MonoBehaviour
    {
        private Rigidbody2D rb;
        public bool IsCollidedWithWall { get; private set; }

        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IsCollidedWithWall = true;
            rb.velocity *= -1f;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsCollidedWithWall = false;
        }
    }
}