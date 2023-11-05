using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileState : MonoBehaviour
{
    int screenBoundLayer = 6;
    int playerLayer = 7;
    readonly BoxCollider2D screenCollider = ScreenBounds.screenBoxCollider;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(screenBoundLayer, playerLayer, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}
