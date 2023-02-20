using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    #region Public Methods
    //Execute each time action is necessary
    public override void Execute(EnemyAIStandard info, float deltaTime)
    {
        return;
    }

    //Return self or the next state to be called
    public override State GetState()
    {
        return this;
    }
    #endregion
}
