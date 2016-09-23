using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PathGeneration : MonoBehaviour
    {
        public GameObject[] Platforms;
        public int RenderDistance;

        private float _platformLength;
        private Transform _lastPlatform;

        void Start()
        {
            _platformLength = Platforms[0].transform.localScale.z;

            SpawnPlatform(false, -_platformLength); // so there is always at least one platform behind camera

            for (var i = 0; i < RenderDistance; i++)
            {
                SpawnPlatform(false, i * _platformLength);
            }
            
        }
        
        /// <summary>
        /// Spawn a new platform at the end of the world.
        /// </summary>
        /// <param name="removeOld"> First platform in world queue will be removed when true </param>
        /// <param name="zPos"> z position to spawn the new platform at </param>
        /// <returns> The newly instantiated platform </returns>
        public GameObject SpawnPlatform(bool removeOld, float zPos = -1)
        {
            var world = GameManager.Instance.ActivePlatforms;
            var platformNumber = (world.Count < 4) ? 0 : Random.Range(0, Platforms.Length);

            var pos = (_lastPlatform == null) 
                ? Platforms[0].transform.position : _lastPlatform.transform.position;
            var zPosition = (zPos == -1) 
                ? _lastPlatform.transform.position.z + _platformLength : zPos;

            // -0.02f is to compensate for rigidbody collision with floor failure
            var platform = Instantiate(
                Platforms[platformNumber],
                new Vector3(pos.x, pos.y - 0.02f, zPosition),
                Quaternion.identity) as GameObject;

            platform.transform.parent = transform;

            
            world.Enqueue(platform);

            if (removeOld) Destroy(world.Dequeue()); // TODO: pool different platforms

            _lastPlatform = platform.transform;

            return platform;
        }
    }
}
