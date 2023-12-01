using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public GameObject[] screensArray;
    [HideInInspector] public GameObject mainMenuScreen, pauseScreen, gameOverScreen;
    GameObject currentScreen;

    public void ShowScreen(GameObject screen)
    {
        if(currentScreen != null) currentScreen.SetActive(false);

        currentScreen = screen;
        currentScreen.SetActive(true);
    }

    public void HideScreen(GameObject screen)
    {
        if (currentScreen == screen) currentScreen.SetActive(false);
        currentScreen = null;
    }

    public bool IsScreenShown(GameObject screen)
    {
        return screen == currentScreen;
    }

}
