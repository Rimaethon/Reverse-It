using Health_Damage;
using Keys_Doors;
using UI;
using UnityEngine;

namespace Utility
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("References:")] [Tooltip("The UIManager component which manages the current scene's UI")]
        public UIManager uiManager;

        [Tooltip("The player GameObject")] public GameObject player;

        [Header("Scores")] [Tooltip("The player's score")] [SerializeField]
        private int gameManagerScore;

        [Tooltip("The highest score acheived on this device")]
        public int highScore;

        [Header("Game Progress / Victory Settings")] [Tooltip("Whether the game is winnable or not \nDefault: true")]
        public bool gameIsWinnable = true;

        [Tooltip("Page index in the UIManager to go to on winning the game")]
        public int gameVictoryPageIndex;

        [Tooltip("The effect to create upon winning the game")]
        public GameObject victoryEffect;

        [Header("Game Over Settings:")] [Tooltip("The index in the UI manager of the game over page")]
        public int gameOverPageIndex;

        [Tooltip("The game over effect to create when the game is lost")]
        public GameObject gameOverEffect;

        // Whether or not the game is over
        [HideInInspector] public bool gameIsOver;

        // Static getter/setter for player score (for convenience)
        public static int Score
        {
            get => Instance.gameManagerScore;
            private set => Instance.gameManagerScore = value;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }


        private void Start()
        {
            if (PlayerPrefs.HasKey("highscore")) highScore = PlayerPrefs.GetInt("highscore");
            if (PlayerPrefs.HasKey("score")) Score = PlayerPrefs.GetInt("score");
            InitializeGamePlayerPrefs();
        }


        private void OnApplicationQuit()
        {
            SaveHighScore();
            ResetScore();
        }


        private void InitializeGamePlayerPrefs()
        {
            if (player != null)
            {
                var playerHealth = player.GetComponent<Health>();

                // Set lives accordingly
                if (PlayerPrefs.GetInt("lives") == 0) PlayerPrefs.SetInt("lives", playerHealth.currentLives);

                playerHealth.currentLives = PlayerPrefs.GetInt("lives");

                // Set health accordingly
                if (PlayerPrefs.GetInt("health") == 0) PlayerPrefs.SetInt("health", playerHealth.currentHealth);

                playerHealth.currentHealth = PlayerPrefs.GetInt("health");
            }

            KeyRing.ClearKeyRing();
        }


        private void SetGamePlayerPrefs()
        {
            if (player != null)
            {
                var playerHealth = player.GetComponent<Health>();
                PlayerPrefs.SetInt("lives", playerHealth.currentLives);
                PlayerPrefs.SetInt("health", playerHealth.currentHealth);
            }
        }

        public static void UpdateUIElements()
        {
            if (Instance != null && Instance.uiManager != null) Instance.uiManager.UpdateUI();
        }


        public void LevelCleared()
        {
            PlayerPrefs.SetInt("score", Score);
            SetGamePlayerPrefs();
            if (uiManager != null)
            {
                player.SetActive(false);
                uiManager.allowPause = false;
                uiManager.GoToPage(gameVictoryPageIndex);
                if (victoryEffect != null)
                {
                    var transform1 = transform;
                    Instantiate(victoryEffect, transform1.position, transform1.rotation, null);
                }
            }
        }


        public void GameOver()
        {
            gameIsOver = true;
            if (gameOverEffect != null)
            {
                var transform1 = transform;
                Instantiate(gameOverEffect, transform1.position, transform1.rotation, null);
            }

            if (uiManager == null) return;
            uiManager.allowPause = false;
            uiManager.GoToPage(gameOverPageIndex);
        }


        public static void AddScore(int scoreAmount)
        {
            Score += scoreAmount;
            if (Score > Instance.highScore) SaveHighScore();
            UpdateUIElements();
        }


        public static void ResetScore()
        {
            PlayerPrefs.SetInt("score", 0);
            Score = 0;
        }

        public static void ResetGamePlayerPrefs()
        {
            PlayerPrefs.SetInt("score", 0);
            Score = 0;
            PlayerPrefs.SetInt("lives", 0);
            PlayerPrefs.SetInt("health", 0);
        }


        private static void SaveHighScore()
        {
            if (Score > Instance.highScore)
            {
                PlayerPrefs.SetInt("highscore", Score);
                Instance.highScore = Score;
            }

            UpdateUIElements();
        }


        public static void ResetHighScore()
        {
            PlayerPrefs.SetInt("highscore", 0);
            if (Instance != null) Instance.highScore = 0;
            UpdateUIElements();
        }
    }
}