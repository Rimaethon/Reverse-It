


using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Health_Damage
{
    public class PlayerDamager:DamageBase
    {
        
        void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.TryGetComponent(out EnemyHealth enemy);
            if (enemy != null && IsHittingFromAbove())
            {
                enemy.TakeDamage(this); // Example damage value
            }
        }
        
        
        
        bool IsHittingFromAbove()
        {
            Vector3 rayDirection = Vector3.down;

            Vector3 rayStart = transform.position + new Vector3(0, -0.5f, 0);

            float rayLength = 0.6f;

            RaycastHit hit;
            if (Physics.Raycast(rayStart, rayDirection, out hit, rayLength))
            {
                // Check if the ray hit an object with the "Enemy" tag
                if (hit.collider.CompareTag("Enemy"))
                {
                    return true; // The ray hit an enemy from above
                }
            }

            return false; // No hit from above
        }
        
    }

   
}