using System;

namespace TheKiwiCoder
{
    [Serializable]
    public class Blackboard
    {
        public bool isCollidedWithWall;
        public bool isGrounded;
        public float moveSpeed;
        public float timer;
        public bool isPlayerAlive;
        public bool isPlayerInSight;
    }
}