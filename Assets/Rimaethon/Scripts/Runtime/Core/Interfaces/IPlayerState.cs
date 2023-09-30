namespace Rimaethon.Scripts.Player
{
    public interface IPlayerState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
        void CheckSwitchState();
    }
}