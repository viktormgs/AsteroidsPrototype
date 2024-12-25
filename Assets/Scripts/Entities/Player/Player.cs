using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject projectile;
    private PlayerShoot playerShoot;

    public override int CurrentLives
    {
        get => base.CurrentLives;
        protected set
        {
            base.CurrentLives = value;
            // Do something to send to life manager ????
        }
    }

    private Renderer materialRenderer;
    private readonly int invincibilityLifetime = 4;
    private const int playerLayer = 7;
    private const int enemyLayer = 9;


    protected override void Start()
    {
        base.Start();

        GameplayEvents.OnPlayerIsDamaged += TakeDamage;
        GameManagerEvents.OnGameOver += DestroyEntity;

        TryGetComponent(out materialRenderer); // Needed for visual flickering

        // Added the component like this because all relevant data comes from PlayerStats,
        // also, cleaner in the Inspector because there are no fields to expose from PlayerShoot Class.
        playerShoot = gameObject.AddComponent<PlayerShoot>(); 
        playerShoot.Initialize(playerStats.projectileSpeed, playerStats.lifeTime, playerStats.shootingSpeed, projectile);
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        ResetLives(playerStats.maxLives);
        movementSpeed = playerStats.movementSpeed;
        rotateSpeed = playerStats.rotateSpeed;
    }

    protected override void Movement()
    {
        var playerDirection = new Vector2(Inputs.HorizontalInput, Inputs.VerticalInput);
        rb.AddForce(movementSpeed * playerDirection); // AddForce gives a more realistic feeling when moving


        if (playerDirection != Vector2.zero) //Rotate player towards direction
        {
            var lookAtDirection = Quaternion.LookRotation(transform.forward, playerDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtDirection, rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage();
            GameplayEvents.InvokePlayerIsDamaged();
        }
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        if(HasMoreLives()) StartCoroutine(Respawn());

        else GameManagerEvents.InvokeOnGameOver();
    }

    private IEnumerator Respawn()
    {
        float flickerRate = .4f; // For Player visual flickering effect
        materialRenderer.enabled = false;

        // Gives a bit of time so the player doesn't instantly respawn
        while (fXPlaying != null) yield return null; 

        transform.position = Vector2.zero;

        for (int i = 0; i < invincibilityLifetime; i++)
        {
            // Used this than disabling collider because screen wrapping depends on collisions
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer); 
            
            yield return new WaitForSeconds(flickerRate);
            materialRenderer.enabled = false;
            yield return new WaitForSeconds(flickerRate);
            materialRenderer.enabled = true;

            flickerRate /= 1.5f; // The flickering happens faster after each iteration, no need to change so magic number
        }

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        yield break;
    }

    protected override void DestroyEntity()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

