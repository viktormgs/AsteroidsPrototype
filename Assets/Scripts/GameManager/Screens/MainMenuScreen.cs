using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitGameButton;
    // THINK ABOUT CREATING AN ARROW SELECTOR TOO

    private void Start()
    {
        playButton.onClick.AddListener(Play);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void Play()
    {
        GameManagerEvents.InvokeOnStartPlay();
        Debug.Log("Play Button Clicked");
    }

    private void ExitGame()
    {
        GameManagerEvents.InvokeOnExitGame();
    }

}
