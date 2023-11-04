using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDuration = 5f;
    [SerializeField] int ammoPoolCapacity = 5;
    Rigidbody2D rb;

    Queue<GameObject> ammoQueue = new Queue<GameObject>();

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
        if (ammoQueue.Count == 0)
        {
            return null;
        }
        GameObject pooledProjectile = ammoQueue.Dequeue();
        pooledProjectile.SetActive(true);
        return pooledProjectile;
    }

    private void OnMouseDown()
    {
        Fire();
    }
    void Fire()
    {
        var newProjectile = GetProjectile();
        rb = newProjectile.GetComponent<Rigidbody2D>();
        newProjectile.transform.position = gameObject.transform.position;
        newProjectile.transform.rotation = gameObject.transform.rotation;
        float startTime = 0f;
        while(startTime <= projectileDuration)
        {
            rb.transform.position = projectileSpeed * Time.deltaTime * Vector2.up;
            startTime += Time.deltaTime/projectileDuration;
        }
    }
    

}
