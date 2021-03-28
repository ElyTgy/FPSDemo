using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    
    [SerializeField]private int ammoNum = 5;
    [SerializeField]private AmmoType ammoType;

    void Start()
    { 
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Trigger");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entered");
            other.GetComponent<Ammo>().AddAmmo(ammoType, ammoNum);
            Destroy(gameObject);
        }
    }
}
