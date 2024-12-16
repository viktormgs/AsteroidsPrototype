using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private int randomSpeed;
    private Vector2 directionToCenter;
    private Vector3 randomRotation;

    private readonly int minMovementSpeed;
    private readonly int maxMovementSpeed;
    private readonly float minScale;
    private readonly float maxScale;
    
    private bool isSplitEnemy;

    public Enemy(int minMovementSpeed, int maxMovementSpeed, float minScale, float maxScale, int maxLives)
    {
        this.minMovementSpeed = minMovementSpeed;
        this.maxMovementSpeed = maxMovementSpeed;
        this.minScale = minScale;
        this.maxScale = maxScale;
        this.maxScale = maxScale;
        ResetLives(maxLives);
    }


    protected override void Start()
    {
        base.Start();
        float randomScale = Random.Range(minScale, maxScale);
        randomSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        rotateSpeed = Random.Range(0, 500);

        // Allows the enemy to rotate one direction or the other
        int randomOrientation = Random.Range(0, 1) * 2 - 1; 
        randomRotation = new Vector3(0, 0, randomOrientation);
        

        if (isSplitEnemy == true) return;

        SetDirectionAndScale((Vector2.zero - (Vector2)transform.position).normalized, new Vector2(randomScale, randomScale));
    }

    public void SetIsASplitEnemy() => isSplitEnemy = true;
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
        GameplayEvents.InvokeOnEnemyToDestroy(this);
        GameplayEvents.InvokeOnEnemyIsDestroyed();
    }
}
