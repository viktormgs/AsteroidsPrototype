using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileState : MonoBehaviour
{
    readonly int screenBoundLayer = 6;
    readonly int projectilesLayer = 8;


    private void Start()
    {
        Physics2D.IgnoreLayerCollision(screenBoundLayer, projectilesLayer, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

    }
}
