using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _velocityY;
        private float _defaultYpos;
        private float _gravity;
        private bool _onGround;

        private const int Lanes = 5;

        // touch properties
        private const int MinSwipeDist = 10;
        private Vector2 _startPos;

        private void Awake()
        {
            _velocityY = 0.0f;
            _defaultYpos = transform.position.y;
            _gravity = 0.5f;
            _onGround = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Slide();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }

            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startPos = touch.position;
                        break;
                    case TouchPhase.Ended:
                        var swipeDistVertical =
                            (new Vector3(0, touch.position.y, 0) - new Vector3(0, _startPos.y, 0)).magnitude;

                        if (swipeDistVertical > MinSwipeDist)
                        {
                            var swipeValue = Mathf.Sign(touch.position.y - _startPos.y);

                            if (swipeValue > 0)
                            {
                                Jump();
                            }
                            else if (swipeValue < 0)
                            {
                                Slide();
                            }
                        }

                        var swipeDistHorizontal =
                            (new Vector3(touch.position.x, 0, 0) - new Vector3(_startPos.x, 0, 0)).magnitude;
                        if (swipeDistHorizontal > MinSwipeDist)
                        {
                            var swipeValue = Mathf.Sign(touch.position.x - _startPos.x);
                            if (swipeValue > 0)
                            {
                                MoveRight();
                            }
                            else if (swipeValue < 0)
                            {
                                MoveLeft();
                            }
                        }
                        break;
                }
            }

            _velocityY -= _gravity;

            transform.Translate(0, _velocityY, 0);

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

            _velocityY = 2;
            _onGround = false;
        }

        private void Slide()
        {
            Debug.Log("Slide");
        }

        private void MoveLeft()
        {
            if (transform.position.x > -2)
            {
                transform.Translate(Vector3.left);
            }
        }

        private void MoveRight()
        {
            if (transform.position.x < 2)
            {
                transform.Translate(Vector3.right);
            }
        }
    }
}
