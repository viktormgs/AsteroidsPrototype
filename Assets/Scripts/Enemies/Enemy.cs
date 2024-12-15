using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private int randomSpeed;
    private Vector2 directionToCenter;
    private int randomOrientation;
    private Vector3 randomRotation;
    [SerializeField] private float minSizeForSplit;
    [HideInInspector] public bool isSplitEnemy;
    readonly EnemySpawner enemySpawner = EnemySpawner.instance;
    [SerializeField] private ParticleSystem destroyFX;

    public Enemy(int minMovementSpeed, int maxMovementSpeed, float minScale, float maxScale, int maxLives)
    {
        // TO DO
        ResetLives(maxLives);
    }

    protected override void Start()
    {
        base.Start();
        var currentEnemyLvl = EnemyTypeManager.currentEnemyLvl;
        float randomScale = Random.Range(currentEnemyLvl.minScale, currentEnemyLvl.maxScale);
        randomSpeed = Random.Range(currentEnemyLvl.minMovementSpeed, currentEnemyLvl.maxMovementSpeed);
        randomOrientation = Random.Range(0, 1) * 2 - 1; //Allows the enemy to rotate one direction or the other
        rotateSpeed = Random.Range(0, 500);
        randomRotation = new Vector3(0, 0, randomOrientation);
        
        if (isSplitEnemy == false)
        {
            SetDirectionAndScale((Vector2.zero - (Vector2)transform.position).normalized, new Vector2(randomScale, randomScale));
        }
    }

    public void SetDirectionAndScale(Vector2 direction, Vector2 scale)
    {
        directionToCenter = direction;
        transform.localScale = scale;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Rotation();
    }

    protected override void Movement() => rb.velocity = randomSpeed * directionToCenter;
    private void Rotation() => transform.Rotate(randomRotation, rotateSpeed * Time.deltaTime);

    protected override void DestroyEntity()
    {
        // How big the enemy is will determine how many times will it be split
        if (transform.localScale.x >= minSizeForSplit)
        {
            // Think about how to improve this !!!
            enemySpawner.SplitIntoEnemies(gameObject.transform.position, gameObject.transform.localScale, directionToCenter);
        }
        GameplayEvents.InvokeOnEnemyToDestroy(this);
        GameplayEvents.InvokeOnEnemyIsDestroyed();
    }
}
