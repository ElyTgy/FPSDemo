using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health = 100.0f;

    public void TakeDamage(float damage)
    {
        Debug.Log("Health: " + health);
        health -= damage;
        if (health <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        //die
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
