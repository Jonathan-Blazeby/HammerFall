using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GravityManager gravityManager;
    [SerializeField] private CharactersManager charactersManager;

    [SerializeField] private Transform playerTransform;

    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;
    [SerializeField] private float yDeathHeight = -5.0f;
    [SerializeField] private int activeEnemyCount;

    public static GameManager Instance;

    public UIManager GetUIManager() => uIManager;
    public SpawnManager GetSpawnManager() => spawnManager;
    public WaveManager GetWaveManager() => waveManager;
    public GravityManager GetGravityManager() => gravityManager;
    public CharactersManager GetCharactersManager() => charactersManager;
    public Transform GetPlayerTransform() => playerTransform;
    public int GetActiveEnemyCount() => activeEnemyCount;
    public void SetActiveEnemyCount(int waveSpawnedEnemies) 
    { 
        activeEnemyCount = waveSpawnedEnemies;
    }

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
        if (Input.GetKey(KeyCode.R))
        {
            ResetGame();
        }

        if(playerTransform.position.y < yDeathHeight)
        {
            ResetPlayer();
        }
    }

    private void Initialise()
    {
        playerStartPosition = playerTransform.position;
        playerStartRotation = playerTransform.rotation;

        waveManager.FirstWave();
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

    private void ResetAllHealth()
    {
        charactersManager.ResetAllHealth();
    }

    private void ResetUI()
    {
        uIManager.ResetUI();
    }

    public void WaveComplete()
    {
        if(waveManager.FinalWaveCompleteCheck())
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
    }

    public void PlayerLoss()
    {
        uIManager.PlayerLoss();
    }

}