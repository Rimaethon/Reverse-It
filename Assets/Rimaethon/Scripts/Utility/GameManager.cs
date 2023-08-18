using Rimaethon.Scripts.Health_Damage;
using Rimaethon.Scripts.Keys_Doors;
using Rimaethon.Scripts.UI;
using UnityEngine;

namespace Rimaethon.Scripts.Utility
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public UIManager uiManager;

         public GameObject player;

        [SerializeField]
        private int gameManagerScore;

        public int highScore;

        
        public bool gameIsWinnable = true;

        
        public int gameVictoryPageIndex;

        
        public GameObject victoryEffect;

        
        public int gameOverPageIndex;

        
        public GameObject gameOverEffect;

        // Whether or not the game is over
        [HideInInspector] public bool gameIsOver;

       
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
                var playerHealth = player.GetComponent<BaseHealth>();

                // Set lives accordingly


                // Set health accordingly

            }

            KeyRing.ClearKeyRing();
        }


        private void SetGamePlayerPrefs()
        {
            if (player != null)
            {
                var playerHealth = player.GetComponent<BaseHealth>();
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