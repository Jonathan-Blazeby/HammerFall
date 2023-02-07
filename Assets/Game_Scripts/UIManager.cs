using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private TMPro.TMP_Text victoryText;
    [SerializeField] private TMPro.TMP_Text lossText;
    [SerializeField] private TMPro.TMP_Text waveNumberText;
    [SerializeField] private TMPro.TMP_Text waveCountdownText;
    [SerializeField] private TMPro.TMP_Text defenceTimer;
    private List<GameObject> uiObjectList = new List<GameObject>();

    private float startTime;

    private bool playerLoss = false;

    private GameModes gameMode;
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }

    private void Update()
    {
        if (gameMode == GameModes.Objectives && !playerLoss)
        {
            DefenceTimerUpdate();
        }       
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        gameMode = GameManager.Instance.GetGameMode();

        uiObjectList.Add(victoryText.gameObject);
        uiObjectList.Add(lossText.gameObject);

        if(gameMode == GameModes.Waves)
        {
            waveNumberText.gameObject.SetActive(true);
            uiObjectList.Add(waveNumberText.gameObject);
            uiObjectList.Add(waveCountdownText.gameObject);
        }

        else if(gameMode == GameModes.Objectives)
        {
            defenceTimer.gameObject.SetActive(true);
            uiObjectList.Add(defenceTimer.gameObject);
            startTime = Time.time;
        }

    }

    private IEnumerator WaveCountdownTimer(int waveDelaySecs)
    {
        while (waveDelaySecs > 0)
        {
            waveDelaySecs--;
            waveCountdownText.text = waveDelaySecs.ToString();
            yield return new WaitForSeconds(1);
        }
        waveCountdownText.gameObject.SetActive(false);
    }

    private void DefenceTimerUpdate()
    {
        float time = Time.time - startTime;

        string minutes = ((int)time / 60).ToString();
        string seconds = (time % 60).ToString("f0");

        defenceTimer.text = minutes + ":" + seconds;
    }
    #endregion

    #region Public Methods
    public void ResetUI()
    {
        playerLoss = false;
        foreach (GameObject uiObject in uiObjectList)
        {
            uiObject.SetActive(false);
        }

        if(gameMode == GameModes.Waves)
        {
            waveNumberText.gameObject.SetActive(true);
        }
        else if(gameMode == GameModes.Objectives)
        {
            startTime = Time.time;
            defenceTimer.gameObject.SetActive(true);
        }
    }

    public void PlayerVictory()
    {
        victoryText.gameObject.SetActive(true);
    }

    public void PlayerLoss()
    {
        playerLoss = true;
        lossText.gameObject.SetActive(true);
    }

    public void SetWaveCount(int waveCount, int totalWaves)
    {
        waveNumberText.text = "Wave: " + waveCount + "/" + totalWaves;
    }

    public void StartWaveCountdown(int waveDelaySecs)
    {
        waveCountdownText.gameObject.SetActive(true);
        StartCoroutine(WaveCountdownTimer(waveDelaySecs));
    }

    public void StopDefenceTimer()
    {

    }
    #endregion

}
