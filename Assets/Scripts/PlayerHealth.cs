using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health = 100.0f;
    private DeathHandler deathHandler;
    private DisplayDamage damageDisplay;

    void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
        damageDisplay = GetComponent<DisplayDamage>();
    }

    public void TakeDamage(float damage)
    {
        
        if (health <= 0.0f)
        {
            damageDisplay.TurnOffCanvas();
            deathHandler.HandleDeath();
        }
        else
        {
            health -= damage;
            damageDisplay.ShowDamageImage();
        }

    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
