using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Private Fields
    private int waveBestScoreInt;
    private int waveBestMaxInt;
    private string waveBestScoreString;
    private float defenceBestTimeFloat;
    private string defenceBestTimeString;

    private bool timerActive = false;

    //Gets & Sets
    public string GetWaveScoreString() => waveBestScoreString;

    public void SetWaveScore(int waveReached, int maxWaves)
    {
        if(waveReached >= waveBestScoreInt)
        {
            waveBestScoreInt = waveReached;
            waveBestMaxInt = maxWaves;
            waveBestScoreString = WaveScoreToString(waveReached, maxWaves);
        }
    }

    public string GetDefenceBestTimeString() => defenceBestTimeString;
    public void SetGameTime(float gameTime)
    {
        if(gameTime > defenceBestTimeFloat)
        {
            defenceBestTimeFloat = gameTime;
            defenceBestTimeString = GameTimerToString(gameTime);
        }
    }
    #endregion

    #region Public Fields
    public static DataManager Instance;
    #endregion

    #region Monobehavior Callbacks
    private void Awake()
    {
        Initialise();
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

        ReadScore();
    }
    #endregion

    #region Public Methods
    public void ReadScore()
    {
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/score.txt", false);
        
        waveBestScoreString = reader.ReadLine();
        WaveScoreFromString(waveBestScoreString);

        defenceBestTimeString = reader.ReadLine();
        GameTimerFromString(defenceBestTimeString);

        reader.Close();
    }

    public void SaveScore()
    {
        StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/score.txt", false);
        writer.WriteLine(waveBestScoreString);
        writer.WriteLine(defenceBestTimeString);
        writer.Close();
    }

    //Convert wave score to string, formatted to 'Wave: Wave-Reached/Max Waves'
    public string WaveScoreToString(int waveReached, int maxWaves)
    {
        string formattedScore = "Wave: " + waveReached + "/" + maxWaves;
        return formattedScore;
    }

    //Convert wave score string to individual ints
    public void WaveScoreFromString(string waveScoreString)
    {
        string[] sArray = waveScoreString.Split('/');
        waveBestScoreInt = int.Parse(sArray[0]);
        waveBestMaxInt = int.Parse(sArray[1]);
    }

    //Convert GameTime to a string, formatted to 'Min : Sec'
    public string GameTimerToString(float gameTime)
    {
        string minutes = ((int)gameTime / 60).ToString();
        string seconds = (gameTime % 60).ToString("f0");

        string formattedTime = minutes + ":" + seconds;
        return formattedTime;
    }

    //Convert time string (in Mins:Secs format) to seconds float 
    public void GameTimerFromString(string gameTimeString)
    {
        string[] sArray = gameTimeString.Split(':');
        int mins = int.Parse(sArray[0]);
        int secs = int.Parse(sArray[1]);

        float time = mins * 60;
        time += secs;

        defenceBestTimeFloat = int.Parse(sArray[0]);
        waveBestMaxInt = int.Parse(sArray[1]);
    }
    #endregion
}
