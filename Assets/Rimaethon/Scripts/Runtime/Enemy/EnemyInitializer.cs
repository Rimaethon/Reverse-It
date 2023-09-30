using Rimaethon.Runtime.Enemy;
using Rimaethon.Scripts.Enemy;
using Rimaethon.Scripts.Enemy.Enemy_State_Machine;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyInitializer : MonoBehaviour
{
    private Enemy _enemy;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>(); 
        var animator = GetComponent<Animator>();
        IAnimationBehavior idleAnimation = new RunningAnimationBehavior(animator);
        IAnimationBehavior walkingAnimation = new RunningAnimationBehavior(animator);
        BaseEnemyState idleEnemyState = new IdleEnemyState(_enemy, idleAnimation);
        BaseEnemyState attackEnemyState = new AttackEnemyState(_enemy, walkingAnimation);
        BaseEnemyState walkingEnemyState = new WalkingEnemyState(_enemy, walkingAnimation);
        IStateMachine stateMachine = new EnemyStateMachine(walkingEnemyState);
        _enemy.SetStateMachine(stateMachine);
    }
}