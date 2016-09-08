using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour {

        private ScoreManager _instance;
        public ScoreManager Instance
        {
            get { return _instance ?? new ScoreManager(); }
        }

        private float _distance;
        private const int MetersPerSecond = 10;

        public int Distance
        {
            get
            {
                return (int) _distance;
            }
        }

        public Text DistanceText;
        public Text CoinsText;

        public int Coins { get; private set; }

        void Awake()
        {
            _instance = this;
            Coins = 0;
        }
    
        void Start ()
        {
            SetCoinsText(Coins + "");
        }
	
        void Update () {
            // increase distance
            _distance += Time.deltaTime * GameManager.Instance.MovementSpeed * MetersPerSecond;
            DistanceText.text = Distance + "";
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
    }
}
