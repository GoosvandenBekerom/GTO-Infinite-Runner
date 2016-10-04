using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMotor : MonoBehaviour
    {

        private Transform _player;
        private Vector3 _offset;
        private Vector3 _moveVector;

        private float _transition = 0.0f;
        private float _animationDuration = 3.5f;
        private Vector3 _animationOffset = new Vector3(0, 5, 5);
        
        void Start ()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _offset = transform.position - _player.position;
        }
	
        void Update ()
        {
            _moveVector = _player.position + _offset;
            
            _moveVector.x = 0;

            if (_transition > 1)
            {
                transform.position = _moveVector;
            }
            else
            {
                // camera animation at game start
                transform.position = Vector3.Lerp(_moveVector + _animationOffset, _moveVector, _transition);
                _transition += Time.deltaTime*1/_animationDuration;
                transform.LookAt(_player.position + Vector3.up);
            }
        }
    }
}
