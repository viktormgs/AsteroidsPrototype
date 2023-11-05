using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
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
        rb = projectile.GetComponent<Rigidbody2D>();

        for (int i = 0; i < ammoPoolCapacity; i++)
        {
            GameObject pooledProjectile = Instantiate(projectile);
            pooledProjectile.SetActive(false);

            ammoQueue.Enqueue(pooledProjectile);
        }
    }
    private GameObject GetProjectile()
    {
        GameObject pooledProjectile = ammoQueue.Dequeue();
        pooledProjectile.SetActive(true);
        return pooledProjectile;
    }

    private void Update()
    {
        Fire();
    }
    void Fire()
    {
        if (ammoQueue.Count != 0 && PlayerReadInput.fire && canShoot)
        {
            StartCoroutine(AttackSpeed());
            inUseProjectile = GetProjectile();
            rb = inUseProjectile.GetComponent<Rigidbody2D>();
            inUseProjectile.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation); //Set Projectile Pos and Rotation

            rb.velocity = projectileSpeed * Time.deltaTime * transform.up; //Projectile Movement

            //Call Projectile lifetime
            StartCoroutine(DisableProjectile(inUseProjectile));
        }
    }

    IEnumerator DisableProjectile(GameObject inUseProjectile)
    {
        yield return new WaitForSeconds(projectileLifetime);
        inUseProjectile.SetActive(false);
        ammoQueue.Enqueue(inUseProjectile);
        yield break;
    }

    IEnumerator AttackSpeed()
    {
        canShoot = false;
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
        yield break;
    }
}
