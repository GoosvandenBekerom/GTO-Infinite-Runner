using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PathGeneration : MonoBehaviour
    {
        /// <summary>
        /// Different prefabs for all platforms, index 0 should be empty platform without obstacles
        /// </summary>
        public GameObject[] PlatformPrefabs;
        /// <summary>
        /// Ammount of platforms spawned in front of the player
        /// </summary>
        public int RenderDistance;

        private float _platformLength;
        private Transform _lastPlatform;

        private List<GameObject> _pooledPlatforms;
        private int _poolSize;
        private const int FreePlatformsAtStart = 4;

        private bool _doneCreatingWorld;

        void Start()
        {
            _platformLength = PlatformPrefabs[0].transform.localScale.z;
            _poolSize = PlatformPrefabs.Length * 3;
            _pooledPlatforms = new List<GameObject>();

            
            for (var i = 0; i < _poolSize; i++)
            {
                // Fill the object pool with platforms
                var platform = 
                    Instantiate(PlatformPrefabs[i%PlatformPrefabs.Length],
                        new Vector3(-100, -100, -100), Quaternion.identity) as GameObject;

                platform.transform.parent = transform;
                _pooledPlatforms.Add(platform);
            }

            SpawnPlatform(true, true, -_platformLength); // so there is always at least one platform behind camera
            for (var i = 0; i < FreePlatformsAtStart; i++)
            {
                // spawn empty platforms at the start
                SpawnPlatform(true, true, i*_platformLength);
            }

            if (RenderDistance > _poolSize) RenderDistance = _poolSize;

            for (var i = 0; i < RenderDistance; i++)
            {
                // allocate first platforms
                SpawnPlatform(false, false);
            }

            _doneCreatingWorld = true;
        }

        /// <summary>
        /// Spawn a new platform at the end of the world.
        /// </summary>
        /// <param name="instantiate"> Create new platform or get from pool </param>
        /// <param name="emptyPlatform"> should the instantiated platform be empty </param>
        /// <param name="zPos"> z position to spawn the new platform at </param>
        /// <returns> The newly instantiated platform </returns>
        public void SpawnPlatform(bool instantiate, bool emptyPlatform, float zPos = -1)
        {
            var pos = (_lastPlatform == null) 
                ? PlatformPrefabs[0].transform.position : _lastPlatform.transform.position;
            var zPosition = (zPos == -1) 
                ? _lastPlatform.transform.position.z + _platformLength : zPos;

            // -0.02f is to compensate for rigidbody collision with floor failure
            var newPos = new Vector3(pos.x, pos.y - 0.02f, zPosition);

            GameObject platform;

            if (instantiate)
            {
                platform = Instantiate(
                    PlatformPrefabs[(emptyPlatform) ? 0 : Random.Range(0, PlatformPrefabs.Length)],
                    newPos, Quaternion.identity) as GameObject;

                platform.transform.parent = transform;
            }
            else
            {
                platform = _pooledPlatforms[Random.Range(0, _pooledPlatforms.Count)];
                platform.transform.position = newPos;
                _pooledPlatforms.Remove(platform);
            }

            var world = GameManager.Instance.ActivePlatforms;

            world.Enqueue(platform);
            if (_doneCreatingWorld) _pooledPlatforms.Add(world.Dequeue());

            _lastPlatform = platform.transform;
        }
        
        /*
        Old way of spawning platforms without pooling

        /// <summary>
        /// Spawn a new platform at the end of the world.
        /// </summary>
        /// <param name="removeOld"> remove the first platform in the world </param>
        /// <param name="zPos"> z position to spawn the new platform at </param>
        /// <returns> The newly instantiated platform </returns>
        public GameObject SpawnPlatform(bool removeOld, float zPos = -1)
        {
            var world = GameManager.Instance.ActivePlatforms;
            var platformNumber = (world.Count < 4) ? 0 : Random.Range(0, PlatformPrefabs.Length);

            var pos = (_lastPlatform == null)
                ? PlatformPrefabs[0].transform.position : _lastPlatform.transform.position;
            var zPosition = (zPos == -1)
                ? _lastPlatform.transform.position.z + _platformLength : zPos;

            // -0.02f is to compensate for rigidbody collision with floor failure
            var platform = Instantiate(
                PlatformPrefabs[platformNumber],
                new Vector3(pos.x, pos.y - 0.02f, zPosition),
                Quaternion.identity) as GameObject;

            platform.transform.parent = transform;


            world.Enqueue(platform);

            if (removeOld) Destroy(world.Dequeue());

            _lastPlatform = platform.transform;

            return platform;
        }*/
    }
}
