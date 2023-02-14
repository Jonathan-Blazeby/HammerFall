using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackboard : MonoBehaviour
{
    #region Private Fields
    private List<EnemyAIBasic> enemiesFollowingPlayer = new List<EnemyAIBasic>();
    [SerializeField] private int maxEnemiesFollowingPlayer = 3;
    #endregion

    #region Public Fields
    public static EnemyBlackboard Instance;
    #endregion

    #region Monobehavior Callbacks
    void Start()
    {
        Instance = this;
    }
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
    public bool WantToFollowPlayer(EnemyAIBasic enemy)
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
    public void StopFollowPlayer(EnemyAIBasic enemy)
    {
        enemiesFollowingPlayer.Remove(enemy);
    }
    #endregion
}
