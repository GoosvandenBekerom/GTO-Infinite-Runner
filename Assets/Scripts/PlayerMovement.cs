using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        void Update()
        {
            var movement = GameManager.Instance.MovementSpeed;
            transform.Translate(new Vector3(0, 0, movement));
        }

    }
}
