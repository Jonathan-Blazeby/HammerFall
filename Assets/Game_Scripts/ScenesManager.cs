using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    #region MonoBehaviour Callbacks
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #endregion

    #region Public Methods
    public void LoadTestLevel()
    {
        SceneManager.LoadScene("Test_Scene");
    }

    public void LoadArenaLevel()
    {
        SceneManager.LoadScene("LargeArena_Scene");
    }

    public void LoadCastleLevel()
    {
        SceneManager.LoadScene("ObjectiveCastle_Scene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu_Scene");
    }
    #endregion

}
