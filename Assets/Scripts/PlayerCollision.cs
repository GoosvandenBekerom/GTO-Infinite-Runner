using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCollision : MonoBehaviour {
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                Debug.Log("Hit Obstacle");
                GameManager.Instance.CrashIntoObject();
            }
        }
    }
}
