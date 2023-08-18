using UnityEngine;

namespace Rimaethon.Scripts.Health_Damage
{
    [RequireComponent(typeof(Animator))]
    public class DeathEffectAnimationHandler : MonoBehaviour
    {
        private static readonly int IsDead = Animator.StringToHash("isDead");

        private void Start()
        {
            SetIsDead();
        }


        private void SetIsDead()
        {
            GetComponent<Animator>().SetTrigger(IsDead);
        }
    }
}