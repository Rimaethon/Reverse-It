using System;
using TheKiwiCoder;
using UnityEngine;

namespace Rimaethon.AI
{
    [Serializable]
    public class SearchPlayer : ActionNode
    {
        private float deltaX;
        private float deltaY;
        private float xThreshold = 7.0f;
        private float yTreshold = 2f;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            deltaX = context.PlayerData.GetPlayerPosition().x - context.Transform.position.x;
            deltaY = context.PlayerData.GetPlayerPosition().y - context.Transform.position.y;


            if (Mathf.Abs(deltaX) < xThreshold && Mathf.Abs(deltaY) < yTreshold)
            {
                blackboard.isPlayerInSight = true;
                return State.Success;
            }

            blackboard.isPlayerInSight = false;
            return State.Running;
        }
    }
}