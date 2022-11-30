using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text victoryText;
    [SerializeField] private TMPro.TMP_Text lossText;
    [SerializeField] private GameObject playerCharacter;
    private List<IDamageable> allDamagebles = new List<IDamageable>();
    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;
    [SerializeField] private float yDeathHeight = -5.0f;
    private int currentEnemyCount;

    public static GameManager Instance;

    private void Start()
    {
        Instance = this;

        playerStartPosition = playerCharacter.transform.position;
        playerStartRotation = playerCharacter.transform.rotation;

        currentEnemyCount = 0;

        allDamagebles.Add(playerCharacter.GetComponent<PlayerHealth>()); //Start the list with the PlayerHealth
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>(); //Add all enemyHealth componants to the list
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is IDamageable && allScripts[i].transform.root.tag == "Enemy")
            {
                allDamagebles.Add(allScripts[i] as IDamageable);
                currentEnemyCount++;
            }   
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetGame();
        }

        if(playerCharacter.transform.position.y < yDeathHeight)
        {
            ResetPlayer();
        }
    }

    private void ResetGame()
    {
        ResetPlayer();
        ResetHealth();
        ResetUI();
    }

    private void ResetPlayer()
    {
        playerCharacter.transform.position = playerStartPosition;
        playerCharacter.transform.rotation = playerStartRotation;
        playerCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
