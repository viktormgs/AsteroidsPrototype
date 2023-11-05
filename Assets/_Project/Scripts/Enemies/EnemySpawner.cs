using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    int enemyPoolSize = 20;
    Queue<GameObject> enemyQueue = new();

    void Start()
    {
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemyToPool = Instantiate(enemy);
            enemyToPool.SetActive(false);
            enemyQueue.Enqueue(enemyToPool);
        }


    }

}
