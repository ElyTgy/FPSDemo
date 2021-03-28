using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [System.Serializable]
    private class AmmoSlot
    {    
        public int ammoAmount = 10;
        public AmmoType ammoType;
    }

    [SerializeField] private AmmoSlot[] ammoSlots;

    public int GetCurrentAmmoCount(AmmoType ammoType) { return ammoSlots[(int)ammoType].ammoAmount; }
    public void ReduceAmmo(AmmoType ammoType) {--ammoSlots[(int)ammoType].ammoAmount;}
    public void AddAmmo(AmmoType type, int amount) { ammoSlots[(int)type].ammoAmount += amount; }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
