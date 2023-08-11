using UnityEngine;

namespace Rimaethon.Scripts.Core.Interfaces
{
    public interface IMovementStrategy
    {
        Vector3 GetMovement(float moveSpeed);
    }
}