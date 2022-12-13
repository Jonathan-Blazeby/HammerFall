using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<int> waveList;
    [SerializeField] private int secondsBetweenWaves = 10;
    private int currentWave;

    private void Start()
    {
        
    }

    private IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(secondsBetweenWaves);
        BeginNextWave();
    }

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
        if(currentWave == waveList.Count -1) { return true; }
        else { return false; }
    }
}
