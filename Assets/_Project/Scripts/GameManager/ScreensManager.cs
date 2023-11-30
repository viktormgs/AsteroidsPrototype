using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public static ScreensManager instance;

    public GameObject pause;
    public GameObject mainMenu;
    public GameObject gameOver;
    GameObject currentScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ShowScreen(GameObject screen)
    {
        if(currentScreen != null) currentScreen.SetActive(false);

        currentScreen = screen;
        currentScreen.SetActive(true);
    }

    public void HideScreen(GameObject screen)
    {
        if (currentScreen == screen) currentScreen.SetActive(false);
    }


}
