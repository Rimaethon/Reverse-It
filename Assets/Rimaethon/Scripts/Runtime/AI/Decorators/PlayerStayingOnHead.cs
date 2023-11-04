using System.Threading.Tasks;
using TheKiwiCoder;
using UnityEngine;

public class PlayerStayingOnHead : DecoratorNode
{
    //Okay I couldn't find a better name. This script is for making fast movement to left and right if player is on top of the enemy.

    private bool _isWaiting;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }


    protected override State OnUpdate()
    {
        if (!_isWaiting)
        {
            if (child.state == State.Failure)
            {
                blackboard.moveSpeed = Mathf.Sin(Time.time * 10) * 5;
                child.state = State.Running;
            }
            else
            {
                child.Update();
            }
        }


        return State.Running;
    }

    private async void HandlePlayerStay()
    {
        await Task.Delay(500);
        child.Update();
        // We are checking if player is still on top of enemy's head 
        if (child.state == State.Failure)
        {
            blackboard.moveSpeed *= 2;

            await Task.Delay(400);
            blackboard.moveSpeed *= -1;
            await Task.Delay(400);
            blackboard.moveSpeed /= 2;

            child.state = State.Running;
            _isWaiting = false;
        }
        else
        {
            _isWaiting = false;
        }
    }
}