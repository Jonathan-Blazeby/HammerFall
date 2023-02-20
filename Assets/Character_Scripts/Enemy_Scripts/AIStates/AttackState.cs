using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    #region Private Fields
    private EnemyAIStandard aiInfo;
    private NavMeshAgent navMeshAgent;
    private Transform aiTransform;

    #endregion

    #region Private Methods
    private void DetermineAction()
    {
        Transform target = aiInfo.GetTargetTransform();
        if (target == null)
        {
            target = GameManager.Instance.GetPlayerTransform();
        }
        float distance = Vector3.Distance(target.position, navMeshAgent.nextPosition);

        float attackDistance = aiInfo.GetAttackDistance();
        if (distance <= attackDistance)
        {
            if (navMeshAgent.enabled) { navMeshAgent.isStopped = true; }
            if (aiInfo.GetGameMode() == GameModes.Objectives) { ObjectiveModeDetermineTarget(); }
        }
        else
        {
            aiInfo.SetCurrentState(aiInfo.GetMoveState());
        }

        CheckDirection();
        FaceTarget();
    }

    private void CheckDirection()
    {
        Vector3 currentPos = aiTransform.position;
        Vector3 oldPos = aiInfo.GetOldPosition();
        Vector3 dir = (currentPos - oldPos);

        aiInfo.SetOldPosition(currentPos);
        aiInfo.SetCurrentPosition(currentPos);

        Vector3 newDirection = (dir / Time.deltaTime).normalized;

        aiInfo.SetDirection(newDirection);
    }

    //Allows for enemy rotation to function properly with the navmesh agent, but only when its movementManager is set to not use the Calculated Rotation
    private void FaceTarget()
    {
        var turnTowardNavSteeringTarget = aiInfo.GetNavMeshAgent().steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - aiTransform.position).normalized;
        if (direction.x != 0 && direction.z != 0) //Eliminates unnecessary calculations
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            aiTransform.rotation = Quaternion.Slerp(aiTransform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }

    private void ObjectiveModeDetermineTarget()
    {
        bool followPlayer = EnemyBlackboard.WantToFollowPlayer(aiInfo);
        aiInfo.SetFollowingPlayer(followPlayer);

        Transform target;
        if (followPlayer)
        {
            target = GameManager.Instance.GetPlayerTransform();
        }
        else
        {
            target = GameManager.Instance.GetCurrentObjectiveTransform();
        }
        aiInfo.SetTargetTransform(target);
    }
    #endregion

    #region Public Methods
    //Execute each time action is necessary
    public override void Execute(EnemyAIStandard info, float deltaTime)
    {
        aiInfo = info;
        navMeshAgent = aiInfo.GetNavMeshAgent();
        aiTransform = aiInfo.transform;

        DetermineAction();
    }

    //Return self or the next state to be called
    public override State GetState()
    {
        return this;
    }
    #endregion
}
