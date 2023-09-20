namespace Rimaethon.Scripts.Enemy.Enemy_State_Machine
{
    public abstract class BaseEnemyState : IState
    {
        protected Enemy enemy;

        protected BaseEnemyState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Enter()
        {
        }

        public virtual void Execute()
        {
        }

        public virtual void Exit()
        {
        }
    }
}