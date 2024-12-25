using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class RoundsManager : MonoBehaviour
{    
    public static EnemyStats CurrentEnemyLevel { get; private set; } // Review This !!
    [SerializeField] private EnemyStats[] enemyLevels;

    private const float nextRoundTimer = 7f; // Hard coded, consider adding it to the enemylevels as a field
    private Coroutine roundTimer = null;


    private void Start()
    {
        GameManagerEvents.OnStartPlay += ResetRounds;
        GameManagerEvents.OnStartPlay += Initialize;
        GameManagerEvents.OnGameOver += StopRounds;
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnStartPlay -= ResetRounds;
        GameManagerEvents.OnStartPlay -= Initialize;
        GameManagerEvents.OnGameOver -= StopRounds;
    }

    private void Initialize() => roundTimer = StartCoroutine(RoundHandler());

    private void StopRounds()
    {
        if (roundTimer != null) StopCoroutine(roundTimer);
        roundTimer = null;
    }

    private IEnumerator RoundHandler()
    {
        int enemyLevel = 0;
        while (enemyLevel < enemyLevels.Length)
        {
            CurrentEnemyLevel = enemyLevels[enemyLevel];
            yield return new WaitForSeconds(nextRoundTimer);
            enemyLevel++;

            Debug.Log("Round " + enemyLevel.ToString() + " starts now");
        }
        yield break;
    }

    private void ResetRounds()
    {
        if (roundTimer != null) StopCoroutine(roundTimer);

        roundTimer = StartCoroutine(RoundHandler());
    }
}
