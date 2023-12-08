using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button exitGame;
    readonly GameManager gameManager = GameManager.instance;

    void Start()
    {
        play.onClick.AddListener(Play);
        exitGame.onClick.AddListener(ExitGame);
    }
    
    void Play() => gameManager.CallAction(gameManager.OnPlay);
    void ExitGame()=> gameManager.CallAction(gameManager.OnQuitGame);
}
