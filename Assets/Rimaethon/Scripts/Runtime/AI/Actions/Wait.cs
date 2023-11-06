using System;
using UnityEngine;

namespace TheKiwiCoder
{
    [Serializable]
    public class Wait : ActionNode
    {
        [Tooltip("Amount of time to wait before returning success")]
        public float duration = 1;

        private float startTime;

        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            var timeRemaining = Time.time - startTime;
            if (timeRemaining > duration) return State.Success;
            return State.Running;
        }

        
    }
}