using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameHandler : MonoBehaviour
{
    private void Start()
    {
        GameManagerEvents.OnPause += PauseGame;
        GameManagerEvents.OnResume += ResumeGame;

    }

    private void OnDestroy()
    {
        GameManagerEvents.OnPause -= PauseGame;
        GameManagerEvents.OnResume -= ResumeGame;
    }

    private void PauseGame() => Time.timeScale = 0;
    private void ResumeGame() => Time.timeScale = 1;
}
