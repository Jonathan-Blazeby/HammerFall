using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text victoryText;
    [SerializeField] private TMPro.TMP_Text lossText;
    [SerializeField] private Transform playerTransform;
    private List<IDamageable> allDamagebles = new List<IDamageable>();
    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;
    [SerializeField] private float yDeathHeight = -5.0f;
    private int currentEnemyCount;

    public static GameManager Instance;

    public Transform GetPlayerTransform() => playerTransform;

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

        currentEnemyCount = 0;
    }

    public void ResetGame()
    {
        ResetPlayer();
        ResetHealth();
        ResetUI();
    }

    public void ResetPlayer()
    {
        playerTransform.position = playerStartPosition;
        playerTransform.rotation = playerStartRotation;
        playerTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void ResetHealth()
    {
        foreach(IDamageable healthComponent in allDamagebles)
        {
            healthComponent.ResetHealth();
        }
    }

    private void ResetUI()
    {
        victoryText.enabled = false;
        lossText.enabled = false;
    }

    private void PlayerVictory()
    {
        victoryText.enabled = true;
    }

    private void PlayerLoss()
    {
        lossText.enabled = true;
    }

    //If new living characters created, this should be called to add them to the list
    public void AddNewDamageable(IDamageable damageableComp)
    {
        allDamagebles.Add(damageableComp);
        currentEnemyCount++;
    }

    public void AddPlayerDamageable(PlayerHealth playerHealthComp)
    {
        allDamagebles.Insert(0, playerHealthComp);
    }

    public void DeathSignal(IDamageable characterHealth)
    {
        if(characterHealth == allDamagebles[0])
        {
            PlayerLoss();
        }
        else
        {
            for (int i = 1; i < allDamagebles.Count; i++)
            {
                if (characterHealth == allDamagebles[i])
                {
                    currentEnemyCount--;

                    if (currentEnemyCount <= 0) { PlayerVictory(); }

                    return;
                }
            }
        }
    }
}
