using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool _onGround;
        private bool _sliding;

        private const int JumpHeight = 275;
        private const int SlideVelocity = 750; // force downwards for sliding after jump

        private Rigidbody _rigidbody;

        // touch properties
        private const int MinSwipeDist = 50;
        private Vector2 _startPos;

        private void Awake()
        {
            _onGround = true;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {

#if UNITY_EDITOR
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
#endif

            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startPos = touch.position;
                        break;
                    case TouchPhase.Ended:
                        var distance = 
                            new Vector2(touch.position.x, touch.position.y) - 
                            new Vector2(_startPos.x, _startPos.y);

                        var vertical = Math.Abs(distance.y) > Math.Abs(distance.x);
                        var vertDirection = distance.y;
                        var horDirection = distance.x;

                        if (Math.Abs(vertDirection) < MinSwipeDist && Math.Abs(horDirection) < MinSwipeDist) break;

                        if (vertDirection > 0 && vertical)
                        {
                            Jump();
                            break;
                        }
                        if (vertDirection < 0 && vertical)
                        {
                            Slide();
                            break;
                        }
                        if (horDirection < 0 && !vertical)
                        {
                            MoveLeft();
                            break;
                        }
                        if (horDirection > 0 && !vertical)
                        {
                            MoveRight();
                        }
                        break;
                }
            }
        }

        private void Jump()
        {
            if (!_onGround || _sliding) return;

            _rigidbody.AddForce(Vector3.up * JumpHeight);
            _onGround = false;
        }

        private void Slide()
        {
            if (_sliding) return;

            StartCoroutine(SlideAndRise());
        }

        IEnumerator SlideAndRise()
        {
            transform.localScale += Vector3.down/2;
            _rigidbody.AddForce(Vector3.down * SlideVelocity);
            _sliding = true;

            yield return new WaitForSeconds(0.65f);

            transform.localScale += Vector3.up / 2;
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

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor") && !_onGround)
            {
                _onGround = true;
            }
        }
    }
}
