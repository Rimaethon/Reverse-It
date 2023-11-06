using System;
using TheKiwiCoder;
using UnityEngine;

namespace Rimaethon.Runtime.AI.Actions
{
    [Serializable]
    public class IsPlayerInRange : CompositeNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (CheckPlayerInRange())
                children[0].Update();
            else
                children[1].Update();


            return State.Running;
        }

        private bool CheckPlayerInRange()
        {
            return Mathf.Abs(context.Transform.position.x - context.PlayerData.GetPlayerPosition().x) < 7.0f &&
                   Mathf.Abs(context.Transform.position.y - context.PlayerData.GetPlayerPosition().y) < 4.0f;
        }
    }
}