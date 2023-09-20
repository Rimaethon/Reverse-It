namespace Rimaethon.Scripts.Enemy.Enemy_State_Machine
{
    public class EnemyStateMachine : IStateMachine
    {
        private IState _currentState;

        public EnemyStateMachine(IState initialState)
        {
            _currentState = initialState;
            _currentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Execute();
        }
    }
}