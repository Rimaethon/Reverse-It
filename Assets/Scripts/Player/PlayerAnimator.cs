using Core;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int IsIdle = Animator.StringToHash("isIdle");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsDead = Animator.StringToHash("isDead");

        [Header("Settings")] [Tooltip("The player controller script to read state information from")]
        public PlayerController playerController;

        [Tooltip("The animator component that controls the player's animations")]
        public Animator animator;


        private void Start()
        {
            ReadPlayerStateAndAnimate();
        }


        public void Update()
        {
            ReadPlayerStateAndAnimate();
        }


        private void ReadPlayerStateAndAnimate()
        {
            if (animator == null) return;

            animator.SetBool(IsIdle, playerController.state == PlayerStates.Idle);

            animator.SetBool(IsJumping, playerController.state == PlayerStates.Jump);

            animator.SetBool(IsFalling, playerController.state == PlayerStates.Fall);

            animator.SetBool(IsWalking, playerController.state == PlayerStates.Walk);

            animator.SetBool(IsDead, playerController.state == PlayerStates.Dead);
        }
    }
}