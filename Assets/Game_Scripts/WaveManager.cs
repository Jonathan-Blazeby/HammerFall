using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<int> waveList;
    [SerializeField] private float timeBetweenWaves = 10.0f;
    private int currentWave;

    private void Start()
    {
        
    }

    private IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        BeginNextWave();
    }

    public void BeginNextWave()
    {
        currentWave++;
        GameManager.Instance.GetSpawnManager().SpawnWave(waveList[currentWave]);
    }

    public void FirstWave()
    {
        GameManager.Instance.GetSpawnManager().SpawnWave(waveList[0]);
    }

    public void NextWave()
    {
        StartCoroutine(WaveTimer());
    }

    public bool FinalWaveCompleteCheck()
    {
        if(currentWave == waveList.Count -1) { return true; }
        else { return false; }
    }
}
