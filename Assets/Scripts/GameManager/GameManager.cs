using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Add Logic between is on menu vs is playing for buttons
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player playerObject; 
    public static Player Player { get; private set; } = null;

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);

        GameManagerEvents.OnStartPlay += InitializeGameplay;
        GameManagerEvents.OnExitGame += QuitGame;

        InitializeGameStart();
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnStartPlay -= InitializeGameplay;
        GameManagerEvents.OnExitGame -= QuitGame;
    }


    // Review this so it covers exit and start at main menu
    private void InitializeGameStart() => GameManagerEvents.InvokeOnGameStart();

    private void InitializeGameplay()
    {
        if (Player == null) Player = Instantiate(playerObject);
        if(!Player.gameObject.activeSelf) Player.gameObject.SetActive(true);
    }

    private void QuitGame() => Application.Quit();
}
