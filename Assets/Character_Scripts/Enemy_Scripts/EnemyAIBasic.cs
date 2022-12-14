using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIStates 
{ 
    Idle, //0 
    Moving, //1
    Attacking, //2
    Dazed, //3
    Dead //4
}

public class EnemyAIBasic : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Rigidbody enemyRigidbody;
    private Transform playerTransform;
    private Vector3 direction;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    [SerializeField] private float attackDistance;
    [SerializeField] private float dazedDelay;
    [SerializeField] private AIStates currentState;
    #endregion

    #region MonoBehaviour Callbacks
    private void OnEnable()
    {
        playerTransform = GameManager.Instance.GetPlayerTransform();
        oldPosition = transform.position;
        if (playerTransform)
        {
            currentState = AIStates.Moving;
        }
    }

    private void Update()
    {
        AIUpdate();
    }
    #endregion

    #region Private Methods
    private void AIUpdate()
    {
        switch ((int)currentState)
        {
            case 0:
                break;
            case 1:
                RunMovingState();
                break;
            case 2:
                RunAttackingState();
                break;
            case 3:
                RunDazedState();
                break;
            case 4:
                RunDeadState();
                break;
        }
    }

    private void RunMovingState()
    {
        enemyRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navMeshAgent.enabled = true;
        enemyRigidbody.isKinematic = true;

        if (Vector3.Distance(playerTransform.position, navMeshAgent.nextPosition) > attackDistance)
        {
            if (navMeshAgent.enabled)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(playerTransform.position);
            }
        }
        else
        {
            currentState = AIStates.Attacking;
        }

        CheckDirection();
        FaceTarget();
    }

    private void RunAttackingState()
    {
        if (Vector3.Distance(playerTransform.position, navMeshAgent.nextPosition) <= attackDistance)
        {
            if (navMeshAgent.enabled)
            {
                navMeshAgent.isStopped = true;
            }
        }
        else
        {
            currentState = AIStates.Moving;
        }


        CheckDirection();
        FaceTarget();
    }

    private void RunDazedState()
    {
        navMeshAgent.enabled = false;
        enemyRigidbody.isKinematic = false;
        StartCoroutine(DazedTimer());
    }

    private void RunDeadState()
    {
        navMeshAgent.enabled = false;
        enemyRigidbody.isKinematic = false;
        enemyRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private IEnumerator DazedTimer()
    {
        if (currentState != AIStates.Dead) { currentState = AIStates.Dazed; }
        yield return new WaitForSeconds(dazedDelay);
        if (currentState != AIStates.Dead) { currentState = AIStates.Moving; }
    }

    private void CheckDirection()
    {
        newPosition = transform.position;
        Vector3 dir = (newPosition - oldPosition);
        oldPosition = newPosition;
        newPosition = transform.position;
        direction = (dir / Time.deltaTime).normalized;
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
    #endregion

    #region Public Methods
    public Vector3 GetDirection()
    {
        return direction;
    }
    public void SetDazed() => currentState = AIStates.Dazed;
    public void SetDead() => currentState = AIStates.Dead;
    public AIStates GetState() => currentState;
    #endregion

}
