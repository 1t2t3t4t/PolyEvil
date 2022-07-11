using System;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class FixedCameraController : MonoBehaviour
{
    [SerializeField] private bool ShouldLookAt = false;
    
    private CinemachineBrain _cinemachineBrain;
    private CinemachineVirtualCamera _virtualCamera;
    private Collider _collider;

    private void Start()
    {
        _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _collider = GetComponent<Collider>();
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

public static class FixedCameraControllerGizmos
{
    [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.NonSelected)]
    public static void DrawGizmos(FixedCameraController controller, GizmoType type)
    {
        var bounds = controller.GetComponent<Collider>().bounds;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}