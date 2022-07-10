using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAim : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;

    private static readonly int IsAiming = Animator.StringToHash("IsAiming");

    public bool IsAim { get; private set; } = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        IsAim = Input.GetButton("Fire2");
        _animator.SetBool(IsAiming, IsAim);
    }
}