using Rimaethon.Scripts.Enemy;
using UnityEngine;

namespace Rimaethon.Runtime.Enemy
{
    public class IdleAnimationBehavior:IAnimationBehavior
    {
        public IdleAnimationBehavior(Animator animator)
        {
            animator.SetBool("isIdle", false);
        }
        public void PlayAnimation()
        {
            
        }

        public void StopAnimation()
        {
        }
    }
}