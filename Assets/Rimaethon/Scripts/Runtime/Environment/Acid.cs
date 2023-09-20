using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Core.Interfaces;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.Environment
{
    // Tries to melt every object that enters it
    public class Acid : MonoBehaviour
    {
        [SerializeField] private GameObject acidMeltingSmoke;
        [SerializeField] private bool isGravityAcid;

        private readonly int _damageAmount = 30;
        private float _contactNormal;
        private bool _isObjectInAcid;
        private Vector2 _meltingPoint;
        private Vector3 _meltingRotation;

        private void Awake()
        {
            _meltingRotation = Vector3.zero;

            _meltingRotation.z = isGravityAcid ? 180 : 0;
        }


        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnPlayerDead, CreateAcidSmoke);
            EventManager.Instance.AddHandler(GameEvents.OnEnemyDead, CreateAcidSmoke);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerDead, CreateAcidSmoke);
            EventManager.Instance.RemoveHandler(GameEvents.OnEnemyDead, CreateAcidSmoke);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _isObjectInAcid = true;
            _meltingPoint = other.gameObject.transform.position;
            EventManager.Instance.Broadcast(GameEvents.OnPlayerMelted);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isObjectInAcid = false;
            EventManager.Instance.Broadcast(GameEvents.OnPlayerMelted);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _meltingPoint.x = other.gameObject.transform.position.x;
            if (other.gameObject.TryGetComponent(out IDamageAble damageable)) damageable.TakeDamage(_damageAmount);
        }

        private void CreateAcidSmoke()
        {
            if (!_isObjectInAcid) return;

            Instantiate(acidMeltingSmoke, _meltingPoint, Quaternion.Euler(_meltingRotation));
            _isObjectInAcid = false;
        }
    }
}