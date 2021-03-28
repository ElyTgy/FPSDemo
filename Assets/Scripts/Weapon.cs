using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private Camera firstPersonCamera;

    [SerializeField] private AmmoType ammoType;

    [SerializeField] private float maxRayDist = 200.0f;
    [SerializeField] private float weaponDamage = 10.0f;
    public Ammo ammoSlot;
    [SerializeField] private float timeBetweenShots = 2.0f;
    [SerializeField] private float timeBetweenWeaponSwitch = 1.0f;

    [SerializeField] TextMeshProUGUI ammoText;


    private EnemyToPlayer enemy;
    [HideInInspector]public bool canShoot = true;


    private void Start()
    {
    }

    private void OnEnable()
    {
        StartCoroutine("EnableSwitchDelay");
    }

    void Update()
    {
        DisplayAmmo();
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
           StartCoroutine(Shoot());
        }   
    }

    private void DisplayAmmo()
    {
        ammoText.text = ammoSlot.GetCurrentAmmoCount(ammoType).ToString();
    }

    private void ProccessRaycast()
    {
        muzzleEffect.Play();

        RaycastHit raycastHit;
        if (Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out raycastHit, maxRayDist))
        {
            enemy = raycastHit.transform.GetComponent<EnemyToPlayer>();

            if (enemy != null)
            {
                HitEnemy();
            }
        }
    }

    private void HitEnemy()
    {
        enemy.enemyHealth.TakeDamage(weaponDamage);
        enemy.enemyAi.setProvoked = true;
    }

    IEnumerator EnableSwitchDelay()
    {
        yield return new WaitForSeconds(timeBetweenWeaponSwitch);
        canShoot = true;
    }

    IEnumerator Shoot()
    {
        Debug.Log("Shoot");
        if (ammoSlot.GetCurrentAmmoCount(ammoType) > 0)
        {
            canShoot = false;
            ammoSlot.ReduceAmmo(ammoType);
            ProccessRaycast();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

}
