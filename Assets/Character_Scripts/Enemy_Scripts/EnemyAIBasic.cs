using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBasic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Rigidbody enemyRigidbody;
    private Transform playerTransform;
    private Vector3 direction;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private Vector2 moveInput;
    private float rotationInput;
    [SerializeField] private float attackDistance;
    [SerializeField] private float dazedDelay;
    private float dazedTimer;
    private int willAttack; //Above 0 = Attack

    public Vector3 GetDirection()
    {
        return direction;
    }

    public int GetWillAttack()
    {
        return willAttack;
    }

    private void OnEnable()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        oldPosition = transform.position;
    }
    
    private void Update()
    {
        dazedTimer -= Time.deltaTime;

        if(dazedTimer <= 0)
        {
            navMeshAgent.enabled = true;
            enemyRigidbody.isKinematic = true;

            if (Vector3.Distance(playerTransform.position, navMeshAgent.nextPosition) >= attackDistance)
            {
                if (navMeshAgent.enabled) { navMeshAgent.isStopped = false; navMeshAgent.SetDestination(playerTransform.position); }
                willAttack = 0;
            }
            else if (Vector3.Distance(playerTransform.position, navMeshAgent.nextPosition) < attackDistance)
            {
                if (navMeshAgent.enabled) { navMeshAgent.isStopped = true; }
                willAttack = 1;
            }
            else
            {
                willAttack = 1;
            }

            CheckDirection();
            FaceTarget();
        }

    }

    private void CheckDirection()
    {
        newPosition = transform.position;
        var media = (newPosition - oldPosition);
        oldPosition = newPosition;
        newPosition = transform.position;
        direction = (media / Time.deltaTime).normalized;   
    }

    //Allows for enemy rotation to function properly with the navmesh agent, but only when its movementManager is set to not use the Calculated Rotation
    private void FaceTarget()
    {
        var turnTowardNavSteeringTarget = navMeshAgent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        if (direction.x != 0 && direction.z != 0) //Eliminates unnecessary calculations
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }

    public bool GetDazed() 
    { 
        if(dazedTimer > 0) 
        {
            return true;
        } 
        else { return false; }
    }

    public void SetDazed()
    {
        dazedTimer = dazedDelay;
        navMeshAgent.enabled = false;
        enemyRigidbody.isKinematic = false;
    }
}
