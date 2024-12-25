using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        retryButton.onClick.AddListener(Retry);
        exitButton.onClick.AddListener(Exit);
    }

    private void Retry()
    {
        GameManagerEvents.InvokeOnStartPlay();
    }

    private void Exit()
    {
        GameManagerEvents.InvokeOnExitPlay();
    }

}
