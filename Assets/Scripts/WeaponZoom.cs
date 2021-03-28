using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private float zoomedFOV = 25.0f;
    [SerializeField] private float normalFOV = 50.0f;

    [SerializeField] private float zoomedSensitivity = 0.5f;
    [SerializeField] private float normalSensitivity = 3.0f;

    [SerializeField] public RigidbodyFirstPersonController FPSPlayer;
    [SerializeField] private Camera firstPersonCamera;

    private void HandleZoom()
    {
        if (Input.GetMouseButtonDown(1) && (Camera.main.fieldOfView != zoomedFOV))
        {
            firstPersonCamera.fieldOfView = zoomedFOV;
            FPSPlayer.mouseLook.XSensitivity = zoomedSensitivity;
            FPSPlayer.mouseLook.YSensitivity = zoomedSensitivity;
        }
        else if (!Input.GetMouseButton(1) && (Camera.main.fieldOfView == zoomedFOV))
        {
            firstPersonCamera.fieldOfView = normalFOV;
            FPSPlayer.mouseLook.YSensitivity = normalSensitivity;
            FPSPlayer.mouseLook.XSensitivity = normalSensitivity;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleZoom();
    }
}
