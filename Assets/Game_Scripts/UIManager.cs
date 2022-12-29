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
    private List<GameObject> uiObjectList = new List<GameObject>();
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        uiObjectList.Add(victoryText.gameObject);
        uiObjectList.Add(lossText.gameObject);
        uiObjectList.Add(waveNumberText.gameObject);
        uiObjectList.Add(waveCountdownText.gameObject);
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
    #endregion

    #region Public Methods
    public void ResetUI()
    {
        foreach (GameObject uiObject in uiObjectList)
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

    public void SetWaveCount(int waveCount, int totalWaves)
    {
        waveNumberText.text = "Wave: " + waveCount + "/" + totalWaves;
    }

    public void StartWaveCountdown(int waveDelaySecs)
    {
        waveCountdownText.gameObject.SetActive(true);
        StartCoroutine(WaveCountdownTimer(waveDelaySecs));
    }
    #endregion

}
