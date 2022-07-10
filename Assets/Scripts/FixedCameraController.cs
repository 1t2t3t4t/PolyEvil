using System;
using Cinemachine;
using UnityEngine;

public class FixedCameraController : MonoBehaviour
{
    [SerializeField] private bool ShouldLookAt = false;
    
    private CinemachineBrain _cinemachineBrain;
    private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _virtualCamera.LookAt = other.transform;
            _virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _virtualCamera.enabled = false;
        }
    }
}