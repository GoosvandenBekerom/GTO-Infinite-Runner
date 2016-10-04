using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        private float _distance;
        
        private const int MaxDifficultyLevel = 20;
        private int _scoreToNextLevel = 50;

        public int Distance
        {
            get { return (int) _distance; }
        }

        public Text DistanceText;
        public Text CoinsText;

        public int Coins { get; private set; }
        
        void Start ()
        {
            Coins = 0;
            SetCoinsText(Coins + "");
        }
	
        void Update () {
            // increase distance
            if (GameManager.Instance.GameOver) return;

            _distance += Time.deltaTime*GameManager.Instance.MovementSpeed;
            DistanceText.text = Distance + "";

            if (_distance >= _scoreToNextLevel)
            {
                LevelUp();
            }
        }

        public void IncreaseCoins(int ammount)
        {
            Coins += ammount;
            SetCoinsText(Coins + "");
        }

        private void SetCoinsText(string s)
        {
            CoinsText.text = s;
        }

        private void LevelUp()
        {
            if (GameManager.Instance.DifficultyLevel >= MaxDifficultyLevel) return;

            _scoreToNextLevel *= 2;
            GameManager.Instance.DifficultyLevel += 2;
        }
    }
}
