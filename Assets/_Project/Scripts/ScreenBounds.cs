using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    float cameraHeight;
    public static BoxCollider2D screenBoxCollider;
    Vector2 boundsForPlayer;


    void Start()
    {
        screenBoxCollider = GetComponent<BoxCollider2D>(); //Code Reference because public static
        cameraHeight = Camera.main.orthographicSize *  2;
        var boxColliderSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight); //Adding a Box collider to the screen border

        boundsForPlayer = boxColliderSize / 2;
        screenBoxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Vector2 newPosition = other.transform.position;

        if (Mathf.Abs(newPosition.x) >= boundsForPlayer.x)
            newPosition.x = -Mathf.Sign(newPosition.x) * boundsForPlayer.x;

        if (Mathf.Abs(newPosition.y) >= boundsForPlayer.y)
            newPosition.y = -Mathf.Sign(newPosition.y) * boundsForPlayer.y;

        other.transform.position = newPosition;
    }
}
