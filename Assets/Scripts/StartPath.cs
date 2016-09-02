using UnityEngine;

namespace Assets.Scripts
{
    public class StartPath : MonoBehaviour
    {
        public GameObject Platform;

        void Start()
        {
            var platform = Instantiate(Platform, new Vector3(0, -0.5f, 5), Quaternion.identity) as GameObject;
            GameManager.Instance.Platforms.Enqueue(platform);
        }
    }
}
