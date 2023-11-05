using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    float cameraHeight;
    public static BoxCollider2D screenBoxCollider;
    public static Vector2 bounds;


    void Start()
    {
        screenBoxCollider = GetComponent<BoxCollider2D>(); //Code Reference because public static
        cameraHeight = Camera.main.orthographicSize *  2;
        var boxColliderSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight); //Adding a Box collider to the screen border

        bounds = boxColliderSize / 2;
        screenBoxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Vector2 newPosition = other.transform.position;

        if (Mathf.Abs(newPosition.x) >= bounds.x)
            newPosition.x = -Mathf.Sign(newPosition.x) * bounds.x;

        if (Mathf.Abs(newPosition.y) >= bounds.y)
            newPosition.y = -Mathf.Sign(newPosition.y) * bounds.y;

        other.transform.position = newPosition;
    }
}
