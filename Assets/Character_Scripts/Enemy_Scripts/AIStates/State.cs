using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    #region Public Methods
    //Execute each time action is necessary
    public abstract void Execute(EnemyAIStandard info, float deltaTime);
    
    //Return self or the next state to be called
    public abstract State GetState();
    #endregion
}
