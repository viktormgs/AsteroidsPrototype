using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public GameObject[] screensArray;
    [HideInInspector] public GameObject mainMenuScreen, pauseScreen, gameOverScreen;
    GameObject currentScreen;

    public void ShowScreen(GameObject screen, bool pause)
    {
        if(currentScreen != null) currentScreen.SetActive(false);

        PauseHandler(pause);
        currentScreen = screen;
        currentScreen.SetActive(true);
    }

    public void HideScreen(GameObject screen, bool pause)
    {
        if (currentScreen == screen) currentScreen.SetActive(false);
        currentScreen = null;
        PauseHandler(pause);
    }

    public bool IsScreenShown(GameObject screen)
    {
        return screen == currentScreen;
    }

    public void PauseHandler(bool pause)
    {
        if (pause) Time.timeScale = 0;
        else { Time.timeScale = 1; }
    }
}
