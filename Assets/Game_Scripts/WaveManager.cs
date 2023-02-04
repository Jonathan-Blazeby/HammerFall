using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private List<int> waveList;
    [SerializeField] private int secondsBetweenWaves = 10;
    private int currentWave;
    #endregion

    #region Public Fields
    public static WaveManager Instance;
    #endregion

    #region Monobehavior Callbacks 
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Private Methods
    private IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(secondsBetweenWaves);
        BeginNextWave();
    }
    #endregion

    #region Public Methods
    public void BeginNextWave()
    {
        currentWave++;
        GameManager.Instance.GetUIManager().SetWaveCount(currentWave + 1, waveList.Count);
        GameManager.Instance.GetSpawnManager().SpawnWave(waveList[currentWave]);
    }

    public void FirstWave()
    {
        GameManager.Instance.GetUIManager().SetWaveCount(1, waveList.Count);
        GameManager.Instance.GetSpawnManager().SpawnWave(waveList[0]);
    }

    public void NextWave()
    {
        StartCoroutine(WaveTimer());
        GameManager.Instance.GetUIManager().StartWaveCountdown(secondsBetweenWaves);
    }

    public bool FinalWaveCompleteCheck()
    {
        if (currentWave == waveList.Count - 1) { return true; }
        else { return false; }
    }
    #endregion

}
