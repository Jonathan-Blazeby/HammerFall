using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private TMPro.TMP_Text victoryText;
    [SerializeField] private TMPro.TMP_Text lossText;
    private List<GameObject> uiObjectList = new List<GameObject>();

    private void Start()
    {
        uiObjectList.Add(victoryText.gameObject);
        uiObjectList.Add(lossText.gameObject);
    }

    public void ResetUI()
    {
        foreach(GameObject uiObject in uiObjectList)
        {
            uiObject.SetActive(false);
        }
    }

    public void PlayerVictory()
    {
        victoryText.gameObject.SetActive(true);
    }

    public void PlayerLoss()
    {
        lossText.gameObject.SetActive(true);
    }

    public void SetWaveCount(int waveCount)
    {

    }

    public void SetEnemiesRemaining(int liveEnemies)
    {

    }

    public void StartWaveCountdown()
    {

    }


}
