using UnityEngine;

namespace Rimaethon.Scripts.Player
{
    public static class AnimationParameters
    {
        public static readonly int IsIdle = Animator.StringToHash("isIdle");
        public static readonly int IsRunning = Animator.StringToHash("isRunning");
        public static readonly int IsFalling = Animator.StringToHash("isFalling");
        public static readonly int IsJumping = Animator.StringToHash("isJumping");
        public static readonly int IsDead = Animator.StringToHash("isDead");
        public static readonly int IsDamaged = Animator.StringToHash("isDamaged");
    }
}