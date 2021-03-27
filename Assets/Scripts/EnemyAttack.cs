using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 40.0f;
    [SerializeField] private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackHitEvent()
    {
        if(playerHealth == null) { Debug.LogError("Assign player in EnemyAttackScript"); return; }
        playerHealth.TakeDamage(damage);
    }
}
