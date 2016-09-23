using UnityEngine;

namespace Assets.Scripts
{
    public class CoinScript : MonoBehaviour
    {
        public int Value;

        void Start()
        {
            if (Value == 0) Value = 1;
        }

        void Update()
        {
            if (transform.position.z < (CoinPoolScript.Instance.Player.position.z - 2))
            {
                CoinPoolScript.Instance.ReplaceCoin();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ScoreManager.Instance.IncreaseCoins(Value);
                transform.Translate(Vector3.up*20);
                CoinPoolScript.Instance.ReplaceCoin();
            }
        }
    }
}
