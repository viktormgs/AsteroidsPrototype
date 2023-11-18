using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name.ToString() + " has collided with " + gameObject.name.ToString());
            Debug.Log("player position: " + other.transform.position.ToString() + " and screenLimit: " + ScreenBounds.screenLimit.ToString());
        }

        Vector2 newPosition = other.transform.position;

        if (Mathf.Abs(newPosition.x) >= ScreenBounds.screenLimit.x)
        {
            newPosition.x = -Mathf.Sign(newPosition.x) * ScreenBounds.screenLimit.x;
        }

        if (Mathf.Abs(newPosition.y) >= ScreenBounds.screenLimit.y)
        {
            newPosition.y = -Mathf.Sign(newPosition.y) * ScreenBounds.screenLimit.y;
        }

        other.transform.position = newPosition;
    }
}
