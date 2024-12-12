using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    const int bootScene = 0,
            gameScene = 1,
            uiScene = 2;

    void Start()
    {
        SceneManager.LoadScene(gameScene);
        SceneManager.UnloadSceneAsync(gameScene);
        SceneManager.LoadScene(uiScene, LoadSceneMode.Additive);
    }
}
