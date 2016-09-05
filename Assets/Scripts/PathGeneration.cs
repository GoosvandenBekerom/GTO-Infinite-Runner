using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts
{
    public class PathGeneration : MonoBehaviour
    {
        public GameObject Platform;
        public int RenderDistance;

        private float _platformLength;
        private Transform _lastPlatform;

        void Start()
        {
            _platformLength = Platform.transform.localScale.z;

            for (var i = 0; i < RenderDistance; i++)
            {
                SpawnPlatform(i*_platformLength);
            }
        }

        void Update()
        {
            // move platforms (this gameobject is parent to all platforms)
            transform.Translate(0, 0, -GameManager.Instance.MovementSpeed);

            if (GameManager.Instance.Platforms.Peek().transform.position.z < -_platformLength)
            {
                SpawnPlatform(_lastPlatform.transform.position.z + _platformLength);

                var oldPlatform = GameManager.Instance.Platforms.Dequeue();
                Destroy(oldPlatform);
            }
        }

        /// <summary>
        /// Spawn a new platform at the end of the world.
        /// </summary>
        /// <param name="zPosition"> Position on the z axis the platform should spawn at</param>
        /// <returns> The newly instantiated platform </returns>
        public GameObject SpawnPlatform(float zPosition)
        {
            var pos = Platform.transform.position;
            var platform = Instantiate(
                Platform,
                new Vector3(pos.x, pos.y, zPosition),
                Quaternion.identity) as GameObject;

            platform.transform.parent = transform;

            GameManager.Instance.Platforms.Enqueue(platform);
            _lastPlatform = platform.transform;

            return platform;
        }
    }
}
