using Rimaethon.Scripts.Core.Enums;
using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private EnemyBase m_EnemyComponent;

        [SerializeField] private Animator enemyAnimator;

   
        [SerializeField] private string idleAnimatorParameter = "isIdle";

        [SerializeField] private string movingAnimatorParameter = "isWalking";

        [SerializeField] private string deadAnimatorParameter = "isDead";

        private bool _isEnemyAnimatorNull;

        private bool _isEnemyComponentNull;


        private void Awake()
        {
            m_EnemyComponent = GetComponent<EnemyBase>();
        }

        private void Start()
        {
            _isEnemyAnimatorNull = enemyAnimator == null;
            _isEnemyComponentNull = m_EnemyComponent == null;
            SetAnimatorState();
        }


        private void Update()
        {
            SetAnimatorState();
        }


        private void SetAnimatorState()
        {
            if (_isEnemyComponentNull || _isEnemyAnimatorNull) return;

            enemyAnimator.SetBool(idleAnimatorParameter, m_EnemyComponent.EnemyStates == EnemyStates.Idle);

            enemyAnimator.SetBool(movingAnimatorParameter, m_EnemyComponent.EnemyStates == EnemyStates.Walking);

            enemyAnimator.SetBool(deadAnimatorParameter, m_EnemyComponent.EnemyStates == EnemyStates.Dead);
        }
    }
}