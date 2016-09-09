using UnityEngine;

namespace Assets.Scripts
{
    public class PathGeneration : MonoBehaviour
    {
        public GameObject Platform;
        public int RenderDistance;

        private float _platformLength;
        private Transform _firstPlatform;
        private Transform _lastPlatform;

        public Transform Player;

        void Start()
        {
            _platformLength = Platform.transform.localScale.z;

            for (var i = 0; i < RenderDistance; i++)
            {
                SpawnPlatform(false, i*_platformLength);
            }

            _firstPlatform = GameManager.Instance.Platforms.Peek().transform;
        }

        void FixedUpdate()
        {
            /*// move platforms (this gameobject is parent to all platforms)
            transform.Translate(0, 0, -GameManager.Instance.MovementSpeed);

            if (GameManager.Instance.Platforms.Peek().transform.position.z < -_platformLength)
            {
                SpawnPlatform(_lastPlatform.transform.position.z + _platformLength);

                var oldPlatform = GameManager.Instance.Platforms.Dequeue();
                Destroy(oldPlatform);
            }*/
            // TODO: Levels genereren voor de speler, object plaatsen waar speler mee botst om te weten
            // TODO: dat de speler op een nieuw platform is....
        }

        /// <summary>
        /// Spawn a new platform at the end of the world.
        /// </summary>
        /// <returns> The newly instantiated platform </returns>
        public GameObject SpawnPlatform(bool removeOld, float zPos = -1)
        {
            var pos = Platform.transform.position;
            var zPosition = (zPos == -1) 
                ? _lastPlatform.transform.position.z + _platformLength 
                : zPos;
            var platform = Instantiate(
                Platform,
                new Vector3(pos.x, pos.y, zPosition),
                Quaternion.identity) as GameObject;

            platform.transform.parent = transform;

            var world = GameManager.Instance.Platforms;
            world.Enqueue(platform);

            //if (removeOld) Destroy(world.Dequeue()); // TODO: pool different platforms

            _lastPlatform = platform.transform;

            return platform;
        }
    }
}
