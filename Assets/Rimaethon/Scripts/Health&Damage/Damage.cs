using UnityEngine;

namespace Health_Damage
{
    public class Damage : MonoBehaviour
    {
        [SerializeField] private int teamId;

        [SerializeField] private int damageAmount = 1;

        [SerializeField] private bool destroyAfterDamage = true;

        [SerializeField] private bool dealDamageOnTriggerEnter;

        [SerializeField] private bool dealDamageOnTriggerStay;

        [SerializeField] private bool dealDamageOnCollision;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (dealDamageOnCollision) DealDamage(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (dealDamageOnTriggerEnter) DealDamage(collision.gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (dealDamageOnTriggerStay) DealDamage(collision.gameObject);
        }

        private void DealDamage(GameObject collisionGameObject)
        {
            var collidedHealth = collisionGameObject.GetComponent<Health>();
            if (collidedHealth != null)
                if (collidedHealth.teamId != teamId)
                {
                    collidedHealth.TakeDamage(damageAmount);
                    if (destroyAfterDamage) Destroy(gameObject);
                }
        }
    }
}