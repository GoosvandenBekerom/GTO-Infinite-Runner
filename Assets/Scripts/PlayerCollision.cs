using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCollision : MonoBehaviour {
        
        void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Floor"))
            {
                Debug.Log("Stopped moving, end game");
                ScoreManager.Instance.Moving = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlatformTrigger"))
            {
                other.GetComponentInParent<PlatformScript>().IsEnterred = true;
                other.transform.parent.GetComponentInParent<PathGeneration>().SpawnPlatform(true);
            }
        }
    }
}
