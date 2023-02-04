using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    #region Private Fields
    private List<IDamageable> allDamagebles = new List<IDamageable>();
    #endregion

    #region Public Methods
    /// <summary>
    /// If new living characters created, this should be called to add them to the list
    /// </summary>
    /// <param name="damageableComp"></param>
    public void AddNewDamageable(IDamageable damageableComp)
    {
        allDamagebles.Add(damageableComp);
    }

    public void AddPlayerDamageable(PlayerHealth playerHealthComp)
    {
        allDamagebles.Insert(0, playerHealthComp);
    }

    /// <summary>
    /// Called to implement death effect of different character types in the GameManager
    /// </summary>
    /// <param name="characterHealth"></param>
    public void DeathSignal(IDamageable characterHealth)
    {
        if (characterHealth.GetGameObject().CompareTag("Player"))
        {
            GameManager.Instance.PlayerLoss();
        }
        else if(characterHealth.GetGameObject().CompareTag("Enemy"))
        {
            for (int i = 1; i < allDamagebles.Count; i++)
            {
                if (characterHealth == allDamagebles[i])
                {
                    Debug.Log("Enemy Died");
                    GameManager.Instance.DecrementActiveEnemyCount();

                    if (GameManager.Instance.GetActiveEnemyCount() == 0)
                    {
                        GameManager.Instance.WaveComplete();
                    }

                    return;
                }
            }
        }
        else if(characterHealth.GetGameObject().CompareTag("Objective"))
        {
            ObjectiveManager.Instance.ObjectiveDestroyed((ObjectiveHealth)characterHealth);
        }
    }

    public void ResetAllHealth()
    {
        foreach (IDamageable healthComponent in allDamagebles)
        {
            healthComponent.ResetHealth();
        }
    }

    public void ResetEnemyHealth()
    {
        for (int i = 1; i < allDamagebles.Count; i++) //Index starts at 1 as index 0 is always PlayerHealth
        {
            allDamagebles[i].ResetHealth();
        }
    }
    #endregion

}
