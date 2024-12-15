using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] private GameObject gameObjectEnemy;
    public static GameObject enemy;
    const int enemyPoolSize = 7;
    private readonly Queue<GameObject> enemyQueue = new();
    private List<GameObject> activeEnemyList = new();
    private GameObject inUseEnemy;

    float cameraHalfHeight;
    Vector2 screenLimit;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        enemy = gameObjectEnemy;
        screenLimit = ScreenBounds.screenLimit;
        cameraHalfHeight = ScreenBounds.cameraHalfHeight;

        GameplayEvents.OnEnemyToDestroy += DisableEnemy;

        for (int i = 0; i < enemyPoolSize; i++)
        {
            CreateEnemy();
        }
        StartCoroutine(Spawner());
    }

    private void CreateEnemy()
    {
        GameObject enemyToPool = Instantiate(enemy, gameObject.transform);
        enemyToPool.SetActive(false);
        enemyQueue.Enqueue(enemyToPool);
    }

    private IEnumerator Spawner() //It controls the spawnInterval as time delay
    {
        while (true)
        {
            Vector2 spawnPos = GetRandomPos();

            inUseEnemy = GetEnemy();
            inUseEnemy.transform.position = spawnPos;

            yield return new WaitForSeconds(EnemyTypeManager.currentEnemyLvl.spawnInterval);
        }
    }

    private GameObject GetEnemy()
    {
        if (enemyQueue.Count == 0)
        {
            CreateEnemy();
        }

        GameObject pooledEnemy = enemyQueue.Dequeue();
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
            0 => new Vector2(Random.Range(-screenLimit.x, screenLimit.x), cameraHalfHeight),
            // Bottom
            1 => new Vector2(Random.Range(-screenLimit.x, screenLimit.x), -cameraHalfHeight),
            // Left
            2 => new Vector2(-screenLimit.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
            // Right
            _ => new Vector2(screenLimit.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
        };
    }

    private void DisableEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyQueue.Enqueue(enemy.gameObject);
        activeEnemyList.Remove(enemy.gameObject);
    }

    public void SplitIntoEnemies(Vector2 pos, Vector2 scale, Vector2 direction)
    {
        int amountOfEnemies = Random.Range(2, 5); // Flexible number of enemies (e.g., 2 to 4)
        float angleStep = 360f / amountOfEnemies; // Angle between split enemies

        for (int i = 0; i < amountOfEnemies; i++)
        {
            var smallEnemy = GetEnemy();
            smallEnemy.transform.position = pos;
            smallEnemy.transform.localScale = scale * .5f;

            smallEnemy.TryGetComponent(out Enemy enemyState);
            enemyState.isSplitEnemy = true;

            // Calculate direction for each enemy
            float angle = angleStep * i; // Angle for this enemy
            Vector2 enemyDirection = RotateVector(direction, angle);
            enemyState.SetDirectionAndScale(enemyDirection, smallEnemy.transform.localScale);
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
            vector.x * sin + vector.y * cos
        );
    }


    public void ResetSpawner() // MAKE PRIVATE !!
    {
        foreach (GameObject activeEnemy in activeEnemyList)
        {
                activeEnemy.SetActive(false);
                enemyQueue.Enqueue(activeEnemy);
        }
        activeEnemyList.Clear();
    }
}
