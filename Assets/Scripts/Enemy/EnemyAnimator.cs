using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The enemy component responsible for tracking the enemy's state")]
        public EnemyBase enemyComponent;
        [Tooltip("The animator to use to animate the enemy")]
        public Animator enemyAnimator;

        [Header("Animator Parameter Names")]
        [Tooltip("The name of the boolean parameter in the animator which causes a transition to the idle state")]
        public string idleAnimatorParameter = "isIdle";
        [Tooltip("The name of the boolean parameter in the animator which causes a transition to the walking/moving state")]
        public string movingAnimatorParameter = "isWalking";
        [Tooltip("The name of the trigger parameter in the animator which causes a transition to the dead state")]
        public string deadAnimatorParameter = "isDead";

        private bool _isEnemyComponentNull;
        private bool _isEnemyAnimatorNull;


        private void Start()
        {
            _isEnemyAnimatorNull = enemyAnimator == null;
            _isEnemyComponentNull = enemyComponent == null;
            SetAnimatorState();
        }

  
        private void Update()
        {
            SetAnimatorState();
        }

   
        private void SetAnimatorState()
        {
            if (_isEnemyComponentNull || _isEnemyAnimatorNull) return;
            
            enemyAnimator.SetBool(idleAnimatorParameter, enemyComponent.enemyState == EnemyBase.EnemyState.Idle);

            enemyAnimator.SetBool(movingAnimatorParameter, enemyComponent.enemyState == EnemyBase.EnemyState.Walking);

            enemyAnimator.SetBool(deadAnimatorParameter, enemyComponent.enemyState == EnemyBase.EnemyState.Dead);
        }
    }
}
