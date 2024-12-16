using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Add Logic between is on menu vs is playing for buttons
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
        //OnPlay += PlayerManager.instance.ResetPosition;
        OnPlay += RoundsManager.instance.DifficultyReset;
        OnPlay += NewGameEvent;
        //add enemyspawner resume?
        OnGoToMainMenu += MainMenuScreen;
        OnGoToMainMenu += EnemySpawner.instance.ResetSpawner;

        OnGameOver += GameOverScreen;
        OnGameOver += EnemySpawner.instance.ResetSpawner;

        OnQuitGame += QuitGame;

        //Starts the game on the main Menu
        CallAction(OnGoToMainMenu);
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

    void UIHandlerWhenOnMainMenu(Transform parent, bool hide)
    {
        if(hide == true)
        {
            foreach (Transform childGameObject in parent)
            {
                childGameObject.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform childGameObject in parent)
            {
                childGameObject.gameObject.SetActive(true);
            }
        }
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

    void MainMenuScreen()
    {
        UIHandlerWhenOnMainMenu(ScoreManager.instance.gameObject.transform, true);
        UIHandlerWhenOnMainMenu(LifeManager.instance.gameObject.transform, true);
        ShowScreen(mainMenuScreen, true);
    }

    void NewGameEvent()
    {
        if (gameOverScreen.activeSelf) HideScreen(gameOverScreen, false);
        else if (mainMenuScreen.activeSelf)
        {
            UIHandlerWhenOnMainMenu(ScoreManager.instance.gameObject.transform, false);
            UIHandlerWhenOnMainMenu(LifeManager.instance.gameObject.transform, false);
            HideScreen(mainMenuScreen, false);
        }
    }
}
