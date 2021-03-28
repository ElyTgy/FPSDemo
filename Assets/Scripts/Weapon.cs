using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private Camera firstPersonCamera;

    [SerializeField] private float normalSensitivity = 3.0f;

    [SerializeField] private float maxRayDist = 200.0f;
    
    [SerializeField] private float weaponDamage = 10.0f;
    [SerializeField] Ammo ammoSlot;

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
    }

    private void Shoot()
    {
        ammoSlot.ReduceAmmo();

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
