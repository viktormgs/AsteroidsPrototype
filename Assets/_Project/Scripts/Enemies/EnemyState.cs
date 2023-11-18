using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    int randomSpeed;
    int randomOrientation;
    Vector3 randomRotation = new();
    int rotationSpeed;
    Rigidbody2D rb;

    Vector2 directionToCenter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var currentEnemyLvl = EnemyTypeManager.currentEnemyLvl;

        //Speed for Movement
        randomSpeed = Random.Range(currentEnemyLvl.minSpeed, currentEnemyLvl.maxSpeed);

        //Gives -1 or 1
        randomOrientation = Random.Range(0, 1) * 2 - 1; 
        randomRotation = new Vector3(0, 0, randomOrientation);
        rotationSpeed = Random.Range(0, 500);
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
        gameObject.SetActive(false);
        EnemySpawner.enemyQueue.Enqueue(gameObject);
    }
}
