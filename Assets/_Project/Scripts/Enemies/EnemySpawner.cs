using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject gameObjectEnemy;
    public static GameObject enemy;
    readonly int enemyPoolSize = 3;
    public static readonly Queue<GameObject> enemyQueue = new();
    GameObject inUseEnemy;

    float cameraHalfHeight;
    Vector2 screenLimit;

    void Start()
    {
        enemy = gameObjectEnemy;
        screenLimit = ScreenBounds.screenLimit;
        cameraHalfHeight = ScreenBounds.cameraHalfHeight;

        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemyToPool = Instantiate(enemy);
            enemyToPool.SetActive(false);
            enemyQueue.Enqueue(enemyToPool);
        }
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner() //Is Coroutine because can use spawnInterval as time delay
    {
        while (true)
        {
            Vector2 spawnPos = GetRandomPos();

            inUseEnemy = GetEnemy();
            inUseEnemy.transform.position = spawnPos;

            yield return new WaitForSeconds(EnemyTypeManager.currentEnemyLvl.spawnInterval);
        }
    }

    public static GameObject GetEnemy()
    {
        if (enemyQueue.Count == 0)
        {
            GameObject newEnemyToPool = Instantiate(enemy);
            newEnemyToPool.SetActive(false);
            enemyQueue.Enqueue(newEnemyToPool);
        }

        GameObject pooledEnemy = enemyQueue.Dequeue();
        pooledEnemy.SetActive(true);
        return pooledEnemy;
    }

    private Vector2 GetRandomPos()
    {
        int side = Random.Range(0, 4);

        return side switch
        {
            //top
            0 => new Vector2(Random.Range(-screenLimit.x, screenLimit.x), cameraHalfHeight),
            //Bottom
            1 => new Vector2(Random.Range(-screenLimit.x, screenLimit.x), -cameraHalfHeight),
            //Left
            2 => new Vector2(-screenLimit.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
            //Right
            _ => new Vector2(screenLimit.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
        };
    }

    public static void SplitEnemies(Vector2 pos)
    {
        var splitEnemy = GetEnemy();

        for (int i = 0; i < 2; i++)
        {
            var smallEnemy = Instantiate(splitEnemy);

            Vector2 direction = i == 0 ? Vector2.left : Vector2.right;
            smallEnemy.transform.position = pos;
            smallEnemy.GetComponent<Rigidbody2D>().velocity = direction;
        }
    }


}
