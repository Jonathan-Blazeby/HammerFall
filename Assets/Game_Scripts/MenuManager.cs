using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private TMPro.TMP_Text waveScoreText;
    [SerializeField] private TMPro.TMP_Text defenceTimeText;
    #endregion

    #region Public Fields

    #endregion

    #region Monobehavior Callbacks
    private void Start()
    {
        waveScoreText.text = "Arena Waves Best Score: " + DataManager.Instance.GetWaveScoreString();
        defenceTimeText.text = "Objective Defence Best Time: " + DataManager.Instance.GetDefenceBestTimeString();
    }
    #endregion

    #region Private Methods

    #endregion

    #region Public Methods
    public void LoadTestLevel()
    {
        ScenesManager.Instance.LoadTestLevel();
    }

    public void LoadArenaLevel()
    {
        ScenesManager.Instance.LoadArenaLevel();
    }

    public void LoadCastleLevel()
    {
        ScenesManager.Instance.LoadCastleLevel();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
    #endregion
}
