using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The player controller script to read state information from")]
        public PlayerController playerController;
        [Tooltip("The animator component that controls the player's animations")]
        public Animator animator;

        private static readonly int IsIdle = Animator.StringToHash("isIdle");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsDead = Animator.StringToHash("isDead");


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
            if (animator == null)
            {
                return;
            }

            animator.SetBool(IsIdle, playerController.state == PlayerController.PlayerState.Idle);

            animator.SetBool(IsJumping, playerController.state == PlayerController.PlayerState.Jump);

            animator.SetBool(IsFalling, playerController.state == PlayerController.PlayerState.Fall);

            animator.SetBool(IsWalking, playerController.state == PlayerController.PlayerState.Walk);

            animator.SetBool(IsDead, playerController.state == PlayerController.PlayerState.Dead);
        }
    }
}
