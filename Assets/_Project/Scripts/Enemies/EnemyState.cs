using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    Rigidbody2D rb;
    int randomSpeed;
    Vector2 directionToCenter;
    int randomOrientation;
    Vector3 randomRotation = new();
    int rotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var currentEnemyLvl = EnemyTypeManager.currentEnemyLvl;
        float randomScale = Random.Range(currentEnemyLvl.minScale, currentEnemyLvl.maxScale);
        randomSpeed = Random.Range(currentEnemyLvl.minSpeed, currentEnemyLvl.maxSpeed);
        randomOrientation = Random.Range(0, 1) * 2 - 1; //Allows the enemy to rotate one direction or the other
        rotationSpeed = Random.Range(0, 500);

        randomRotation = new Vector3(0, 0, randomOrientation);
        transform.localScale = new Vector2(randomScale, randomScale);
        directionToCenter = (Vector2.zero - (Vector2)transform.position).normalized;
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    void Movement()=> rb.velocity = randomSpeed * Time.deltaTime * directionToCenter;

    void Rotation() => transform.Rotate(randomRotation, rotationSpeed * Time.deltaTime);

    public void DestroyEnemy()
    {

        if (gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            EnemySpawner.enemyQueue.Enqueue(gameObject);

            //Code for children enemies
        }
        else
        {
            gameObject.SetActive(false);
            EnemySpawner.enemyQueue.Enqueue(gameObject);
        }


    }
}
