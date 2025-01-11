using UnityEngine;

[CreateAssetMenu(fileName = "ScreensCollection", menuName = "Custom/Create Screens Collection")]
public class ScreensCollection : ScriptableObject
{
    public GameObject uIRoot;
    public GameObject mainMenuScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject hud;
}
