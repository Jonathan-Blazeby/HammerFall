using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    private List<IDamageable> allDamagebles = new List<IDamageable>();
    private int currentEnemyCount;

    //If new living characters created, this should be called to add them to the list
    public void AddNewDamageable(IDamageable damageableComp)
    {
        allDamagebles.Add(damageableComp);
    }

    public void AddPlayerDamageable(PlayerHealth playerHealthComp)
    {
        allDamagebles.Insert(0, playerHealthComp);
    }

    public void DeathSignal(IDamageable characterHealth)
    {
        if (characterHealth == allDamagebles[0])
        {
            GameManager.Instance.PlayerLoss();
        }
        else
        {
            for (int i = 1; i < allDamagebles.Count; i++)
            {
                if (characterHealth == allDamagebles[i])
                {
                    GameManager.Instance.SetActiveEnemyCount(GameManager.Instance.GetActiveEnemyCount() - 1);

                    if (GameManager.Instance.GetActiveEnemyCount() == 0) 
                    { 
                        GameManager.Instance.WaveComplete(); 
                    }

                    return;
                }
            }
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
}
