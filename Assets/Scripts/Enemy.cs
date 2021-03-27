using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyHealth enemyHealth { get; set; }
    public EnemyAI enemyAi { get; set; }


    public void Start()
    {
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        enemyAi =  gameObject.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
