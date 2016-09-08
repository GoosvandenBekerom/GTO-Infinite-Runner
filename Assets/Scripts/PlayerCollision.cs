using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCollision : MonoBehaviour {
        
        void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Floor"))
            {
                Debug.Log("collision detected");
            }
        }
    }
}
