namespace Rimaethon.Scripts.Enemy.Enemy_State_Machine
{
    public class AttackEnemyState : BaseEnemyState
    {
        private readonly IAnimationBehavior _animationBehavior;

        public AttackEnemyState(Enemy enemy, IAnimationBehavior animationBehavior) : base(enemy)
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