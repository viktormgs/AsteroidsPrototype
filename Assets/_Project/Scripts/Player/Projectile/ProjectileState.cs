using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileState : MonoBehaviour
{
    int screenBoundLayer = 6;
    readonly bool ignore = true;
    BoxCollider2D screenCollider = ScreenBounds.screenBoxCollider;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7, ignore);
        Physics2D.IgnoreCollision(screenCollider, screenCollider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
    }
}
