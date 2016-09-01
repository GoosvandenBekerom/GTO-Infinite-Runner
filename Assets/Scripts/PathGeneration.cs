using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PathGeneration : MonoBehaviour
    {
        public GameObject Platform;

        public int PlatformLength;

        void Start ()
        {
            StartCoroutine(SpawnAfterSeconds(GameManager.Instance.SpawnSpeed));
        }

        IEnumerator SpawnAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            var pos = transform.position;
            var platform = Instantiate(
                Platform, 
                new Vector3(pos.x, pos.y, pos.z + PlatformLength),
                Quaternion.identity) as GameObject;

            var queue = GameManager.Instance.Platforms;

            queue.Enqueue(platform);
            if (queue.Count > 8)
            {
                var oldPlatform = queue.Dequeue();
                Destroy(oldPlatform);
            }
        }

    }
}
