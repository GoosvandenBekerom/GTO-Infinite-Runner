using UnityEngine;

namespace Assets.Scripts
{
    public class RotationScript : MonoBehaviour {
        
        void Update () {
            transform.Rotate(Vector3.up * Time.deltaTime);
        }
    }
}
