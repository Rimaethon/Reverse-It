using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Health_Damage
{
    public class DamageBase : MonoBehaviour,IDamageSource
    {
        private int teamId;

        protected int damageAmount;


        private bool dealDamageOnTriggerEnter;

        private bool dealDamageOnTriggerStay;

        private bool dealDamageOnCollision;

        private void OnCollisionEnter2D(Collision2D collision)
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
        }

        

        public int DamageAmount { get; }
        public void ApplyDamage(IDamageable target)
        {
            throw new System.NotImplementedException();
        }
    }
}