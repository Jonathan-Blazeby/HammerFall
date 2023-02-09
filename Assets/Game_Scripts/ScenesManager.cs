using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    #region Public Fields
    public static ScenesManager Instance;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        Initialise();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "StartMenu_Scene")
            {
                Application.Quit();
            }
            else
            {
                LoadMainMenu();
            }

        }

        if (Input.GetKey(KeyCode.R))
        {
            ResetScene();
        }
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
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
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    #endregion

}
