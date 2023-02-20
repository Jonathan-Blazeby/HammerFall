using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackboard
{
    #region Private Fields
    private static List<EnemyAIStandard> enemiesFollowingPlayer = new List<EnemyAIStandard>();
    [SerializeField] private static int maxEnemiesFollowingPlayer = 3;
    #endregion

    #region Private Methods

    #endregion

    #region Public Methods
    /// <summary>
    /// Enemy Requests blackboard for permission to follow player.
    /// Will return false if enought enemies already are.
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public static bool WantToFollowPlayer(EnemyAIStandard enemy)
    {
        if(enemiesFollowingPlayer.Contains(enemy)) { return true; }

        if(enemiesFollowingPlayer.Count < maxEnemiesFollowingPlayer)
        {
            enemiesFollowingPlayer.Add(enemy);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes enemy AI from list of those following the player
    /// </summary>
    /// <param name="enemy"></param>
    public static void StopFollowPlayer(EnemyAIStandard enemy)
    {
        enemiesFollowingPlayer.Remove(enemy);
    }
    #endregion
}
