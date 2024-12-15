using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    private int currentLives;
    protected float movementSpeed;
    protected float rotateSpeed;
    
    protected Rigidbody2D rb;
    [SerializeField] protected ParticleSystem damagedFX;


    protected virtual void Start()
    {
        if (damagedFX.gameObject.activeSelf) damagedFX.gameObject.SetActive(false);

        // Rigidbody is required for this class, this ensures we don't have a missing serialized reference instead
        TryGetComponent(out rb); 
    }

    protected virtual void FixedUpdate() => Movement();
    protected abstract void Movement();


    public virtual void TakeDamage()
    {
        currentLives--;
        if (!damagedFX.gameObject.activeSelf) damagedFX.gameObject.SetActive(true);
        if (HasMoreLives()) return;

        DestroyEntity();
    }

    private bool HasMoreLives()
    {
        if (currentLives > 0) return true;
        else
        {
            currentLives = 0;
            return false;
        }
    }

    protected void ResetLives(int maxLives) => currentLives = maxLives;

    protected abstract void DestroyEntity();

}
