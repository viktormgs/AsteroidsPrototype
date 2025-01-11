using System;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private ScreensCollection screens;

    public static GameObject UIRoot {  get; private set; }
    private static GameObject mainMenuScreen;
    private static GameObject gameOverScreen;
    private static GameObject pauseScreen;
    private static GameObject hud;


    private static GameObject currentScreen = null;
    private static GameObject CurrentScreen
    {
        get => currentScreen;
        set
        {
            // This is like that because there's no scenario where you can pause the game if there's another screen running
            if ((value == pauseScreen && currentScreen != null)) return;

            currentScreen = value;
        }
    }

    private void Awake() // It's on Awake because it needs to be initialized for the game manager to use the Main Menu 
    {
        Initialize();

        GameManagerEvents.OnGameStart += ShowMainMenuScreen;
        GameManagerEvents.OnExitPlay += ShowMainMenuScreen;
        GameManagerEvents.OnExitPlay += DisableHUD;
        GameManagerEvents.OnGameOver += ShowGameOverScreen;
        GameManagerEvents.OnPause += ShowPauseScreen;
        GameManagerEvents.OnResume += ExitCurrentScreen;
        GameManagerEvents.OnStartPlay += ExitCurrentScreen;
        GameManagerEvents.OnStartPlay += ShowHUD;
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnGameStart -= ShowMainMenuScreen;
        GameManagerEvents.OnExitPlay -= ShowMainMenuScreen;
        GameManagerEvents.OnExitPlay -= DisableHUD;
        GameManagerEvents.OnGameOver -= ShowGameOverScreen;
        GameManagerEvents.OnPause -= ShowPauseScreen;
        GameManagerEvents.OnResume -= ExitCurrentScreen;
        GameManagerEvents.OnStartPlay -= ExitCurrentScreen;
        GameManagerEvents.OnStartPlay -= ShowHUD;
    }


    private void Initialize()
    {
        UIRoot = Instantiate(screens.uIRoot);

        mainMenuScreen = InstantiateScreen(screens.mainMenuScreen);
        gameOverScreen = InstantiateScreen(screens.gameOverScreen);
        pauseScreen = InstantiateScreen(screens.pauseScreen);
        hud = InstantiateScreen(screens.hud);
    }

    private void ShowMainMenuScreen() => Show(mainMenuScreen);
    private void ShowGameOverScreen() => Show(gameOverScreen);
    private void ShowPauseScreen() => Show(pauseScreen);
    private void ShowHUD() => hud.SetActive(true);
    private void DisableHUD() => hud.SetActive(false);

    private void ExitCurrentScreen() => Hide(CurrentScreen);


    public static bool CanPauseOrResumeGame()
    {
        if (CurrentScreen != null && CurrentScreen != pauseScreen) return false;
        else return true;
    }

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
        var obj = Instantiate(screen, UIRoot.transform);
        obj.SetActive(false);
        return obj;
    }
}
