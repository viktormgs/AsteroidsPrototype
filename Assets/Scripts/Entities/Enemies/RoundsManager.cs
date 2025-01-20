using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class RoundsManager : MonoBehaviour
{    
    public static EnemyStats CurrentEnemyLevel { get; private set; } // Review This !!
    [SerializeField] private EnemyStats[] enemyLevels;

    private const float nextRoundTimer = 3f; // Hard coded, consider adding it to the enemylevels as a field
    private Coroutine roundTimer = null;


    private void Start()
    {
        GameManagerEvents.OnStartPlay += Initialize;
        GameManagerEvents.OnGameOver += StopRounds;
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnStartPlay -= Initialize;
        GameManagerEvents.OnGameOver -= StopRounds;
    }

    private void Initialize()
    {
        if (roundTimer != null) StopCoroutine(roundTimer);
        roundTimer = StartCoroutine(RoundHandler());
    }

    private IEnumerator RoundHandler()
    {
        int enemyLevel = 0;
        while (enemyLevel < enemyLevels.Length)
        {
            Debug.Log($"Round {enemyLevel + 1} starts now!");

            CurrentEnemyLevel = enemyLevels[enemyLevel];
            
            yield return new WaitForSeconds(nextRoundTimer);
            enemyLevel++;
        }

        yield break;
    }

    private void StopRounds()
    {
        if (roundTimer != null) StopCoroutine(roundTimer);
        roundTimer = null;
    }
}
