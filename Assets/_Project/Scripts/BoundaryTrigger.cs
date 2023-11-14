using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 newPosition = other.transform.position;

        if (Mathf.Abs(newPosition.x) >= ScreenBounds.bounds.x)
            newPosition.x = -Mathf.Sign(newPosition.x) * ScreenBounds.bounds.x;

        if (Mathf.Abs(newPosition.y) >= ScreenBounds.bounds.y)
            newPosition.y = -Mathf.Sign(newPosition.y) * ScreenBounds.bounds.y;

        other.transform.position = newPosition;
    }
}
