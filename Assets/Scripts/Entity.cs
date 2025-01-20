using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    private int currentLives;
    protected float movementSpeed;
    protected float rotateSpeed;

    protected bool CanMove = true;
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Renderer materialRenderer;
    [SerializeField] protected ParticleSystem damagedFX;
    public Coroutine FXPlaying { get; private set; }

    public int CurrentLives 
    { 
        get => currentLives; 
        private set
        {
            if(value < 0) value = 0;
            currentLives = value;
        }
    }

    protected virtual void Start()
    {
        TryGetComponent(out materialRenderer);

        Initialize();
    }

    private void OnEnable() => Initialize();

    protected virtual void Initialize()
    {
        if (damagedFX.gameObject.activeSelf) damagedFX.gameObject.SetActive(false);

        if(materialRenderer.enabled == false) materialRenderer.enabled = true;
        EnableMovement();
    }

    private void FixedUpdate() => Movement();
    protected abstract void Movement();

    protected void EnableMovement() => CanMove = true;
    protected void DisableMovement() => CanMove = false; 

    public virtual void TakeDamage()
    {
        CurrentLives--;      
        PlayDamagedFX();
    }

    protected bool HasMoreLives()
    {
        if (CurrentLives > 0) return true;
        else return false;
    }


    protected void PlayDamagedFX()
    {
        if (!damagedFX.gameObject.activeSelf) damagedFX.gameObject.SetActive(true);
        damagedFX.Play();

        FXPlaying = StartCoroutine(WaitForFX());
        IEnumerator WaitForFX() // To make sure there are no particles disappearing before destroying the GameObject
        {
            while (damagedFX.isPlaying) yield return null;

            damagedFX.gameObject.SetActive(false);
            FXPlaying = null;
        }
    }

    protected void ResetLives(int maxLives) => CurrentLives = maxLives;

    protected abstract void DestroyEntity();
}
