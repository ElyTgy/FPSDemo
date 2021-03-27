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
    private states currState = states.idle;

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
        currState = GetCurrentState();
        PerformActionForState(currState);
        SetColorForCurrentState();
    }

    private states GetCurrentState()
    {
        distToTarget = Vector3.Distance(transform.position, targetTransform.position);

        if (distToTarget < targetStopDist)
        {
            return states.attack;
        }
        else if (distToTarget <= targetFollowDist)
        {
            return states.follow;
        }
        else
        {
            return  states.idle;
        }
    }

    private void PerformActionForState(states state)
    {
        if (state == states.attack)
        {
            Attack();
        }
        else if (state == states.follow)
        {
            Follow();
        }
        else
        {
            Idle();
        }
    }

    private void Attack()
    {
        //Code for attack
    }

    private void Follow()
    {
        navMesh.SetDestination(targetTransform.position);
    }

    private void Idle()
    {
        //code for idle
    }

    private void SetColorForCurrentState()
    {

        if ((currState == states.attack) && (bodyRenderer.material.color != colorStateArr[(int)states.attack]))
        {
            bodyRenderer.material.color = colorStateArr[(int)states.attack];
        }
        else if ((currState == states.follow) && (bodyRenderer.material.color != colorStateArr[(int)states.follow]))
        {
            bodyRenderer.material.color = colorStateArr[(int)states.follow];
        }
        else if ((currState == states.idle) && (bodyRenderer.material.color != colorStateArr[(int)states.idle]))
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
