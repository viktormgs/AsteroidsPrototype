using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] GameObject gameObjectEnemy;
    public static GameObject enemy;
    const int enemyPoolSize = 3;
    public readonly Queue<GameObject> enemyQueue = new();
    public List<GameObject> activeEnemyList = new();
    GameObject inUseEnemy;

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

    void Start()
    {
        enemy = gameObjectEnemy;
        screenLimit = ScreenBounds.screenLimit;
        cameraHalfHeight = ScreenBounds.cameraHalfHeight;

        for (int i = 0; i < enemyPoolSize; i++)
        {
            EnemyInstantiator();
        }
        StartCoroutine(Spawner());
    }

    void EnemyInstantiator()
    {
        GameObject enemyToPool = Instantiate(enemy, gameObject.transform);
        enemyToPool.SetActive(false);
        enemyQueue.Enqueue(enemyToPool);
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

    GameObject GetEnemy()
    {
        if (enemyQueue.Count == 0)
        {
            EnemyInstantiator();
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

    public void SplitEnemies(Vector2 pos, Quaternion rotation, Vector2 scale, Vector2 direction)
    {
        for (int i = 0; i < 2; i++)
        {
            var smallEnemy = GetEnemy();
            smallEnemy.transform.SetPositionAndRotation(pos, rotation);
            smallEnemy.transform.localScale = scale * .5f;

            var enemyState = smallEnemy.GetComponent<EnemyState>();
            enemyState.isSplitEnemy = true;

            var enemyDirectionToCenter = i == 0 ? Vector2.Perpendicular(direction) : -Vector2.Perpendicular(direction);
            enemyState.SetScaleAndDirection(enemyDirectionToCenter, smallEnemy.transform.localScale);
        }
    }

    public void EnemySpawnerReset()
    {
        foreach (GameObject activeEnemy in activeEnemyList)
        {
                activeEnemy.SetActive(false);
                enemyQueue.Enqueue(activeEnemy);
        }
        activeEnemyList.Clear();
    }
}
