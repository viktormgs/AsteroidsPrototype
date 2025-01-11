using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

// This Script is added as a component on Start() when player is instantiated.
// More info in Player.cs
public class PlayerShoot : MonoBehaviour
{
    private GameObject projectile;
    private static GameObject projectileGroup; // Used to group things and keep scene's hierarchy clean
    private Rigidbody2D rb;
    
    // Stats
    private float projectileSpeed;
    private float lifeTime;
    private float shootingSpeed;

    private bool canShoot = true;
    private int maxAmmo;
    private readonly Queue<GameObject> projectileQueue = new();
    private readonly List<GameObject> activeProjectiles = new();

    public void Initialize(float projectileSpeed, float lifeTime, float shootingSpeed, int maxAmmo, GameObject projectile)
    {
        this.projectileSpeed = projectileSpeed;
        this.lifeTime = lifeTime;
        this.shootingSpeed = shootingSpeed;
        this.projectile = projectile;
        this.maxAmmo = maxAmmo;

        if (projectileGroup == null) projectileGroup = new("Projectiles");
        SetupProjectiles();
        GameplayEvents.OnPlayerShoot += Shoot;
        GameManagerEvents.OnGameOver += Clearprojectiles;
    }

    private void OnEnable()
    {
        GameplayEvents.OnPlayerShoot += Shoot;
        canShoot = true;
    }

    private void OnDisable()
    {
        canShoot = false;
        GameplayEvents.OnPlayerShoot -= Shoot;
    }

    private void SetupProjectiles()
    {
        if (projectileGroup == null) projectileGroup = Instantiate(projectileGroup);

        for (int i = 0; i < maxAmmo; i++) // To easily re-use instantiated projectiles
        {
            projectileQueue.Enqueue(CreateProjectile());
        }
    }

    private GameObject CreateProjectile()
    {
        GameObject pooledProjectile = Instantiate(projectile, projectileGroup.transform);
        pooledProjectile.SetActive(false);
        return pooledProjectile;
    }

    private GameObject GetProjectile()
    {
        // Creates a projectile if there are no available projectiles in the pool
        if (projectileQueue.Count == 0) 
        {
            GameObject newProjectile = CreateProjectile();
            projectileQueue.Enqueue(newProjectile);
            return newProjectile;
        }

        GameObject activeProjectile = projectileQueue.Dequeue();
        activeProjectile.TryGetComponent(out rb); // Need this because it uses physics (velocity) to move
        activeProjectile.SetActive(true);

        activeProjectiles.Add(activeProjectile); // To keep track of active projectiles on screen

        return activeProjectile;
    }

    private void Shoot()
    {
        if (canShoot)
        {
            StartCoroutine(ShootingSpeed());
            var projectileInUse = GetProjectile();

            // Z-Position is set to always be behind the player otherwise it doesn't look good
            projectileInUse.transform.SetPositionAndRotation(new Vector3
                (gameObject.transform.position.x, gameObject.transform.position.y, 
                gameObject.transform.position.z + 1), gameObject.transform.rotation); 

            rb.velocity = projectileSpeed * transform.up;

            StartCoroutine(ProjectileLifetime(projectileInUse));
        }

        // Coroutines embedded into this method because they are not supposed to be called from anywhere else.
        IEnumerator ProjectileLifetime(GameObject inUseProjectile)
        {
            yield return new WaitForSeconds(lifeTime);
            DisableProjectile(inUseProjectile);
            yield break;
        }

        // Easiest way I can think of setting up Attack Speed without having to use Update();
        IEnumerator ShootingSpeed()
        {
            canShoot = false;
            yield return new WaitForSeconds(shootingSpeed);
            canShoot = true;
            yield break;
        }
    }

    private void DisableProjectile(GameObject projectileInUse)
    {
        activeProjectiles.Remove(projectileInUse);

        projectileInUse.SetActive(false);
        projectileQueue.Enqueue(projectileInUse);
    }

    private void Clearprojectiles()
    {
        foreach (GameObject projectile in activeProjectiles)
        {
            DisableProjectile(projectile);
        }
        projectileQueue.Clear();
    }
}
