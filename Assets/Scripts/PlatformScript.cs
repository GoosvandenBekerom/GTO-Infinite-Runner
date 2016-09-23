using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformScript : MonoBehaviour {

        public bool IsEnterred { get; set; }

        void OnTriggerExit(Collider other)
        {
            if (IsEnterred) return;

            if (other.CompareTag("Player"))
            {
                IsEnterred = true;
                transform.parent.GetComponentInParent<PathGeneration>().SpawnPlatform(IsEnterred);
            }
        }
    }
}
