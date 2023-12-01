using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    GameObject[] prefabsToInstantiate;
    GameObject[] managersToInstantiate;
    Scene[] scenesToInstantiate;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject manager in managersToInstantiate)
        {
            InstantiateManager(manager, UIRoot.instance.gameObject.transform);
        }
        foreach (GameObject prefab in prefabsToInstantiate)
        {
            InstantiateManager(prefab, null);
        }
        //foreach (Scene scene in scenesToInstantiate)
        //{
        //    //Initialize scenes
        //}
    }

    GameObject InstantiateManager(GameObject gameObject, Transform parent)
    {
        var instantiatedGameObject = Instantiate(gameObject, parent);
        if(instantiatedGameObject.activeSelf == false) instantiatedGameObject.SetActive(true);
        return instantiatedGameObject;
    }
}
