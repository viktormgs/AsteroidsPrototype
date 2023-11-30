using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action OnGameOver;
    public event Action OnReset;
    public event Action OnPause;
    public event Action OnResume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

    }

    void Start()
    {
        //OnGameOver += ScoreManager.instance.CheckForNewRecord;
        OnGameOver += LifeManager.instance.ResetLives;
        OnGameOver += GameOverScreen;
        //add ui screens to the events

        OnReset += LifeManager.instance.ResetLives;
        OnReset += ScoreManager.instance.ResetScore;

    }

    public void Reset()
    {
        OnReset?.Invoke();
    }

    public void LostAllLives()
    {
        OnGameOver?.Invoke();
    }


    // Screens handling from this point

    void PauseGameScreen()
    {
        Time.timeScale = 0;
        ScreensManager.instance.ShowScreen(ScreensManager.instance.pause);
    }

    void ResumeGameScreen()
    {
        Time.timeScale = 1;
        ScreensManager.instance.HideScreen(ScreensManager.instance.pause);
    }

    void GameOverScreen()
    {
        ScreensManager.instance.ShowScreen(ScreensManager.instance.gameOver);
    }

}
