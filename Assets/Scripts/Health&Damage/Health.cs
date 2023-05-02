using Player;
using UnityEngine;
using Utility;

namespace Health_Damage
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Health : MonoBehaviour
    {
        [Header("Team Settings")]
        [Tooltip("The team associated with this damage")]
        public int teamId;

        [Header("Health Settings")]
        [Tooltip("The default health value")]
        public int defaultHealth = 2;
        [Tooltip("The maximum health value")]
        public int maximumHealth = 3;
        [Tooltip("The current in game health value")]
        public int currentHealth = 2;
        [Tooltip("Invulnerability duration, in seconds, after taking damage")]
        public float invincibilityTime = 3f;

        [Header("Lives settings")]
        [Tooltip("Whether or not to use lives")]
        public bool useLives;
        [Tooltip("Current number of lives this health has")]
        public int currentLives = 3;
        [Tooltip("The maximum number of lives this health has")]
        public int maximumLives = 5;
        [Tooltip("The amount of time to wait before respawning")]
        public float respawnWaitTime = 3f;
        [SerializeField] Rigidbody2D playerRigidbody;
        PlayerController _pScript;


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

        private float _respawnTime;
    
   
        private void RespawnCheck()
        {
            if (respawnWaitTime == 0 || currentHealth > 0 || currentLives <= 0) return;
            if (Time.time >= _respawnTime)
            {
                Respawn();
            }
        }

        // The specific game time when the health can be damaged again
        private float _timeToBecomeDamagableAgain;
        // Whether or not the health is invincible
        public bool isInvincible;

 
        private void InvincibilityCheck()
        {
            if (_timeToBecomeDamagableAgain <= Time.time)
            {
                isInvincible = false;
            }
        }

        private Vector3 _respawnPosition;

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
            else
            {
                if (hitEffect != null)
                {
                    var transform1 = transform;
                    Instantiate(hitEffect, transform1.position, transform1.rotation, null);
                }
                _timeToBecomeDamagableAgain = Time.time + invincibilityTime;
                isInvincible = true;
                currentHealth -= damageAmount;
                CheckDeath();
            }
            GameManager.UpdateUIElements();
        }

        public void ReceiveHealing(int healingAmount)
        {
            currentHealth += healingAmount;
            if (currentHealth > maximumHealth)
            {
                currentHealth = maximumHealth;
            }
            CheckDeath();
            GameManager.UpdateUIElements();
        }

        public void AddLives(int bonusLives)
        {
            if (!useLives) return;
            currentLives += bonusLives;
            if (currentLives > maximumLives)
            {
                currentLives = maximumLives;
            }
            GameManager.UpdateUIElements();
        }

        [Header("Effects & Polish")]
        [Tooltip("The effect to create when this health dies")]
        public GameObject deathEffect;
        [Tooltip("The effect to create when this health is damaged (but does not die)")]
        public GameObject hitEffect;


        private void CheckDeath()
        {
            if (currentHealth <= 0)
            {
                Die();
            }
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
                    {
                        Respawn();
                       
                    }
                    else
                    {
                        _respawnTime = Time.time + respawnWaitTime;
                    } 
                }
                else
                {
                    if (respawnWaitTime != 0)
                    {
                        _respawnTime = Time.time + respawnWaitTime;
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                    GameOver();
                }
            
            }
            else
            {
                GameOver();
                Destroy(this.gameObject);
            }
            GameManager.UpdateUIElements();
        }


        private void GameOver()
        {
            if (GameManager.Instance != null && gameObject.CompareTag("Player"))
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
