using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : Entity
{
    [SerializeField] private BoxCollider2D boxCollider;

    private Vector2 directionToCenter;
    private float randomRotation;

    private int minMovementSpeed;
    private int maxMovementSpeed;
    private float minScale;
    private float maxScale;
    
    private bool isSplitEnemy;


    protected override void Initialize()
    {
        base.Initialize();
        float randomScale = Random.Range(minScale, maxScale);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        rotateSpeed = Random.Range(0, 500);
        boxCollider.enabled = true;
        TryGetComponent(out SpriteRenderer sprite);

        // Allows the enemy to rotate one direction or the other
        randomRotation = Random.Range(0, 1) * 2 - 1;

        // The enemy spawner will set the direction and scale based on
        // the parent that was destroyed, that's why it stops at this point
        if (isSplitEnemy == true) 
        {
            sprite.color = Color.red;
            return;
        }

        sprite.color = Color.white;

        SetDirectionAndScale((Vector2.zero - (Vector2)transform.position).normalized, 
            new Vector3(randomScale, randomScale, 1));
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

    public void SetPosition(Vector3 position) => rb.position = position;

    public void ResetVelocity() => rb.velocity = Vector2.zero;

    public void ResetCollider()
    {
        boxCollider.enabled = false;
        boxCollider.enabled = true;
    }

    public void SetIsASplitEnemy() => isSplitEnemy = true;
    public void SetDirectionAndScale(Vector2 direction, Vector3 scale)
    {
        directionToCenter = direction;
        rb.transform.localScale = scale;
    }

    protected override void Movement()
    {
        if (!CanMove)
        {
            rb.position = gameObject.transform.position;
            return;
        }

        rb.velocity = movementSpeed * Time.fixedDeltaTime * directionToCenter;
        Rotation(); // Called from here to avoid rotation if cannot move, local particles will not rotate which is intended
    }

    private void Rotation()
    {
        rb.SetRotation((rotateSpeed * Time.fixedDeltaTime) * randomRotation);
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        if (HasMoreLives()) return;

        DestroyEntity();
    }

    protected override void DestroyEntity()
    {
        isSplitEnemy = false;

        materialRenderer.enabled = false;
        boxCollider.enabled = false;
        DisableMovement();

        // This enqueues the current enemy and creates enemy children if possible
        GameplayEvents.InvokeEnemyToDestroy(this);

        GameplayEvents.InvokeEnemyIsDestroyed();
    }
}
