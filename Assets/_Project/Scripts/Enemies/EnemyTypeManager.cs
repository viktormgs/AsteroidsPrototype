using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeManager : MonoBehaviour
{
    [SerializeField] EnemyStats[] enemyLvlArray;
    public static EnemyStats currentEnemyLvl;
    int lvlIndex;
    [SerializeField] float timeForNextLevel;
    //Dictionary<string, EnemyStats> enemyDictionary = new();


    void Start()
    {
        StartCoroutine(LevelUpEnemyType());

        //foreach (EnemyStats enemyLvl in enemyTypes)
        //{
        //    enemyDictionary.Add(enemyLvl.name, enemyLvl);
        //    Debug.Log(enemyLvl.name.ToString());
        //}
    }

    IEnumerator LevelUpEnemyType()
    {
        lvlIndex = 0;

        while (lvlIndex < enemyLvlArray.Length)
        {
            CurrentEnemyLvl(lvlIndex);
            yield return new WaitForSeconds(timeForNextLevel);
            lvlIndex++;
        }
    }

    public EnemyStats CurrentEnemyLvl(int lvl)
    {
        return currentEnemyLvl = enemyLvlArray[lvl];
    }

}
