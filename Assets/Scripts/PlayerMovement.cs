using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _velocityZ;
        private float _velocityY;
        private float _defaultYpos;
        private float _gravity;
        private bool _onGround;

        void Awake()
        {
            _velocityZ = GameManager.Instance.MovementSpeed;
            _velocityY = 0.0f;
            _defaultYpos = transform.position.y;
            _gravity = 0.5f;
            _onGround = true;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            _velocityY -= _gravity;
            transform.Translate(0, _velocityY, _velocityZ);

            if (transform.position.y < _defaultYpos)
            {
                var pos = transform.position;
                transform.position = new Vector3(pos.x, _defaultYpos, pos.x);
                _velocityY = 0.0f;
                _onGround = true;
            }
        }

        private void Jump()
        {
            if (!_onGround) return;

            _velocityY = 3;
            _onGround = false;
        }
    }
}
