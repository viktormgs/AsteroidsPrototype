using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeManager : MonoBehaviour
{
    public static EnemyTypeManager instance;
    [SerializeField] EnemyStats[] enemyLvlArray;
    public static EnemyStats currentEnemyLvl;
    [SerializeField] float timeForNextLevel;


    void Start() => StartCoroutine(LevelUpEnemyType());

    IEnumerator LevelUpEnemyType()
    {
        int lvlIndex = 0;
        while (lvlIndex < enemyLvlArray.Length)
        {
            CurrentEnemyLvl(lvlIndex);
            yield return new WaitForSeconds(timeForNextLevel);
            lvlIndex++;
            Debug.Log("Level" + lvlIndex.ToString() + "starts now");
        }
        yield break;
    }

    EnemyStats CurrentEnemyLvl(int lvl)
    {
        return currentEnemyLvl = enemyLvlArray[lvl];
    }

    public void DifficultyReset()
    {
        StopCoroutine(LevelUpEnemyType());
        StartCoroutine(LevelUpEnemyType());
    }


}
