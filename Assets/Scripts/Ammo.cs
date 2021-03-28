using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 10;

    public int GetCurrentAmmoCount() { return ammoAmount; }
    public void ReduceAmmo(){--ammoAmount;}


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
