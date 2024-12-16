using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    private readonly GameManager gameManager = GameManager.instance;

    void Start()
    {
        retryButton.onClick.AddListener(Retry);
        exitButton.onClick.AddListener(Exit);
    }

    void Retry() => gameManager.CallAction(gameManager.OnPlay);
    void Exit() => gameManager.CallAction(gameManager.OnGoToMainMenu);
}
