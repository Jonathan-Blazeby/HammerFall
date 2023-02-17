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
    [SerializeField] private Transform targetTransform;

    [SerializeField] private float attackDistance;
    [SerializeField] private float dazedDelay;

    [SerializeField] private AIStates currentState;

    [SerializeField] private bool followingPlayer;
    private GameModes gameMode;

    private Vector3 direction;
    private Vector3 oldPosition;
    private Vector3 currentPosition;

    private bool isGrounded;
    #endregion

    #region MonoBehaviour Callbacks
    private void Update()
    {
        AIUpdate();
    }
    #endregion

    #region Private Methods
    private void AIUpdate()
    {
        switch (currentState)
        {
            case AIStates.Idle:
                break;
            case AIStates.Moving:
                RunMovingState();
                break;
            case AIStates.Attacking:
                RunAttackingState();
                break;
            case AIStates.Dazed:
                RunDazedState();
                break;
            case AIStates.Dead:
                RunDeadState();
                break;
        }
    }

    private void RunMovingState()
    {
        if (!isGrounded) { return; }

        SetNavMeshOn();

        float distance = Vector3.Distance(targetTransform.position, navMeshAgent.nextPosition);
        if (distance > attackDistance) 
        {
            SetDestination();
        }
        else
        {
            currentState = AIStates.Attacking;
        }

        CheckDirection();
        FaceTarget();
    }

    private void SetNavMeshOn()
    {
        enemyRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navMeshAgent.enabled = true;
        enemyRigidbody.isKinematic = true;
    }

    private void SetDestination()
    {
        if(!navMeshAgent.enabled) { return; }
        navMeshAgent.isStopped = false;

        if (gameMode == GameModes.Objectives) { ObjectiveModeDetermineTarget(); }

        navMeshAgent.SetDestination(targetTransform.position);
    }

    private void RunAttackingState()
    {
        float distance = Vector3.Distance(targetTransform.position, navMeshAgent.nextPosition);
        if (distance <= attackDistance)
        {
            if (navMeshAgent.enabled) { navMeshAgent.isStopped = true; }
            if (gameMode == GameModes.Objectives) { ObjectiveModeDetermineTarget(); }
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
        if(!isGrounded)
        {
            StartCoroutine(DazedTimer());
        }
        if (currentState != AIStates.Dead && isGrounded) { currentState = AIStates.Moving; }
    }

    private void CheckDirection()
    {
        //Current Position
        currentPosition = transform.position;
        Vector3 dir = (currentPosition - oldPosition);
        oldPosition = currentPosition;
        currentPosition = transform.position;
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

    private void ObjectiveModeDetermineTarget()
    {
        followingPlayer = EnemyBlackboard.WantToFollowPlayer(this);
        if (followingPlayer)
        {
            targetTransform = GameManager.Instance.GetPlayerTransform();
        }
        else
        {
            targetTransform = GameManager.Instance.GetCurrentObjectiveTransform();
        }
    }
    #endregion

    #region Public Methods
    public void Initialise()
    {
        gameMode = GameManager.Instance.GetGameMode();
        oldPosition = transform.position;

        currentState = AIStates.Moving;

        if (gameMode == GameModes.Objectives)
        {
            ObjectiveModeDetermineTarget();
            return;
        }
        targetTransform = GameManager.Instance.GetPlayerTransform();
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
    public void SetDazed()
    {
        if (currentState != AIStates.Dead)
        {
            currentState = AIStates.Dazed;
        }
    }
    public void SetDead()
    {
        currentState = AIStates.Dead;

        if(gameMode == GameModes.Objectives)
        {
            EnemyBlackboard.StopFollowPlayer(this);
        }
    }
    public bool SetIsGrounded(bool onGround) => isGrounded = onGround;
    public AIStates GetState() => currentState;

    #endregion

}
