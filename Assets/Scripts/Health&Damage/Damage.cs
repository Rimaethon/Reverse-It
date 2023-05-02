using UnityEngine;

namespace Health_Damage
{
    public class Damage : MonoBehaviour
    {
        [Header("Team Settings")]
        [Tooltip("The team associated with this damage")]
        public int teamId;

        [Header("Damage Settings")]
        [Tooltip("How much damage to deal")]
        public int damageAmount = 1;
        [Tooltip("Whether or not to destroy the attached game object after dealing damage")]
        public bool destroyAfterDamage = true;
        [Tooltip("Whether or not to apply damage when triggers collide")]
        public bool dealDamageOnTriggerEnter;
        [Tooltip("Whether or not to apply damage when triggers stay, for damage over time")]
        public bool dealDamageOnTriggerStay;
        [Tooltip("Whether or not to apply damage on non-trigger collider collisions")]
        public bool dealDamageOnCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (dealDamageOnTriggerEnter)
            {
                DealDamage(collision.gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (dealDamageOnTriggerStay)
            {
                DealDamage(collision.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (dealDamageOnCollision)
            {
                DealDamage(collision.gameObject);
            }
        }

        private void DealDamage(GameObject collisionGameObject)
        {
            Health collidedHealth = collisionGameObject.GetComponent<Health>();
            if (collidedHealth != null)
            {
                if (collidedHealth.teamId != this.teamId)
                {
                    collidedHealth.TakeDamage(damageAmount);
                    if (destroyAfterDamage)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
