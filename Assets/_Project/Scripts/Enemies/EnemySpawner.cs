using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    readonly int enemyPoolSize = 3;
    public static readonly Queue<GameObject> enemyQueue = new();
    GameObject inUseEnemy;

    float cameraHalfHeight;
    Vector2 bounds;

    readonly float spawnInterval = 2f; //this should be on a scriptable object

    void Start()
    {
        bounds = ScreenBounds.bounds;
        cameraHalfHeight = ScreenBounds.cameraHalfHeight;

        //Pool Creation
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemyToPool = Instantiate(enemy);
            enemyToPool.SetActive(false);
            enemyQueue.Enqueue(enemyToPool);
        }
        StartCoroutine(Spawner()); //This is the core
    }

    IEnumerator Spawner() //Is Coroutine because of being able to add a delay time between spawns
    {
        while (true)
        {
            Vector2 spawnPos = GetRandomPos();
            //Vector2 toCenter = Vector2.MoveTowards(spawnPos, Vector2.zero, speed * Time.deltaTime);

            inUseEnemy = GetEnemy();
            inUseEnemy.transform.position = spawnPos;
            //inUseEnemy.GetComponent<Rigidbody2D>().velocity = toCenter;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetEnemy()
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
            0 => new Vector2(Random.Range(-bounds.x, bounds.x), cameraHalfHeight),
            //Bottom
            1 => new Vector2(Random.Range(-bounds.x, bounds.x), -cameraHalfHeight),
            //Left
            2 => new Vector2(-bounds.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
            //Right
            _ => new Vector2(bounds.x, Random.Range(-cameraHalfHeight, cameraHalfHeight)),
        };
    }
}
