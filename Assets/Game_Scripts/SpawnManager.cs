using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private GameObject prefab;
    private List<GameObject> liveObjects = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();
    private int nextSpawn = 0;
    [SerializeField] private float maxNumberToSpawnPerFrame = 25;
    private int numberToSpawn;
    #endregion

    #region Public Fields
    public static SpawnManager Instance;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Private Methods
    private IEnumerator SpawnObject()
    {
        if (liveObjects.Count > 0)
        {
            foreach (var obj in liveObjects)
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
                numberToSpawn--;
                if (numberToSpawn == 0)
                {
                    //Reset their health
                    GameManager.Instance.GetCharactersManager().ResetEnemyHealth();
                    yield break;
                }
            }
            //Reset their health
            GameManager.Instance.GetCharactersManager().ResetEnemyHealth();
        }

        while (numberToSpawn > 0)
        {
            for (int i = 0; i < maxNumberToSpawnPerFrame; i++)
            {
                liveObjects.Add(Instantiate(prefab, spawnPoints[nextSpawn].position, Quaternion.identity));
                numberToSpawn--;
                if (numberToSpawn == 0)
                {
                    yield break;
                }

                nextSpawn++;
                if (nextSpawn == spawnPoints.Count)
                {
                    nextSpawn = 0;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion

    #region Public Methods
    public void SpawnWave(int numInWave)
    {
        numberToSpawn = numInWave;
        StartCoroutine(SpawnObject());
        GameManager.Instance.SetActiveEnemyCount(numInWave);
    }

    public void RegisterSpawnPoints(Transform spawn)
    {
        spawnPoints.Add(spawn);
    }
    #endregion

}
