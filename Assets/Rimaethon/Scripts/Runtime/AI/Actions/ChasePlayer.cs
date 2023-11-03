using System;
using TheKiwiCoder;
using UnityEngine;

namespace Rimaethon.Scripts.AI.Runtime.Actions
{
    [Serializable]
    public class ChasePlayer : ActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Mathf.Abs(context.PlayerData.GetPlayerPosition().x - context.Transform.position.x) < 1.5f)
            {
                blackboard.moveSpeed = 0;
                context.Rigidbody2D.velocity = new Vector2(blackboard.moveSpeed, context.Rigidbody2D.velocity.y);
                if (Mathf.Abs(context.PlayerData.GetPlayerPosition().y - context.Transform.position.y) < 0.3f)
                    MoveToPlayer();
            }
            else
            {
                MoveToPlayer();
                return State.Running;
            }


            return State.Running;
        }

        private State MoveToPlayer()
        {
            switch (context.PlayerData.GetPlayerPosition().x - context.Transform.position.x)
            {
                case > 1f:
                    blackboard.moveSpeed = 3;
                    context.Rigidbody2D.velocity = new Vector2(blackboard.moveSpeed, context.Rigidbody2D.velocity.y);
                    return State.Running;
                case < -1f:
                    blackboard.moveSpeed = -3;
                    context.Rigidbody2D.velocity = new Vector2(blackboard.moveSpeed, context.Rigidbody2D.velocity.y);
                    return State.Running;
                default:
                    return State.Failure;
            }
        }
    }
}