﻿using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool _onGround;
        private bool _sliding;

        private const int JumpHeight = 275;
        private const int RollVelocity = 750; // force downwards for sliding after jump
        
        private Rigidbody _rigidbody;
        private CapsuleCollider _collider;
        private Animator _animator;

        // touch properties
        private const int MinSwipeDist = 50;
        private Vector2 _startPos;

        void Awake()
        {
            _onGround = true;
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            _rigidbody.position = new Vector3(0, 1, 0);
        }

        /// <summary>
        /// Handles Player Input
        /// </summary>
        void Update()
        {
            if (GameManager.Instance.GameOver) return;

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Roll();
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
                            Roll();
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

        void LateUpdate()
        {
            if (GameManager.Instance.GameOver) return;

            var velo = _rigidbody.velocity;
            velo.z = GameManager.Instance.MovementSpeed;

            _rigidbody.velocity = velo;
        }

        private void Jump()
        {
            if (!_onGround || _sliding) return;

            _animator.SetTrigger("Jump");
            _rigidbody.AddForce(Vector3.up * JumpHeight);
            _onGround = false;
        }

        private void Roll()
        {
            if (_sliding) return;

            _animator.SetTrigger("Roll");
            StartCoroutine(RollAndRise());

            // TODO: fix rolling with animation and animationevents
        }

        IEnumerator RollAndRise()
        {
            _rigidbody.AddForce(Vector3.down * RollVelocity);
            _collider.height = 1;
            _collider.center += new Vector3(0, -0.5f, 0);
            _sliding = true;

            yield return new WaitForSeconds(0.65f);

            _collider.height = 2;
            _collider.center = new Vector3(_collider.center.x, 0, _collider.center.z);
            _sliding = false;
        }

        private void MoveLeft()
        {
            if (transform.position.x > -2)
            {
                _animator.SetTrigger("Strafe Left");
                _rigidbody.MovePosition(_rigidbody.position += Vector3.left);
            }
        }

        private void MoveRight()
        {
            if (transform.position.x < 2)
            {
                _animator.SetTrigger("Strafe Right");
                _rigidbody.MovePosition(_rigidbody.position += Vector3.right);
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
