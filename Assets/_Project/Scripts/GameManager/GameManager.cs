using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : ScreensManager
{
    public static GameManager instance;
    public Action OnGameOver;
    public Action OnPlay;
    public Action OnGoToMainMenu;
    public Action OnQuitGame;

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


        //Event assigning

        MenuInput.instance.OnEscapePressed += PauseScreenHandler;

        OnPlay += LifeManager.instance.ResetLives;
        OnPlay += ScoreManager.instance.ResetIngameScore;
        OnPlay += PlayerManager.instance.ResetPosition;
        OnPlay += EnemyTypeManager.instance.DifficultyReset;
        OnPlay += NewGameEvent;
        //add enemyspawner resume?
        OnGoToMainMenu += MainMenuScreen;
        OnGoToMainMenu += EnemySpawner.instance.EnemySpawnerReset;

        OnGameOver += GameOverScreen;
        OnGameOver += EnemySpawner.instance.EnemySpawnerReset;

        OnQuitGame += QuitGame;
    }

    GameObject InstantiateScreen(GameObject gameObject)
    {
        var instantiatedGameObject = Instantiate(gameObject, UIRoot.instance.gameObject.transform);
        instantiatedGameObject.SetActive(false);
        return instantiatedGameObject;
    }

    public void CallAction(Action eventToCall)
    {
        eventToCall?.Invoke();
    }

    void QuitGame()
    {
        Application.Quit();
    }


    // Screens handling from this point

    void PauseScreenHandler()
    {
        if (gameOverScreen.activeSelf || mainMenuScreen.activeSelf) return;
        if (!IsScreenShown(pauseScreen)) 
            ShowScreen(pauseScreen, true);
        else { HideScreen(pauseScreen, false); }
    }

    void GameOverScreen() => ShowScreen(gameOverScreen, true);
    void MainMenuScreen() => ShowScreen(mainMenuScreen, true);

    void NewGameEvent()
    {
        if (gameOverScreen.activeSelf) HideScreen(gameOverScreen, false);
        else if (mainMenuScreen.activeSelf) HideScreen(mainMenuScreen, false);
    }
}
