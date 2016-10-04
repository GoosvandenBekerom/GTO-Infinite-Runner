using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CoinPoolScript : MonoBehaviour
    {
        public GameObject CoinPrefab;
        public Transform Player;

        private const int CoinPoolSize = 50;
        private Queue<Transform> _coins;

        private float _lastCoinYpos;
        private const float YScale = 0.0025f;

        private int _lastCoinZpos;

        private const int CoinStartPos = 30;
        private const int CoinsInRow = 20; // 10*2 for spacing between coins
        private int _currentXspawnPos;

        public ScoreManager ScoreManager { get; private set; }
        
        void Start()
        {
            ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

            _lastCoinYpos = 1;
            _coins = new Queue<Transform>(CoinPoolSize);
            _currentXspawnPos = 0;
            _lastCoinZpos = CoinStartPos;

            for (var i = CoinStartPos; i < CoinPoolSize + CoinStartPos; i++)
            {
                var coin = Instantiate(CoinPrefab, new Vector3(_currentXspawnPos, _lastCoinYpos, _lastCoinZpos), Quaternion.identity) as GameObject;
                coin.transform.parent = transform;
                _coins.Enqueue(coin.transform);
                _lastCoinYpos -= YScale;
                 _lastCoinZpos += 2;

                if (i == 10) continue; //so the first coin isn't off

                RandomizeCoinPosition();
            }
        }

        public void RandomizeCoinPosition()
        {
            if ((_lastCoinZpos%CoinsInRow) != 0) return;

            _lastCoinZpos += 4; // to give player time to change rows
            _currentXspawnPos = Random.Range(-2, 3);
        }

        public void ReplaceCoin()
        {
            var coinPos = _coins.Peek().position;
            if (coinPos.y < 15 && coinPos.z > (Player.position.z - 3)) return;
            //coin has been missed or picked up.

            var coin = _coins.Dequeue();
            _lastCoinYpos -= YScale;
            _lastCoinZpos += 2;
            coin.position = new Vector3(_currentXspawnPos, _lastCoinYpos, _lastCoinZpos);
            _coins.Enqueue(coin);

            RandomizeCoinPosition();
        }
    }
}
