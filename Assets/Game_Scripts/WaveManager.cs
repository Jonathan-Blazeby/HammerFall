using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private List<int> waveList;

    [SerializeField] private List<int> easyWaveList;
    [SerializeField] private List<int> mediumWaveList;
    [SerializeField] private List<int> hardWaveList;

    [SerializeField] private int secondsBetweenWaves = 10;
    private int currentWave;
    private int maxWaves;

    //Gets & Sets
    public int GetCurrentWave() => currentWave;
    public int GetMaxWaves() => maxWaves;
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
        maxWaves = waveList.Count;
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

    public void SetWaveDifficulty(int diff)
    {
        switch(diff)
        {
            case 1:
                waveList = easyWaveList;
                break;
            case 2:
                waveList = mediumWaveList;
                break;
            case 3:
                waveList = hardWaveList;
                break;
        }
    }
    #endregion

}
