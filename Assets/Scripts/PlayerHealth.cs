using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health = 100.0f;

    void Start()
    {
                
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
