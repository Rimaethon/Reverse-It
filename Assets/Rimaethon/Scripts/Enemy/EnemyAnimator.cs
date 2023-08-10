using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private EnemyBase enemyComponent;

        [SerializeField] private Animator enemyAnimator;

   
        [SerializeField] private string idleAnimatorParameter = "isIdle";

        [SerializeField] private string movingAnimatorParameter = "isWalking";

        [SerializeField] private string deadAnimatorParameter = "isDead";

        private bool _isEnemyAnimatorNull;

        private bool _isEnemyComponentNull;


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