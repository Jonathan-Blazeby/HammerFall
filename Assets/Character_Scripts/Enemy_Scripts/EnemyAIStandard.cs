using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIStandard : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Rigidbody ownRigidbody;
    [SerializeField] private Transform targetTransform;

    [SerializeField] private float attackDistance;
    [SerializeField] private float dazedDelay;

    [SerializeField] private State currentState;

    [SerializeField] private bool followingPlayer;
    private GameModes gameMode;

    private Vector3 direction;
    private Vector3 oldPosition;
    private Vector3 currentPosition;

    private bool isGrounded;

    private IdleState idleState = new IdleState();
    private MoveState moveState = new MoveState();
    private AttackState attackState = new AttackState();
    private DazedState dazedState = new DazedState();
    private DeadState deadState = new DeadState();
    #endregion

    #region Gets & Sets
    public NavMeshAgent GetNavMeshAgent() => navMeshAgent;
    public Rigidbody GetRigidbody() => ownRigidbody;
    public Transform GetTargetTransform() => targetTransform;
    public void SetTargetTransform(Transform target) => targetTransform = target;
    public float GetAttackDistance() => attackDistance;
    public float GetDazedDelay() => dazedDelay;
    public State GetCurrentState() => currentState;
    public State SetCurrentState(State newState) => currentState = newState;
    public bool GetFollowingPlayer() => followingPlayer;
    public bool SetFollowingPlayer(bool follow) => followingPlayer = follow;
    public GameModes GetGameMode() => gameMode;
    public Vector3 GetDirection() => direction;
    public void SetDirection(Vector3 dir) => direction = dir;
    public Vector3 GetOldPosition() => oldPosition;
    public void SetOldPosition(Vector3 pos) => oldPosition = pos; 
    public Vector3 GetCurrentPosition() => currentPosition;
    public void SetCurrentPosition(Vector3 pos) => currentPosition = pos;
    public bool GetIsGrounded() => isGrounded;
    public void SetIsGrounded(bool grounded) => isGrounded = grounded;
    public State GetIdleState() => idleState;
    public State GetMoveState() => moveState;
    public State GetAttackState() => attackState;
    public State GetDazedState() => dazedState;
    public State GetDeadState() => deadState;
    #endregion

    #region Monobehavior Callbacks
    private void Update()
    {
        currentState.Execute(this, Time.deltaTime);
    }
    #endregion

    #region Private Methods

    #endregion

    #region Public Methods
    public void Initialise()
    {
        gameMode = GameManager.Instance.GetGameMode();
        oldPosition = transform.position;

        currentState = moveState;
    }

    public void SetNavMeshOn()
    {
        ownRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        navMeshAgent.enabled = true;
        ownRigidbody.isKinematic = true;
    }

    public void SetNavMeshOff()
    {
        navMeshAgent.enabled = false;
        ownRigidbody.isKinematic = false;
    }
    #endregion
}
