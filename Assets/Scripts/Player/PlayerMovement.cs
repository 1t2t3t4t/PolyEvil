using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public float Acceleration = 1f;
        
        public float WalkSpeed = 3f;
        public float RotateSpeed = 3f;
    
        private Animator _animator;
        private CharacterController _characterController;
        private PlayerAim _playerAim;
        private bool _isMoving;
        private bool _isRunning;
        private Vector3 _currentVelocity;
        
        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public bool Moving => _isMoving;
        private float RunSpeed => WalkSpeed * 2;
        public float Speed => _isRunning ? RunSpeed : WalkSpeed;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _playerAim = GetComponent<PlayerAim>();
        }

        private void Update()
        {
            var horizontalMove = Input.GetAxis("Horizontal");
            var currentTransform = transform;
            var rotation = currentTransform.rotation;
            var turnRight = Quaternion.Euler(rotation.eulerAngles + (horizontalMove * Vector3.up));
            var adjustedRotSpeed = _playerAim.IsAim ? RotateSpeed / 2 : RotateSpeed;
            rotation = Quaternion.RotateTowards(rotation, turnRight, adjustedRotSpeed * Time.deltaTime);
            currentTransform.rotation = rotation;

            UpdateMovement();
            
            UpdateAnimState();
        }

        private void UpdateMovement()
        {
            _isRunning = Input.GetButton("Run");
            var verticalMove = Input.GetAxis("Vertical");

            var currentTransform = transform;
            var movementDir = _playerAim.IsAim ? Vector3.zero : currentTransform.forward * verticalMove;
            _currentVelocity = Vector3.MoveTowards(
                _currentVelocity,
                movementDir.normalized * Speed,
                Acceleration * Time.deltaTime
            );
            _characterController.Move(_currentVelocity * Time.deltaTime);

            _isMoving = movementDir.sqrMagnitude != 0f;
        }

        private void UpdateAnimState()
        {
            var velocity = _currentVelocity;
            var velocityMagnitude = velocity.magnitude;
            var movement = velocityMagnitude / WalkSpeed;
            var isForward = Vector3.Dot(transform.forward, velocity) >= 0;
            var signedMovement = movement * (isForward ? 1 : -1);
            
            Debug.Log(signedMovement);
            _animator.SetFloat(Movement, signedMovement);
            _animator.SetBool(IsMoving, _isMoving);
        }
    }
}