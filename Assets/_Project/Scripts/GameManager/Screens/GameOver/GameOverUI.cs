using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button retry;
    [SerializeField] Button exit;
    readonly GameManager gameManager = GameManager.instance;

    void Start()
    {
        retry.onClick.AddListener(Retry);
        exit.onClick.AddListener(Exit);
    }

    void Retry() => gameManager.CallAction(gameManager.OnPlay);
    void Exit() => gameManager.CallAction(gameManager.OnGoToMainMenu);
}
