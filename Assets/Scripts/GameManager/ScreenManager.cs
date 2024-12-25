using System;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private ScreensCollection screens;

    private GameObject uIRoot;
    private GameObject mainMenuScreen;
    private GameObject gameOverScreen;
    private GameObject pauseScreen;

    private GameObject currentScreen = null;

    private GameObject CurrentScreen
    {
        get => currentScreen;
        set
        {
            // This is like that because there's no scenario where you can pause the game if there's a pop-up running
            if ((value == pauseScreen && currentScreen != null)) return;

            currentScreen = value;
        }
    }

    private void Awake() // It's on Awake because it needs to be initialized for the game manager to use the Main Menu 
    {
        Initialize();

        GameManagerEvents.OnGameStart += ShowMainMenuScreen;
        GameManagerEvents.OnExitPlay += ShowMainMenuScreen;
        GameManagerEvents.OnGameOver += ShowGameOverScreen;
        GameManagerEvents.OnPause += ShowPauseScreen;
        GameManagerEvents.OnResume += ExitCurrentScreen;
        GameManagerEvents.OnStartPlay += ExitCurrentScreen;
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnGameStart -= ShowMainMenuScreen;
        GameManagerEvents.OnExitPlay -= ShowMainMenuScreen;
        GameManagerEvents.OnGameOver -= ShowGameOverScreen;
        GameManagerEvents.OnPause -= ShowPauseScreen;
        GameManagerEvents.OnResume -= ExitCurrentScreen;
        GameManagerEvents.OnStartPlay -= ExitCurrentScreen;
    }


    private void Initialize()
    {
        uIRoot = Instantiate(screens.uIRoot);

        mainMenuScreen = InstantiateScreen(screens.mainMenuScreen);
        gameOverScreen = InstantiateScreen(screens.gameOverScreen);
        pauseScreen = InstantiateScreen(screens.pauseScreen);
    }


    private void ShowMainMenuScreen() => Show(mainMenuScreen);
    private void ShowGameOverScreen() => Show(gameOverScreen);
    private void ShowPauseScreen() => Show(pauseScreen);
    private void ExitCurrentScreen() => Hide(CurrentScreen);


    private void Show(GameObject screen)
    {
        if (CurrentScreen != null) Hide(CurrentScreen);

        screen.SetActive(true);
        CurrentScreen = screen;
    }

    private void Hide(GameObject screen)
    {
        screen.SetActive(false);
        currentScreen = null;

    }

    private GameObject InstantiateScreen(GameObject screen)
    {
        var obj = Instantiate(screen, uIRoot.transform);
        obj.SetActive(false);
        return obj;
    }
}
