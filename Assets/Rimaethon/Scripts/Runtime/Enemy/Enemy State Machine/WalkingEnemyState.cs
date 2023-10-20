using Rimaethon.Scripts.Enemy.Enemy_State_Machine;

namespace Rimaethon.Scripts.Enemy
{
    public class WalkingEnemyState : BaseEnemyState
    {
        private IAnimationBehavior _animationBehavior;

        public WalkingEnemyState(Enemy enemy, IAnimationBehavior animationBehavior) : base(enemy)
        {
            _animationBehavior = animationBehavior;
        }

        public override void Execute()
        {
            base.Execute();
            enemy.MoveEnemy();
        }
    }
}