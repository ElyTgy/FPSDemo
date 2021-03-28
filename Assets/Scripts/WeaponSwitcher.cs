using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] public int activeWeapon;
    [HideInInspector] public bool enabled = true;

    void Start()
    {
    }

    void Update()
    {
        if(enabled)
        {
            ProccessScrollWheel();
            ProcessKey();
            SetWeaponActive();
        }
    }

    private void ProcessKey()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeWeapon = 2;
        }
    }

    private void ProccessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(activeWeapon >= transform.childCount - 1)
            {
                activeWeapon = 0;
            }
            else
            {
                ++activeWeapon;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (activeWeapon == 0)
            {
                activeWeapon = transform.childCount - 1;
            }
            else
            {
                --activeWeapon;
            }
        }

    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;
        foreach(Transform weapon in transform)
        {
            if(weaponIndex == activeWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            ++weaponIndex;
        }
    }
}
