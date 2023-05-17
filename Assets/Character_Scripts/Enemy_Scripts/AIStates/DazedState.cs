using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazedState : State
{
    #region Private Fields
    private EnemyAIStandard aiInfo;
    private float dazedDelay;

    private float timer = 0;
    #endregion

    #region Public Methods
    //Execute each time action is necessary
    public override void Execute(EnemyAIStandard info, float deltaTime)
    {
        aiInfo = info;
        aiInfo.SetNavMeshOff();

        dazedDelay = aiInfo.GetDazedDelay();
        bool grounded = aiInfo.GetIsGrounded();

        if(timer >= dazedDelay & grounded) //Resets timer and sets state to MoveState
        {
            timer = 0;
            aiInfo.SetCurrentState(aiInfo.GetMoveState());
        }
        else if(timer >= dazedDelay & !grounded) //Restarts timer and continues if it finishes whilst enemy is not Grounded
        {
            timer = 0;
            timer += deltaTime;
        }
        else //Increments the timer
        {
            timer += deltaTime;
        }
    }

    //Return self or the next state to be called
    public override State GetState()
    {
        return this;
    }
    #endregion
}
