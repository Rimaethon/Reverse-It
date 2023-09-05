using System.Linq;
using UnityEngine;

namespace Rimaethon.Scripts.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayers;

        [SerializeField] private GameObject landingEffect;

        private bool _groundedLastCheck;



        [SerializeField] private float groundRayLength = 0.7f;

        
        public bool CheckGrounded()
        {
            Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + 0.1f);

            // Cast the ray downwards
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundRayLength, groundLayers);

            // Check if the ray hit any ground layer
            if (hit.collider != null)
            {
                if (landingEffect && !_groundedLastCheck)
                    Instantiate(landingEffect, transform.position, Quaternion.identity, null);

                _groundedLastCheck = true;
                Debug.Log("I hit the ground!");
                return true;
            }

            _groundedLastCheck = false;
            return false;
        }

        // Visualization of ray in the editor
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.1f), Vector2.down * groundRayLength);
        }
    }
}