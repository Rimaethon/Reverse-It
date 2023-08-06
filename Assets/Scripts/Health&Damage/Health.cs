using Player;
using UnityEngine;
using Utility;

namespace Health_Damage
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Health : MonoBehaviour
    {
        [SerializeField] public int teamId;
        [SerializeField] private int defaultHealth = 2;
        [SerializeField] public int maximumHealth = 3;
        [SerializeField] public int currentHealth = 2;
        [SerializeField] private float invincibilityTime = 3f;
        [SerializeField] private bool useLives;
        [SerializeField] public int currentLives = 3;
        [SerializeField] private int maximumLives = 5;
        [SerializeField] private float respawnWaitTime = 3f;
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private bool isInvincible;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private GameObject hitEffect;
        private PlayerController _pScript;
        private Vector3 _respawnPosition;
        private float _respawnTime;
        private float _timeToBecomeDamagableAgain;


        private void Start()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            SetRespawnPoint(transform.position);
            _pScript = GetComponent<PlayerController>();
        }


        public void Update()
        {
            InvincibilityCheck();
            RespawnCheck();
        }


        private void RespawnCheck()
        {
            if (respawnWaitTime == 0 || currentHealth > 0 || currentLives <= 0) return;
            if (Time.time >= _respawnTime) Respawn();
        }


        private void InvincibilityCheck()
        {
            if (_timeToBecomeDamagableAgain <= Time.time) isInvincible = false;
        }

        public void SetRespawnPoint(Vector3 newRespawnPosition)
        {
            _respawnPosition = newRespawnPosition;
        }


        private void Respawn()
        {
            transform.position = _respawnPosition;
            currentHealth = defaultHealth;
            GameManager.UpdateUIElements();
        }

        public void TakeDamage(int damageAmount)
        {
            if (isInvincible || currentHealth <= 0)
            {
                return;
            }

            if (hitEffect != null)
            {
                var transform1 = transform;
                Instantiate(hitEffect, transform1.position, transform1.rotation, null);
            }

            _timeToBecomeDamagableAgain = Time.time + invincibilityTime;
            isInvincible = true;
            currentHealth -= damageAmount;
            CheckDeath();
            GameManager.UpdateUIElements();
        }

        public void ReceiveHealing(int healingAmount)
        {
            currentHealth += healingAmount;
            if (currentHealth > maximumHealth) currentHealth = maximumHealth;
            CheckDeath();
            GameManager.UpdateUIElements();
        }

        public void AddLives(int bonusLives)
        {
            if (!useLives) return;
            currentLives += bonusLives;
            if (currentLives > maximumLives) currentLives = maximumLives;
            GameManager.UpdateUIElements();
        }


        private void CheckDeath()
        {
            if (currentHealth <= 0) Die();
        }


        private void Die()
        {
            if (deathEffect != null)
            {
                var transform1 = transform;
                Instantiate(deathEffect, transform1.position, transform1.rotation, null);
            }

            if (useLives)
            {
                currentLives -= 1;
                if (currentLives > 0)
                {
                    if (playerRigidbody.gravityScale < 0)
                    {
                        StopCoroutine(_pScript.GravitationOn());
                        playerRigidbody.gravityScale = 2;
                        transform.Rotate(new Vector3(0, 0, -180));
                    }

                    if (respawnWaitTime == 0)
                        Respawn();
                    else
                        _respawnTime = Time.time + respawnWaitTime;
                }
                else
                {
                    if (respawnWaitTime != 0)
                        _respawnTime = Time.time + respawnWaitTime;
                    else
                        Destroy(gameObject);
                    GameOver();
                }
            }
            else
            {
                GameOver();
                Destroy(gameObject);
            }

            GameManager.UpdateUIElements();
        }


        private void GameOver()
        {
            if (GameManager.Instance != null && gameObject.CompareTag("Player")) GameManager.Instance.GameOver();
        }
    }
}