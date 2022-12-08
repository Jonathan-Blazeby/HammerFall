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
    [SerializeField] private int numberToSpawn = 4;

    public static SpawnManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        if(liveObjects.Count > 0)
        {
            foreach(var obj in liveObjects)
            {
                //Move obj to place, reset its health, etc
            }
        }

        numberToSpawn -= liveObjects.Count;

        while(liveObjects.Count < numberToSpawn)
        {       
            for (int i = 0; i < maxNumberToSpawnPerFrame; i++)
            {
                liveObjects.Add(Instantiate(prefab, spawnPoints[nextSpawn].transform.position, Quaternion.identity));

                if(liveObjects.Count >= numberToSpawn)
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

    public void RegisterSpawnPoints(Transform spawn)
    {
        spawnPoints.Add(spawn);
    }
}
