using System;
using TheKiwiCoder;

namespace Rimaethon.AI
{
    [Serializable]
    public class EnemyMoveAction : ActionNode
    {
        public float acceleration = 40.0f;
        public float speed = 5;
        public float stoppingDistance = 0.1f;
        public float tolerance = 1.0f;
        public bool updateRotation = true;


        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Running;
        }
    }
}