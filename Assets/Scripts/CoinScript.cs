using UnityEngine;

namespace Assets.Scripts
{
    public class CoinScript : MonoBehaviour
    {
        public int Value;

        private CoinPoolScript _poolScript;

        void Start()
        {
            if (Value == 0) Value = 1; // default value

            _poolScript = GetComponentInParent<CoinPoolScript>();
        }

        void Update()
        {
            if (transform.position.z < (_poolScript.Player.position.z - 3))
            {
                _poolScript.ReplaceCoin();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _poolScript.ScoreManager.IncreaseCoins(Value);
                transform.Translate(Vector3.up*20);
                _poolScript.ReplaceCoin();
            }
        }
    }
}
