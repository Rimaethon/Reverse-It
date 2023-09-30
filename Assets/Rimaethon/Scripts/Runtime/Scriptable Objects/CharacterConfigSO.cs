using UnityEngine;

namespace Rimaethon.Runtime.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "NewCharacterHealthConfig", menuName = "Character/HealthConfig")]
    public class CharacterConfigSO : ScriptableObject
    {
        [Range(1, 200)] public int maxHealth = 100;

        [Range(0.0f, 10.0f)] public float invincibilityDuration = 2.0f;

        [Range(0, 3)] public int teamId;

        [Range(0, 30)] public int damageAmount;
    }
}