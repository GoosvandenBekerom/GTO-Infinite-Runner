using System.Collections.Generic;
using UnityEngine;

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
        [Header("Player Properties")]
        [Range(5, 50)]
        public float MovementSpeed;
        
        // queue to get rid of old platforms, boolean is true when platform is entered
        public Queue<GameObject> ActivePlatforms { get; private set; }
        
        /// <summary>
        /// Did de player run into an obstacle once?
        /// TODO: reset after certain ammount of time
        /// </summary>
        public bool HadWarning { get; set; }

        /// <summary>
        /// Is the game over?
        /// </summary>
        public bool GameOver { get; set; }

        void Awake()
        {
            _instance = this;
            ActivePlatforms = new Queue<GameObject>();

            HadWarning = false;
            GameOver = false;
        }

        void LateUpdate()
        {
            if (GameOver) return;

            MovementSpeed += 0.005f;
        }

        public void CrashIntoObject()
        {
            if (HadWarning)
            {
                GameOver = true;
                Debug.Log("Game Over!");
                return;
            }
            
            HadWarning = true;
            MovementSpeed -= 2;
        }

        public void RestartGame()
        {
            HadWarning = false;
            GameOver = false;

            // TODO: fix restart functionality
        }
    }
}
