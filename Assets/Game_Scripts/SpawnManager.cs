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

    private GameModes gameMode;
    #endregion

    #region Public Fields
    public static SpawnManager Instance;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameMode = GameManager.Instance.GetGameMode();
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Coroutine that spawns, or resets existing (but dead) enemies, while decrementing the numberToSpawn value toward zero.
    /// Will break if fewer enemies than exist are needed. Will yield to allow timeslicing to instantiate larger numbers over multiple frames.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnObject()
    {
        if (liveObjects.Count > 0)
        {
            foreach (var obj in liveObjects)
            {
                IDamageable healthComponent = obj.GetComponent<IDamageable>();

                //If still alive, do not reset (used for objective mode regular spawning)
                if(healthComponent.Living()) 
                {
                    numberToSpawn--;
                    continue; 
                }

                if(!obj.activeInHierarchy)
                {
                    //Move inactive but instantiated objects to spawns
                    obj.transform.position = spawnPoints[nextSpawn].position;
                    nextSpawn++;
                    if (nextSpawn == spawnPoints.Count)
                    {
                        nextSpawn = 0;
                    }

                    //Enable them again
                    obj.SetActive(true);
                    numberToSpawn--;

                    //Reset their health
                    healthComponent.ResetHealth();

                    if (numberToSpawn == 0)
                    {
                        yield break;
                    }
                }
            }
        }
        //If more to spawn after health resets
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
    /// <summary>
    /// Sets number of enemies to appear in wave, then begins spawning coroutine. Finally, updates GameManager with ActiveEnemyCount
    /// </summary>
    /// <param name="numInWave"></param>
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
