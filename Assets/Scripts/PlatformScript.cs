using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformScript : MonoBehaviour {
        
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                transform.parent.GetComponentInParent<PathGeneration>().SpawnPlatform(false, false);
            }
        }
    }
}
