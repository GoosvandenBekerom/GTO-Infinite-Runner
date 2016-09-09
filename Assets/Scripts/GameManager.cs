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
        
        // queue to get rid of old platforms
        public Queue<GameObject> Platforms { get; private set; } 

        void Awake()
        {
            _instance = this;
            Platforms = new Queue<GameObject>();
        }

        public void RestartGame()
        {
            
        }
    }
}
