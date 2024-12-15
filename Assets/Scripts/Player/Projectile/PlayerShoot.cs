using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

// This Script is added as component on Start() when player is instantiated.
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

    private static bool canShoot = true;
    private const int maxAmountOfProjectiles = 7; // Assuming the average amount of max projectiles on screen
    private readonly Queue<GameObject> projectilePool = new();

    public void Initialize(float projectileSpeed, float lifeTime, float shootingSpeed, GameObject projectile)
    {
        this.projectileSpeed = projectileSpeed;
        this.lifeTime = lifeTime;
        this.shootingSpeed = shootingSpeed;
        this.projectile = projectile;

        projectileGroup = new();
        SetupProjectiles();
    }

    private void SetupProjectiles()
    {
        GameplayEvents.OnPlayerShoot += Shoot;

        if (projectileGroup == null) projectileGroup = Instantiate(projectileGroup);
        
        CreateProjectilePool();

    }

    private void CreateProjectilePool() // To easily re-use instantiated projectiles
    {
        for (int i = 0; i < maxAmountOfProjectiles; i++)
        {
            var newProjectile = CreateProjectile();

            projectilePool.Enqueue(newProjectile);
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
        if (projectilePool.Count == 0) 
        {
            var newProjectile = CreateProjectile();
            projectilePool.Enqueue(newProjectile);
            return newProjectile;
        }

        GameObject pooledProjectile = projectilePool.Dequeue();
        pooledProjectile.TryGetComponent(out rb); // Need this because it uses physics (velocity) to move
        pooledProjectile.SetActive(true);

        return pooledProjectile;
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

            rb.velocity = projectileSpeed * transform.up; //Projectile Movement

            // Call Projectile lifetime
            StartCoroutine(ProjectileLifetime(projectileInUse));
        }

        // Coroutines embedded into the method because they are not supposed to be called from anywhere else.
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
        projectileInUse.SetActive(false);
        projectilePool.Enqueue(projectileInUse);
    }
}
