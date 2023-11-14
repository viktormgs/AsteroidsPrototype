using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    readonly int enemyPoolSize = 20;
    Queue<GameObject> enemyQueue = new();
    GameObject inUseEnemy;
    float speed = 1f;

    float cameraHalfHeight;
    float spawnInterval = 2f;

    void Start()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemyToPool = Instantiate(enemy);
            enemyToPool.SetActive(false);
            enemyQueue.Enqueue(enemyToPool);
        }
        StartCoroutine(Spawner());
    }

    private GameObject GetEnemy()
    {
        if (enemyQueue.Count == 0)
        {
            enemyQueue.Enqueue(enemy);
            return enemy;
        }
        else
        {
            GameObject pooledEnemy = enemyQueue.Dequeue();
            pooledEnemy.SetActive(true);
            return pooledEnemy;
        }
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            Vector2 spawnPos = GetRandomPos();
            Vector2 toCenter = Vector2.MoveTowards(spawnPos, Vector2.zero, speed * Time.deltaTime);

            inUseEnemy = GetEnemy();
            inUseEnemy.transform.position = spawnPos;
            inUseEnemy.GetComponent<Rigidbody2D>().velocity = toCenter;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector2 GetRandomPos()
    {
        int side = Random.Range(0, 4);

        switch (side)
        {
            //top
            case 0: return new Vector2(Random.Range(-ScreenBounds.bounds.x, ScreenBounds.bounds.x), cameraHalfHeight);
            //Bottom
            case 1: return new Vector2(Random.Range(-ScreenBounds.bounds.x, ScreenBounds.bounds.x), -cameraHalfHeight);
            //Left
            case 2: return new Vector2(-ScreenBounds.bounds.x, Random.Range(-cameraHalfHeight, cameraHalfHeight));
            //Right
            default: return new Vector2(ScreenBounds.bounds.x, Random.Range(-cameraHalfHeight, cameraHalfHeight));
        }
    }
}
