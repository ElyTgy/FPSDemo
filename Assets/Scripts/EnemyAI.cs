using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private enum states { idle, follow, attack };

    [SerializeField] private float targetFollowDist = 10.0f;
    [SerializeField] private float targetStopDist = 1.0f;

    [SerializeField] public Color[] colorStateArr = new Color[3];

    private float distToTarget;
    private states state = states.idle;

    private NavMeshAgent navMesh;
    [SerializeField] private Transform targetTransform;
    [SerializeField]private MeshRenderer bodyRenderer;

    [SerializeField] private Color followGizmosColor = Color.red;
    [SerializeField] private Color stopGizmosColor = Color.blue;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        distToTarget = Vector3.Distance(transform.position, targetTransform.position);
        navMesh.stoppingDistance = targetStopDist;
    }

    // Update is called once per frame
    void Update()
    {
        distToTarget = Vector3.Distance(transform.position, targetTransform.position);

        if (distToTarget < targetStopDist)
        {
            state = states.attack;
            bodyRenderer.material.color = colorStateArr[(int)states.attack];
            //code for attacking
        }
        else if (distToTarget <= targetFollowDist)
        {
            state = states.follow;
            navMesh.SetDestination(targetTransform.position);
        }
        else
        {
            state = states.idle;
        }


        if ((state == states.attack) && (bodyRenderer.material.color != colorStateArr[(int)states.attack]))
        {
            bodyRenderer.material.color = colorStateArr[(int)states.attack];
        }
        else if ((state == states.follow) && (bodyRenderer.material.color != colorStateArr[(int)states.follow]))
        {
            bodyRenderer.material.color = colorStateArr[(int)states.follow];
        }
        else if ((state == states.idle) && (bodyRenderer.material.color != colorStateArr[(int)states.idle]))
        {
            bodyRenderer.material.color = colorStateArr[(int)states.idle];
        }   
    }

    private void OnDrawGizmos()
    {
        //starts chasing at this range
        Gizmos.color = followGizmosColor;
        Gizmos.DrawWireSphere(transform.position, targetFollowDist);
        
        //stops chasing at this range
        Gizmos.color = stopGizmosColor;
        Gizmos.DrawWireSphere(transform.position, targetStopDist);
    }
}
