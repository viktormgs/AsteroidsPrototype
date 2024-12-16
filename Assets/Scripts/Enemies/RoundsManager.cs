using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    public static RoundsManager instance; // REMOVE THIS
    
    [SerializeField] private EnemyStats[] enemyLevels;
    public static EnemyStats currentEnemyLevel;

    private const float nextRoundTimer = 7f;
    private Coroutine roundTimer = null;

    private void Start()
    {
        roundTimer = StartCoroutine(RoundHandler());
    }

    private IEnumerator RoundHandler()
    {
        int enemyLevel = 0;
        while (enemyLevel < enemyLevels.Length)
        {
            currentEnemyLevel = enemyLevels[enemyLevel];
            yield return new WaitForSeconds(nextRoundTimer);
            enemyLevel++;

            Debug.Log("Round " + enemyLevel.ToString() + " starts now");
        }
        yield break;
    }

    public void DifficultyReset() // PRIVATE ??
    {
        if (roundTimer != null) StopCoroutine(RoundHandler());
        
        StartCoroutine(RoundHandler());
    }


}
