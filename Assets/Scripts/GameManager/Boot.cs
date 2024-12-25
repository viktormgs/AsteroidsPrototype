using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    private const int bootIndex = 0;
    private const int gameIndex = 1;
        
    private void Awake() => StartCoroutine(LoadGameScene());

    private IEnumerator LoadGameScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(gameIndex, LoadSceneMode.Additive);
        while (!loadOperation.isDone) // Need to wait because I need a different active scene to set
        {
            yield return null;
        }

        // Needed to instantiate stuff into this scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(gameIndex)); 

        SceneManager.UnloadSceneAsync(bootIndex); // Will never use Boot while the game is running
        // No need to break the Coroutine, unloading the scene will stop it
    }
}
