using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : ScreensManager
{
    public static GameManager instance;
    public event Action OnGameOver;
    public event Action OnReset;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        mainMenuScreen = InstantiateScreen(screensArray[0]);
        pauseScreen = InstantiateScreen(screensArray[1]);
        gameOverScreen = InstantiateScreen(screensArray[2]);

        //OnGameOver += ScoreManager.instance.CheckForNewRecord;
        OnGameOver += LifeManager.instance.ResetLives;
        OnGameOver += GameOverScreen;

        MenuInput.instance.OnEscapePressed += PauseGameHandler;

        OnReset += LifeManager.instance.ResetLives;
        OnReset += ScoreManager.instance.ResetScore;
    }

    GameObject InstantiateScreen(GameObject gameObject)
    {
        var instantiatedGameObject = Instantiate(gameObject, UIRoot.instance.gameObject.transform);
        instantiatedGameObject.SetActive(false);
        return instantiatedGameObject;
    }

    public void Reset()
    {
        OnReset?.Invoke();
    }

    public void LostAllLivesEvent()
    {
        OnGameOver?.Invoke();
    }


    // Screens handling from this point

    public void PauseGameHandler()
    {
        if (!IsScreenShown(pauseScreen))
        {
            Time.timeScale = 0;
            ShowScreen(pauseScreen);
        }
        else
        {
            Time.timeScale = 1;
            HideScreen(pauseScreen);
        }
    }

    public void GameOverScreen()
    {
        Time.timeScale = 0;
        ShowScreen(gameOverScreen);
    }

}
