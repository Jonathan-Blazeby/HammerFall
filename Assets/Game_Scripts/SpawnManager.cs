using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private List<GameObject> liveObjects = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();
    private int nextSpawn = 0;
    [SerializeField] private float maxNumberToSpawnPerFrame = 25;
    private int numberToSpawn;

    public static SpawnManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator SpawnObject()
    {
        if(liveObjects.Count > 0)
        {
            foreach(var obj in liveObjects)
            {
                //Move already living objects to spawns
                obj.transform.position = spawnPoints[nextSpawn].position;
                nextSpawn++;
                if (nextSpawn == spawnPoints.Count)
                {
                    nextSpawn = 0;
                }

                //Enable them again
                obj.SetActive(true);
            }
            //Reset their health
            GameManager.Instance.GetCharactersManager().ResetEnemyHealth();
        }

        while(liveObjects.Count < numberToSpawn)
        {       
            for (int i = 0; i < maxNumberToSpawnPerFrame; i++)
            {
                liveObjects.Add(Instantiate(prefab, spawnPoints[nextSpawn].position, Quaternion.identity));

                if(liveObjects.Count == numberToSpawn)
                {
                    yield break;
                }

                nextSpawn++;
                if(nextSpawn == spawnPoints.Count) 
                { 
                    nextSpawn = 0; 
                }
            }
            
            yield return new WaitForEndOfFrame();            
        }        
    }

    public void SpawnWave(int numInWave)
    {
        numberToSpawn = numInWave;
        StartCoroutine(SpawnObject());
        GameManager.Instance.SetActiveEnemyCount(liveObjects.Count);
    }

    public void RegisterSpawnPoints(Transform spawn)
    {
        spawnPoints.Add(spawn);
    }
}
