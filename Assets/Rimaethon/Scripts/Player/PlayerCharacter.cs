using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private IPlayerState currentState;

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }

        currentState = newState;
        currentState.Enter(this);
    }

    private void Update()
    {
        currentState?.Update(this);
    }
}