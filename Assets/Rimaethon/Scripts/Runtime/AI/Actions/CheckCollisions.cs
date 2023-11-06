using System;
using TheKiwiCoder;

namespace Rimaethon.AI
{
    [Serializable]
    public class CheckCollisions : ActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (context.WallColliderCheck.IsCollidedWithWall && !context.GroundCheck.IsGrounded) return State.Failure;
            TurnAround();
            return State.Success;
        }


        private void TurnAround()
        {
            blackboard.moveSpeed = -blackboard.moveSpeed;
        }
    }
}