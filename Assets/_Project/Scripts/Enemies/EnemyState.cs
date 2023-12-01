using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    Rigidbody2D rb;
    int randomSpeed;
    [HideInInspector] public Vector2 directionToCenter;
    int randomOrientation;
    Vector3 randomRotation = new();
    int rotationSpeed;
    [SerializeField] float minSizeForSplit;
    [HideInInspector] public bool isSplitEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var currentEnemyLvl = EnemyTypeManager.currentEnemyLvl;
        float randomScale = Random.Range(currentEnemyLvl.minScale, currentEnemyLvl.maxScale);
        randomSpeed = Random.Range(currentEnemyLvl.minSpeed, currentEnemyLvl.maxSpeed);
        randomOrientation = Random.Range(0, 1) * 2 - 1; //Allows the enemy to rotate one direction or the other
        rotationSpeed = Random.Range(0, 500);
        randomRotation = new Vector3(0, 0, randomOrientation);
        
        if (isSplitEnemy == false)
        {
            SetScaleAndDirection((Vector2.zero - (Vector2)transform.position).normalized, new Vector2(randomScale, randomScale));
        }
    }

    public void SetScaleAndDirection(Vector2 direction, Vector2 scale)
    {
        directionToCenter = direction;
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    void Movement()=> rb.velocity = randomSpeed * directionToCenter;

    void Rotation() => transform.Rotate(randomRotation, rotationSpeed * Time.deltaTime);

    public void DestroyEnemy()
    {
        //check for size of enemy
        if (transform.localScale.x >= minSizeForSplit)
        {
            gameObject.SetActive(false);
            EnemySpawner.enemyQueue.Enqueue(gameObject);
            EnemySpawner.instance.SplitEnemies(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.localScale, directionToCenter);
        }

        else
        {
            gameObject.SetActive(false);
            EnemySpawner.enemyQueue.Enqueue(gameObject);
        }
    }
}
