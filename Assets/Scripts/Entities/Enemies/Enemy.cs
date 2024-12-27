using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Vector2 directionToCenter;
    private Vector3 randomRotation;

    private int minMovementSpeed;
    private int maxMovementSpeed;
    private float minScale;
    private float maxScale;
    
    private bool isSplitEnemy;


    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        float randomScale = Random.Range(minScale, maxScale);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        rotateSpeed = Random.Range(0, 500);

        // Allows the enemy to rotate one direction or the other
        int randomOrientation = Random.Range(0, 1) * 2 - 1;
        randomRotation = new Vector3(0, 0, randomOrientation);

        // The enemy spawner will set the direction and scale based on
        // the parent that was destroyed, that's why it stops at this point
        if (isSplitEnemy == true) 
        {
            TryGetComponent(out SpriteRenderer sprite);
            sprite.color = Color.red;
            return;
        }

        SetDirectionAndScale((Vector2.zero - (Vector2)transform.position).normalized, new Vector2(randomScale, randomScale));
    }


    public void AssignStats(int minMovementSpeed, int maxMovementSpeed, float minScale, float maxScale, int maxLives)
    {
        this.minMovementSpeed = minMovementSpeed;
        this.maxMovementSpeed = maxMovementSpeed;
        this.minScale = minScale;
        this.maxScale = maxScale;
        this.maxScale = maxScale;
        ResetLives(maxLives);
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

    protected override void Movement() => rb.velocity = movementSpeed * directionToCenter;
    private void Rotation() => transform.Rotate(randomRotation, rotateSpeed * Time.deltaTime);

    public override void TakeDamage()
    {
        base.TakeDamage();
        if (HasMoreLives()) return;

        DestroyEntity();
    }

    protected override void DestroyEntity()
    {
        isSplitEnemy = false;
        GameplayEvents.InvokeEnemyToDestroy(this);
        GameplayEvents.InvokeEnemyIsDestroyed();
    }
}
