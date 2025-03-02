using System;
using TheKiwiCoder;
using UnityEngine;

namespace Rimaethon.Runtime.AI.Actions
{
    [Serializable]
    public class Patrol : DecoratorNode
    {
        protected override void OnStart()
        {
            blackboard.moveSpeed = 3;
            context.Rigidbody2D.linearVelocity = new Vector2(blackboard.moveSpeed, 0);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            MoveEnemy();
            return State.Running;
        }

        private void MoveEnemy()
        {
            context.SpriteRenderer.flipX = Mathf.Sign(blackboard.moveSpeed) != -1;
        }
    }
}