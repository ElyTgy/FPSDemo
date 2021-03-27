using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private float maxRayDist = 200.0f;
    [SerializeField] private float weaponDamage = 10.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
            //show effects
            
        }
    }

    private void Shoot()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out raycastHit, maxRayDist))
        {
            Debug.Log("Hit something");
            EnemyHealth hitEnemyHealth = raycastHit.transform.GetComponent<EnemyHealth>();
            if (hitEnemyHealth != null)
            {
                Debug.Log("hit enemy");
                hitEnemyHealth.TakeDamage(weaponDamage);
            }
        }
    }
}
