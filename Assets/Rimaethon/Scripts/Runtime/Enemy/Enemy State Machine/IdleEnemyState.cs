namespace Rimaethon.Scripts.Enemy.Enemy_State_Machine
{
    public class IdleEnemyState : BaseEnemyState
    {
        private readonly IAnimationBehavior _animationBehavior;

        public IdleEnemyState(Enemy enemy, IAnimationBehavior animationBehavior) : base(enemy)
        {
            _animationBehavior = animationBehavior;
        }

        public override void Enter()
        {
            base.Enter();
            _animationBehavior.PlayAnimation();
        }

        public override void Exit()
        {
            base.Exit();
            _animationBehavior.StopAnimation();
        }
    }
}