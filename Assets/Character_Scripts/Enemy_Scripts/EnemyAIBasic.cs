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
        if(Vector3.Distance(playerTransform.position, navMeshAgent.nextPosition) >= attackDistance)
        {
            if(navMeshAgent.enabled) { navMeshAgent.isStopped = false; navMeshAgent.SetDestination(playerTransform.position); }
            willAttack = 0;
        }
        else if(Vector3.Distance(playerTransform.position, navMeshAgent.nextPosition) < attackDistance)
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
        if (direction != Vector3.zero) //Eliminates unnecessary calculations
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.root != gameObject)
        {
            if (collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Player")
            {
                navMeshAgent.enabled = false;
                enemyRigidbody.isKinematic = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.root != gameObject)
        {
            if (collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Player")
            {
                navMeshAgent.enabled = true;
                enemyRigidbody.isKinematic = true;
            }
        }
    }
}
