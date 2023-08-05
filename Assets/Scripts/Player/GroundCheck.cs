﻿using System.Linq;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private Collider2D groundCheckCollider;

        [SerializeField] private GameObject landingEffect;

        private bool _groundedLastCheck;

        
        private void Start()
        {
            groundCheckCollider = GetComponent<Collider2D>();
            GetCollider();
        }

        private void GetCollider()
        {
            if (groundCheckCollider == null)
            {
                groundCheckCollider = gameObject.GetComponent<Collider2D>();
            }
        }

     
        public bool CheckGrounded()
        {
            if (groundCheckCollider == null)
            {
                GetCollider();
            }

            Collider2D[] overlaps = new Collider2D[5];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.layerMask = groundLayers;
            groundCheckCollider.OverlapCollider(contactFilter, overlaps);

            if ((from overlapCollider in overlaps where overlapCollider != null select contactFilter.layerMask.value & (int)Mathf.Pow(2, overlapCollider.gameObject.layer)).Any(match => match > 0))
            {
                if (landingEffect && !_groundedLastCheck)
                {
                    Instantiate(landingEffect, transform.position, Quaternion.identity, null);
                }

                _groundedLastCheck = true;
                return true;
            }

            _groundedLastCheck = false;
            return false;
        }
    }
}
