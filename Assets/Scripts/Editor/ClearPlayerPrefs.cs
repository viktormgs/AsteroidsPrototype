using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : EditorWindow
{
    [MenuItem("Asteroid/Clear PlayerPrefs (All)")]
    static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}
