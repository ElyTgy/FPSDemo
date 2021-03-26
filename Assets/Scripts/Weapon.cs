using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private float maxRayDist = 200.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out raycastHit, maxRayDist);
            Debug.Log("Hit ");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
    }
}
