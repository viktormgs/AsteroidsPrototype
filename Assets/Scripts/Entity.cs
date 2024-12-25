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
    protected Coroutine fXPlaying;

    public virtual int CurrentLives 
    { 
        get => currentLives; 
        protected set
        {
            if(value < 0) value = 0;
            currentLives = value;
        }
    }

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
        CurrentLives--;
        PlayDamagedFX();
    }

    protected bool HasMoreLives()
    {
        if (CurrentLives > 0) return true;
        else
        {
            CurrentLives = 0;
            return false;
        }
    }


    protected void PlayDamagedFX()
    {
        if (!damagedFX.gameObject.activeSelf) damagedFX.gameObject.SetActive(false);
        damagedFX.gameObject.SetActive(true);

        fXPlaying = StartCoroutine(WaitForFX());
        IEnumerator WaitForFX() // To make sure there are no particles disappearing before destroying the GameObject
        {
            while (damagedFX.IsAlive()) yield return null;
            damagedFX.gameObject.SetActive(false);
            yield break;
        }
        fXPlaying = null;
    }

    protected void ResetLives(int maxLives) => CurrentLives = maxLives;

    protected abstract void DestroyEntity();

}
