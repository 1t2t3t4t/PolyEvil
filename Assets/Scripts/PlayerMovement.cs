using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float WalkSpeed = 3f;
    public float RotateSpeed = 3f;
    
    private Animator _animator;
    private CharacterController _characterController;
    private PlayerAim _playerAim;
    private bool _isMoving = false;

    private static readonly int Movement = Animator.StringToHash("Movement");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    public bool Moving => _isMoving;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _playerAim = GetComponent<PlayerAim>();
    }

    private void Update()
    {
        var verticalMove = Input.GetAxis("Vertical");
        var horizontalMove = Input.GetAxis("Horizontal");

        var currentTransform = transform;
        var movementDir = _playerAim.IsAim ? Vector3.zero : currentTransform.forward * verticalMove;
        _characterController.Move(movementDir.normalized * (WalkSpeed * Time.deltaTime));

        _isMoving = movementDir.sqrMagnitude != 0f;

        var rotation = currentTransform.rotation;
        var turnRight = Quaternion.Euler(rotation.eulerAngles + (horizontalMove * Vector3.up));
        var adjustedRotSpeed = _playerAim.IsAim ? RotateSpeed / 2 : RotateSpeed;
        rotation = Quaternion.RotateTowards(rotation, turnRight, adjustedRotSpeed * Time.deltaTime);
        currentTransform.rotation = rotation;

        UpdateAnimState();
    }

    private void UpdateAnimState()
    {
        var velocity = _characterController.velocity;
        var velocityMagnitude = velocity.magnitude;
        var movement = velocityMagnitude / WalkSpeed;
        var isForward = Vector3.Dot(transform.forward, velocity) >= 0;
        _animator.SetFloat(Movement, movement * (isForward ? 1 : -1));
        _animator.SetBool(IsMoving, _isMoving);
    }
}