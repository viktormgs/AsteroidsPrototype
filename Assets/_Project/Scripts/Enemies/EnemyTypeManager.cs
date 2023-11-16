using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeManager : MonoBehaviour
{
    [SerializeField] EnemyStats[] enemyLvlArray;
    //Dictionary<string, EnemyStats> enemyDictionary = new();

    //void Start()
    //{
    //    foreach (EnemyStats enemyLvl in enemyTypes)
    //    {
    //        enemyDictionary.Add(enemyLvl.name, enemyLvl);
    //        Debug.Log(enemyLvl.name.ToString());
    //    }
    //}




    public ScriptableObject CurrentEnemyLvl(int lvl)
    {
        return enemyLvlArray[lvl];
    }

}
