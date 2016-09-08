using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _velocityY;
        private float _defaultYpos;
        private float _gravity;
        private bool _onGround;
        private bool _sliding;

        private bool _applyGravity;

        // touch properties
        private const int MinSwipeDist = 10;
        private Vector2 _startPos;

        private void Awake()
        {
            _velocityY = 0.0f;
            _defaultYpos = transform.position.y;
            _gravity = 0.25f;
            _onGround = true;
            _applyGravity = false;
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

            _applyGravity = !_applyGravity; // to slow down gravity
            if (!_onGround && _applyGravity)
            {
                _velocityY -= _gravity;
                transform.Translate(0, _velocityY, 0);
            }

            if ((transform.position.y < _defaultYpos) && !_sliding)
            {
                var pos = transform.position;
                transform.position = new Vector3(pos.x, _defaultYpos, pos.z);
                _velocityY = 0.0f;
                _onGround = true;
            }
        }

        private void Jump()
        {
            if (!_onGround || _sliding) return;

            _velocityY = 1.25f;
            _onGround = false;
            _applyGravity = true;
        }

        private void Slide()
        {
            //if (_sliding || !_onGround) return;
            if (_sliding) return;

            StartCoroutine(SlideAndRise());
        }

        IEnumerator SlideAndRise()
        {
            var pos = transform.position;
            var grav = _gravity;
            _gravity = 0.0f;
            _velocityY = 0.0f;
            _sliding = true;
            
            transform.position = new Vector3(pos.x, 0.5f, pos.z);
            transform.Rotate(-90, 0, 0);

            yield return new WaitForSeconds(0.65f);

            transform.Rotate(90, 0, 0);
            transform.Translate(Vector3.up/2);
            _gravity = grav;
            _sliding = false;
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
