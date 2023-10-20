namespace Rimaethon.Scripts.Enemy.Enemy_State_Machine
{
    public interface IStateMachine
    {
        void ChangeState(IState newState);
        void Update();
    }
}