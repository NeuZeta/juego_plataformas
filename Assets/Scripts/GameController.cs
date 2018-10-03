using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    private const string HIGHSCORE_LEVEL1 = "Highscore Level 1";
    private const string HIGHSCORE_LEVEL2 = "Highscore Level 2";
    private const string HIGHSCORE_LEVEL3 = "Highscore Level 3";
    private const string LEVEL2 = "Level2";
    private const string LEVEL3 = "Level3";

    private void Awake()
    {
        MakeSingleton();
        IsGameStartedForTheFirstTime();
    }

    private void IsGameStartedForTheFirstTime()
    {
        //PlayerPrefs.DeleteKey("IsTheGameStartedForTheFirstTime");

        if (!PlayerPrefs.HasKey("IsTheGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetFloat(HIGHSCORE_LEVEL1, 0f);
            PlayerPrefs.SetFloat(HIGHSCORE_LEVEL2, 0f);
            PlayerPrefs.SetFloat(HIGHSCORE_LEVEL3, 0f);
            PlayerPrefs.SetInt(LEVEL2, 0);
            PlayerPrefs.SetInt(LEVEL3, 0);
            PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 0);
        }
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void SetHighscoreLevel1(float score)
    {
        PlayerPrefs.SetFloat(HIGHSCORE_LEVEL1, score);
    }

    public float GetHighscoreLevel1()
    {
        return PlayerPrefs.GetFloat(HIGHSCORE_LEVEL1);
    }

    public void SetHighscoreLevel2(float score)
    {
        PlayerPrefs.SetFloat(HIGHSCORE_LEVEL2, score);
    }

    public float GetHighscoreLevel2()
    {
        return PlayerPrefs.GetFloat(HIGHSCORE_LEVEL2);
    }

    public void SetHighscoreLevel3(float score)
    {
        PlayerPrefs.SetFloat(HIGHSCORE_LEVEL3, score);
    }

    public float GetHighscoreLevel3()
    {
        return PlayerPrefs.GetFloat(HIGHSCORE_LEVEL3);
    }

    public void UnlockedLevel2()
    {
        PlayerPrefs.SetInt(LEVEL2, 1);
    }

    public int IsLevel2Unlocked()
    {
        return PlayerPrefs.GetInt(LEVEL2);
    }

    public void UnlockedLevel3()
    {
        PlayerPrefs.SetInt(LEVEL3, 1);
    }

    public int IsLevel3Unlocked()
    {
        return PlayerPrefs.GetInt(LEVEL3);
    }

}//GameController
