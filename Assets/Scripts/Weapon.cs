using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] public RigidbodyFirstPersonController FPSPlayer;
    [SerializeField] private Camera firstPersonCamera;

    [SerializeField] private float zoomedFOV = 25.0f;
    [SerializeField] private float normalFOV = 50.0f;

    [SerializeField] private float zoomedSensitivity = 0.5f;
    [SerializeField] private float normalSensitivity = 3.0f;

    [SerializeField] private float maxRayDist = 200.0f;
    
    [SerializeField] private float weaponDamage = 10.0f;
    
    private EnemyToPlayer enemy;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            muzzleEffect.Play();
            Shoot();
        }
        HandleZoom();
    }

    private void HandleZoom()
    {
        if(Input.GetMouseButtonDown(1) && (Camera.main.fieldOfView != zoomedFOV))
        {
            Camera.main.fieldOfView = zoomedFOV;
            FPSPlayer.mouseLook.XSensitivity = zoomedSensitivity;
            FPSPlayer.mouseLook.YSensitivity = zoomedSensitivity;
        }
        else if (!Input.GetMouseButton(1) && (Camera.main.fieldOfView == zoomedFOV))
        {
            Camera.main.fieldOfView = normalFOV;
            FPSPlayer.mouseLook.YSensitivity = normalSensitivity;
            FPSPlayer.mouseLook.XSensitivity = normalSensitivity;
        }
    }

    private void Shoot()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out raycastHit, maxRayDist))
        {
            Debug.Log("Hit something");
            enemy = raycastHit.transform.GetComponent<EnemyToPlayer>();
            
            if (enemy != null)
            {
                enemy.enemyHealth.TakeDamage(weaponDamage);
                enemy.enemyAi.setProvoked = true;

            }
        }
    }
}
