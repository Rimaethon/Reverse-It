using Rimaethon.Scripts.Core.Enums;
using UnityEngine;

namespace Rimaethon.Scripts.Enemy
{
    public class HorizontalPatrolMovement : MonoBehaviour
    {
        public WalkDirections WalkDirection { get; set; }

        public Vector3 GetMovement(float moveSpeed)
        {
            switch (WalkDirection)
            {
                case WalkDirections.Left:
                    return Vector3.left * (moveSpeed * Time.fixedDeltaTime);
                case WalkDirections.Right:
                    return Vector3.right * (moveSpeed * Time.fixedDeltaTime);
                default:
                    return Vector3.zero;
            }
        }
    }
}