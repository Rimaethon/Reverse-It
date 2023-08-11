using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    public class HorizontalPatrolMovement : IMovementStrategy
    {
        public WalkDirections WalkDirection { get; set; }

        public Vector3 GetMovement(float moveSpeed)
        {
            switch (WalkDirection)
            {
                case WalkDirections.Left:
                    return Vector3.left * (moveSpeed * Time.deltaTime);
                case WalkDirections.Right:
                    return Vector3.right * (moveSpeed * Time.deltaTime);
                default:
                    return Vector3.zero;
            }
        }
    }
}