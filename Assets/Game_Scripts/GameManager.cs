using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameModes
{
    Waves, //0
    Objectives //1
}

public class GameManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private UIManager uIManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GravityManager gravityManager;
    [SerializeField] private CharactersManager charactersManager;
    [SerializeField] private ObjectiveManager objectiveManager;
    
    [SerializeField] private Transform playerTransform;

    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;
    [SerializeField] private float yDeathHeight = -5.0f;

    [SerializeField] private int activeEnemyCount;
    [SerializeField] private GameModes gameMode;

    private bool playerLoss = false;

    private float startTime;
    private float gameTime;

    //Gets & Sets
    public UIManager GetUIManager() => uIManager;
    public SpawnManager GetSpawnManager() => spawnManager;
    public WaveManager GetWaveManager() => waveManager;
    public GravityManager GetGravityManager() => gravityManager;
    public CharactersManager GetCharactersManager() => charactersManager;
    public ObjectiveManager GetObjectiveManager() => objectiveManager;
    public Transform GetPlayerTransform() => playerTransform;
    public Transform GetCurrentObjectiveTransform() => objectiveManager.GetCurrentObjectiveTransform();
    public int GetActiveEnemyCount() => activeEnemyCount;
    public void SetActiveEnemyCount(int waveSpawnedEnemies) => activeEnemyCount = waveSpawnedEnemies;
    public GameModes GetGameMode() => gameMode;

    public float GetGameTime() => gameTime;
    #endregion

    #region Public Fields
    public static GameManager Instance;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialise();
    }

    private void Update()
    {
        UpdateTime();

        if (playerTransform.position.y < yDeathHeight)
        {
            ResetPlayer();
        }
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        playerStartPosition = playerTransform.position;
        playerStartRotation = playerTransform.rotation;

        startTime = Time.time;

        if (gameMode == GameModes.Waves)
        {
            waveManager.FirstWave();
        }
        else if(gameMode == GameModes.Objectives)
        {
            objectiveManager.SetupGameMode();
            spawnManager.SpawnWave(6);
        }        
    }

    private void ResetAllHealth()
    {
        charactersManager.ResetAllHealth();
    }

    private void ResetUI()
    {
        uIManager.ResetUI();
    }

    private void WaveScore()
    {
        int waveScore = waveManager.GetCurrentWave();
        int maxWaves = waveManager.GetMaxWaves();
        DataManager.Instance.SetWaveScore(waveScore, maxWaves);
    }

    private void UpdateTime()
    {
        gameTime = Time.time - startTime;
    }

    private void SaveScores()
    {
        DataManager.Instance.SaveScore();
    }
    #endregion

    #region Public Methods


    public void DecrementActiveEnemyCount()
    {
        activeEnemyCount--;

        if(gameMode == GameModes.Objectives)
        {
            spawnManager.SpawnWave(activeEnemyCount + 2);
        }
    }

    public void ResetGame()
    {
        ResetPlayer();
        ResetAllHealth();
        ResetUI();
    }

    public void ResetPlayer()
    {
        playerTransform.position = playerStartPosition;
        playerTransform.rotation = playerStartRotation;
        playerTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }



    public void WaveComplete()
    {
        if (waveManager.FinalWaveCompleteCheck())
        {
            PlayerVictory();
        }
        else
        {
            waveManager.NextWave();
        }
    }

    public void PlayerVictory()
    {
        uIManager.PlayerVictory();
        if (gameMode == GameModes.Waves)
        {
            WaveScore();
        }
        SaveScores();
    }

    public void PlayerLoss()
    {
        playerLoss = true;
        uIManager.PlayerLoss();
        if(gameMode == GameModes.Waves)
        {
            WaveScore();
        }
        else
        {
            DataManager.Instance.SetGameTime(gameTime);
            
        }
        SaveScores();
    }

    #endregion

}
