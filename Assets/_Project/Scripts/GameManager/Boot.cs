using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    //[SerializeField] GameObject[] prefabsToInstantiate;
    //[SerializeField] GameObject[] managersToInstantiate;
    //[SerializeField] string[] scenesToInstantiate;

    void Start()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    //void InstantiateManager(GameObject gameObject, Transform parent)
    //{
    //    var instantiatedGameObject = Instantiate(gameObject, parent);
    //    if(instantiatedGameObject.activeSelf == false) instantiatedGameObject.SetActive(true);
    //}
}
