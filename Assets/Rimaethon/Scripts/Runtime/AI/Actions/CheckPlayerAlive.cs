using System;
using TheKiwiCoder;

namespace Rimaethon.Runtime.AI.Actions
{
    [Serializable]
    public class CheckPlayerAlive : DecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (!context.PlayerData.IsPlayerAlive())
                return State.Running;
            child.Update();

            return State.Running;
        }
    }
}