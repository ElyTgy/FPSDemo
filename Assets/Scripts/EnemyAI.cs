using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//TODO: enemy currently wont

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private enum states { idle, follow, attack };
    [SerializeField] public Color[] colorStateArr = new Color[3];
    
    private NavMeshAgent navMesh;
    [SerializeField] private Transform targetTransform;
    [SerializeField]private MeshRenderer bodyRenderer;

    [SerializeField] private float targetFollowDist = 10.0f;
    [SerializeField] private float targetStopDist = 1.0f;
    [SerializeField] private float provokedAddDist = 10.0f;
    [SerializeField] private float maxProvokedStopDist = 100.0f;
    private float provokedStopDist = 0.0f;

    [SerializeField] private Color followGizmosColor = Color.red;
    [SerializeField] private Color stopGizmosColor = Color.blue;
    [SerializeField] private Color provokeGizmosColor = Color.black;

    private float distToTarget;
    private states currState = states.idle;
    public bool setProvoked = false;
    public bool isProvoked = false;

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

        currState = GetCurrentState();
        HandleIsProvoked();
        PerformActionForState(currState);
        SetColorForCurrentState();
    }

    private states GetCurrentState()
    {
        if (distToTarget < targetStopDist)
        {
            return states.attack;
        }
        else if ((distToTarget <= targetFollowDist) || setProvoked || isProvoked)
        {
            if (setProvoked) 
            { 
                CalculateDistForProvoked();
                isProvoked = true;
                setProvoked = false;
            }
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

    private void CalculateDistForProvoked()
    {
        provokedStopDist = distToTarget + provokedAddDist;
        if(provokedStopDist > maxProvokedStopDist)
        {
            provokedStopDist = maxProvokedStopDist;
        }
    }

    private void HandleIsProvoked()
    {
        if(isProvoked)
        {
            if (distToTarget >= provokedStopDist)
            {
                currState = states.idle;
                isProvoked = false;
            }
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

        if(isProvoked)
        {
            Gizmos.color = provokeGizmosColor;
            Gizmos.DrawWireSphere(transform.position, provokedStopDist);
        }
    }
}
