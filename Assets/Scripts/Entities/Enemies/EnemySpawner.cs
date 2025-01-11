using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private const float minSizeForSplit = 3.2f;

    private const int enemyPoolSize = 7;
    private readonly Queue<GameObject> enemyQueue = new();
    private readonly List<GameObject> activeEnemyList = new();

    private float cameraHalfHeight;
    private Vector2 screenLimits = Vector2.zero;
    private Coroutine spawner = null;


    private void Start()
    {
        // Assigning these two here to keep the code more readable
        screenLimits = ScreenBorders.ScreenLimits; 
        cameraHalfHeight = ScreenBorders.CameraHalfHeight;

        GameplayEvents.OnEnemyToDestroy += DisableEnemy;
        GameplayEvents.OnEnemyToDestroy += Split;
        GameManagerEvents.OnStartPlay += Initialize;
        GameManagerEvents.OnExitGame += ResetSpawner;
        GameManagerEvents.OnGameOver += ResetSpawner;

    }

    private void OnDestroy()
    {
        GameplayEvents.OnEnemyToDestroy -= DisableEnemy;
        GameplayEvents.OnEnemyToDestroy = Split;
        GameManagerEvents.OnStartPlay -= Initialize;
        GameManagerEvents.OnExitGame -= ResetSpawner;
        GameManagerEvents.OnGameOver -= ResetSpawner;

    }

    private void Initialize()
    {
        if (spawner != null) StopCoroutine(spawner);

        for (int i = 0; i < enemyPoolSize; i++)
        {
            CreateEnemy();
        }
        spawner = StartCoroutine(Spawner());
    }

    // Controls the enemy spawn rate given by the current difficulty
    private IEnumerator Spawner() 
    {
        while (true)
        {
            Vector2 spawnPos = GetRandomPos();

            GameObject enemyInUse = GetEnemy();
            enemyInUse.transform.position = spawnPos;

            yield return new WaitForSeconds(RoundsManager.CurrentEnemyLevel.spawnInterval);
        }
    }

    private void CreateEnemy()
    {
        GameObject enemyToPool = Instantiate(enemy.gameObject, gameObject.transform);
        enemyToPool.SetActive(false);
        enemyQueue.Enqueue(enemyToPool);
    }

    private GameObject GetEnemy()
    {
        if (enemyQueue.Count == 0) CreateEnemy();

        GameObject pooledEnemy = enemyQueue.Dequeue();
       
        pooledEnemy.TryGetComponent(out Enemy enemy);

        AssignStats(enemy);

        pooledEnemy.SetActive(true);
        activeEnemyList.Add(pooledEnemy);

        return pooledEnemy;
    }

    private Vector2 GetRandomPos()
    {
        int side = Random.Range(0, 4);

        return side switch
        {
            // Top
            0 => new Vector2(Random.Range(-screenLimits.x, screenLimits.x), cameraHalfHeight),
            // Bottom
            1 => new Vector2(Random.Range(-screenLimits.x, screenLimits.x), -cameraHalfHeight),
            // Left
            2 => new Vector2(-screenLimits.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
            // Right
            _ => new Vector2(screenLimits.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
        };
    }


    private void AssignStats(Enemy enemy)
    {
        enemy.AssignStats(
            RoundsManager.CurrentEnemyLevel.minMovementSpeed, RoundsManager.CurrentEnemyLevel.maxMovementSpeed, // Speed 
            RoundsManager.CurrentEnemyLevel.minScale, RoundsManager.CurrentEnemyLevel.maxScale, // Scale
            RoundsManager.CurrentEnemyLevel.maxLives); // Lives
    }


    private void DisableEnemy(Enemy enemy)
    {
        StartCoroutine(CanBeDisabled());
        IEnumerator CanBeDisabled()
        {
            while (enemy.FXPlaying != null) yield return null;

            enemy.gameObject.SetActive(false);
            enemyQueue.Enqueue(enemy.gameObject);
            activeEnemyList.Remove(enemy.gameObject);
            yield break;
        }
    }

    private void Split(Enemy enemy)
    {
        // How big the enemy is will determine how many times will it be split
        if (enemy.transform.localScale.x < minSizeForSplit) return;


        int amountOfEnemies = Random.Range(2, 4); // Flexible number of enemy children (e.g., 2 to 4)
        float angleStep = 360f / amountOfEnemies; // Angle between split enemies

        for (int i = 0; i < amountOfEnemies; i++)
        {
            var smallEnemy = GetEnemy();

            smallEnemy.TryGetComponent(out Enemy enemyState);

            enemyState.SetIsASplitEnemy();

            // This forces initialization again to correctly setup child attributes
            enemyState.gameObject.SetActive(false);
            enemyState.gameObject.SetActive(true);


            enemyState.SetPosition(enemy.transform.position);

            // Calculate direction for each enemy
            float angle = angleStep * i; // Angle for this enemy

            // Assigning random values because this will create an offset on the angle of the direction,
            // making it feel more natural, otherwise it will be a cross pattern every time it splits
            angle = Random.Range(angle / 1.7f, angle * 1.7f); 

            Vector2 enemyDirection = RotateVector(Vector2.up, angle);
            enemyState.SetDirectionAndScale(enemyDirection, new Vector3(
                // Child asteroids are half the size of the parent
                enemy.transform.localScale.x * .5f, enemy.transform.localScale.y * .5f, 1));
        }
    }

    // Helper function to rotate a vector by a given angle (in degrees)
    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radians = angle * Mathf.Deg2Rad; // Convert degrees to radians
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        // Apply rotation matrix
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos);
    }

    private void ResetSpawner()
    {
        StopCoroutine(spawner);
        foreach (GameObject activeEnemy in activeEnemyList)
        {
                activeEnemy.SetActive(false);
                enemyQueue.Enqueue(activeEnemy);
        }
        activeEnemyList.Clear();
    }
}
