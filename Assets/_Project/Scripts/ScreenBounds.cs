using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenBounds : MonoBehaviour
{
    public Event ExitTriggerEvent;
    
    float cameraHeight;
    public static BoxCollider2D screenBoxCollider;
    float offset = 0.2f;
    Vector2 boundsForPlayer;

    private void Awake()
    {

        //ExitTriggerEvent += WrapAround;
    }

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
        Log("Has Exit the Screen Boundaries");
        Vector2 newPosition = other.transform.position;

        if (Mathf.Abs(newPosition.x) >= boundsForPlayer.x)
            newPosition.x = -Mathf.Sign(newPosition.x) * boundsForPlayer.x;

        if (Mathf.Abs(newPosition.y) >= boundsForPlayer.y)
            newPosition.y = -Mathf.Sign(newPosition.y) * boundsForPlayer.y;

        other.transform.position = newPosition;

    }


    void Log(object message)
    {
        Debug.Log(message);
    }

}
