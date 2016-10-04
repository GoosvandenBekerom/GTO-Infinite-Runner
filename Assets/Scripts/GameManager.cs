using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        // Singleton instance
        private static GameManager _instance;
        public static GameManager Instance
        {
            get { return _instance ?? new GameManager(); }
        }

        // Player Properties
        private const float BaseMovementSpeed = 5;

        public float MovementSpeed
        {
            get { return BaseMovementSpeed + DifficultyLevel; }
        }

        public int DifficultyLevel { get; set; }
        
        // queue to get rid of old platforms, boolean is true when platform is entered
        public Queue<GameObject> ActivePlatforms { get; private set; }
        
        public bool GameRunning { get; set; }

        /// <summary>
        /// Did de player run into an obstacle?
        /// </summary>
        public bool HadWarning { get; set; }

        private float _warningTime;
        private const float ExtraWarningTime = 15;

        /// <summary>
        /// Is the game over?
        /// </summary>
        public bool GameOver { get; set; }

        void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }

            SceneManager.LoadScene("MainMenu");

            ActivePlatforms = new Queue<GameObject>();

            HadWarning = false;
            GameOver = false;

            DifficultyLevel = 2;
        }

        void LateUpdate()
        {
            if (!GameRunning || GameOver) return;

            if (Mathf.Abs(Time.time - _warningTime) > ExtraWarningTime && HadWarning)
            {
                // Player gets extra warning / warning back
                HadWarning = false;
                Debug.Log("Got Extra warning/life at :" + Time.time);
                _warningTime = 0f;
            }
        }

        public void CrashIntoObject()
        {
            if (HadWarning)
            {
                GameOver = true;

                var panel = GameObject.Find("GameOver Canvas");
                var gameOverScreen = panel.GetComponent<GameOverScreenScript>();
                gameOverScreen.DisplayGameOver();

                Debug.Log("Game Over!");
                return;
            }

            Debug.Log("Hit Obstacle");
            HadWarning = true;
            _warningTime = Time.time;
            DifficultyLevel -= 2;
        }

        /// <summary>
        /// Set all default values for the game
        /// </summary>
        /// <param name="reset">Are you restarting the game from an old game in the same scene?</param>
        public void RestartGame(bool reset)
        {
            HadWarning = false;
            GameOver = false;
            _warningTime = 0f;
            ActivePlatforms = new Queue<GameObject>();
            Time.timeScale = 1;

            if (reset) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
