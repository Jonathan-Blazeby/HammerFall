using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    #region Public Methods
    //Execute each time action is necessary
    public override void Execute(EnemyAIStandard info, float deltaTime)
    {
        info.SetNavMeshOff();
        info.GetRigidbody().constraints = RigidbodyConstraints.FreezeRotation;
    }

    //Return self or the next state to be called
    public override State GetState()
    {
        return this;
    }
    #endregion
}
