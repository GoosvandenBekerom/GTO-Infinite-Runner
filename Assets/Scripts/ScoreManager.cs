using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour {

        private static ScoreManager _instance;
        public static ScoreManager Instance
        {
            get { return _instance ?? new ScoreManager(); }
        }

        private float _distance;

        public bool Moving { get; set; }

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
            Moving = true;
        }
    
        void Start ()
        {
            SetCoinsText(Coins + "");
        }
	
        void Update () {
            // increase distance
            if (Moving)
            {
                _distance += Time.deltaTime*GameManager.Instance.MovementSpeed;
                DistanceText.text = Distance + "";
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
    }
}
