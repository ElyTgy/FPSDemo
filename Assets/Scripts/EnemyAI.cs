using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//spaghetti code:


public class EnemyAI : MonoBehaviour
{
    private enum states { idle, follow, attack };
    //TODO: remove and change animation states by enums
    private Dictionary<states, string> dict = new Dictionary<states, string>()
    {
        {states.idle, "Idle" },
        {states.follow, "Follow" },
        {states.attack, "Attack" }
    };

    [SerializeField] public Color[] colorStateArr = new Color[3];
    
    private NavMeshAgent navMesh;
    private Animator animator;
    [SerializeField] private Transform targetTransform;
    [SerializeField]private MeshRenderer bodyRenderer;

    [SerializeField] private float turnSpeed = 5.0f;

    ///distance where the enemy is triggered to follow player
    [SerializeField] private float targetFollowDist = 10.0f;
    ///distance where the enemy stops following the player
    [SerializeField] private float targetStopDist = 1.0f;
    ///The amount that is added to players and enemys current distance in order to form a 'provoked radius'(for when the enemy is shot)
    [SerializeField] private float provokedAddDist = 10.0f;
    ///The maximum radius for provoked
    [SerializeField] private float maxProvokedStopDist = 100.0f;
    ///The amount that is added to follow radius after isProvoked is turned off(simulate cautionuess)
    [SerializeField] private float targetFollowDistAdd = 10.0f;
    ///The duration during which follow radius is longer
    [SerializeField] private float timeToRemainCautious = 10.0f;
    ///current radius of provoked 
    private float provokedStopDist = 0.0f;


    [SerializeField] private Color followGizmosColor = Color.red;
    [SerializeField] private Color stopGizmosColor = Color.blue;
    [SerializeField] private Color provokeGizmosColor = Color.black;

    private float distToTarget;
    private states currState = states.idle;
    //Is set true the first frame the enemy is provoked
    [HideInInspector] public bool setProvoked = false;
    //remains true unitl the player exits the 'provoked radius'
    [HideInInspector]public bool isProvoked = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.stoppingDistance = targetStopDist;
        distToTarget = Vector3.Distance(transform.position, targetTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distToTarget = Vector3.Distance(transform.position, targetTransform.position);

        currState = GetCurrentState();
        HandleIsProvokedExit();
        PerformActionForState(currState);
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

    private void SetFollowRadiusToNormal()
    {
        targetFollowDist -= targetFollowDistAdd;
    }

    private void StopProvokedMode()
    {
        currState = states.idle;
        isProvoked = false;
    }

    private void BeCautious()
    {

        targetFollowDist += targetFollowDistAdd;
        Invoke("SetFollowRadiusToNormal", timeToRemainCautious);
    }

    private void HandleIsProvokedExit()
    {
        if(isProvoked)
        {
            if (distToTarget >= provokedStopDist)
            {
                StopProvokedMode();
                BeCautious();
            }
        }
    }

    private void Attack()
    {
        SetAnimationTo(states.attack);
        FaceTarget();
    }

    private void Follow()
    {
        SetAnimationTo(states.follow);
        navMesh.SetDestination(targetTransform.position);
    }

    private void Idle()
    {
        SetAnimationTo(states.idle);
    }

    private void SetAnimationTo(states state)
    {
        animator.SetTrigger(dict[state]);
    }

    private void FaceTarget()
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
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
