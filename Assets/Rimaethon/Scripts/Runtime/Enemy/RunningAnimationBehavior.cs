using Rimaethon.Scripts.Enemy;
using UnityEngine;

namespace Rimaethon.Runtime.Enemy
{
    public class RunningAnimationBehavior:IAnimationBehavior
    {
        public RunningAnimationBehavior(Animator animator)
        {
            animator.SetBool("isWalking", true);
        }
        public void PlayAnimation()
        {
        }

        public void StopAnimation()
        {
        }
    }
}