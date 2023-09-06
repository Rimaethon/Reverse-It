public interface IPlayerState
{
    void Enter(PlayerCharacter player);
    void Update(PlayerCharacter player);
    void Exit(PlayerCharacter player);
}