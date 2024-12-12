using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : PlayerReadInput
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] int ammoPoolCapacity;
    Rigidbody2D rb;
    GameObject inUseProjectile;
    bool canShoot = true;
    [SerializeField] float attackSpeed;
    readonly Queue<GameObject> ammoQueue = new();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ammoPoolCapacity; i++)
        {
            var pooledProjectile = EnemyGenerator();

            ammoQueue.Enqueue(pooledProjectile);
        }
    }

    GameObject EnemyGenerator()
    {
        GameObject pooledProjectile = Instantiate(projectile);
        pooledProjectile.SetActive(false);
        return pooledProjectile;
    }

    private GameObject GetProjectile()
    {
        if (ammoQueue.Count == 0)
        {
            var newPooledProjectile = EnemyGenerator();
            ammoQueue.Enqueue(newPooledProjectile);
            return newPooledProjectile;
        }

        GameObject pooledProjectile = ammoQueue.Dequeue();
        pooledProjectile.SetActive(true);

        return pooledProjectile;
    }

    private void Update() => Fire();

    void Fire()
    {
        if (fire && canShoot)
        {
            StartCoroutine(AttackSpeed());
            inUseProjectile = GetProjectile();
            rb = inUseProjectile.GetComponent<Rigidbody2D>();

            //Set Projectile Pos and Rotation, Z position is set to always be behind the player
            inUseProjectile.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1), gameObject.transform.rotation); 

            rb.velocity = projectileSpeed * transform.up; //Projectile Movement

            //Call Projectile lifetime
            StartCoroutine(ProjectileLifetime(inUseProjectile));
        }
    }

    IEnumerator ProjectileLifetime(GameObject inUseProjectile)
    {
        yield return new WaitForSeconds(projectileLifetime);
        DisableProjectile(inUseProjectile);
        yield break;
    }

    IEnumerator AttackSpeed()
    {
        canShoot = false;
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
        yield break;
    }

    public void DisableProjectile(GameObject inUseProjectile)
    {
        inUseProjectile.SetActive(false);
        ammoQueue.Enqueue(inUseProjectile);
    }
}
