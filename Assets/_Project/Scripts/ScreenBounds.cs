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

    private void Awake()
    {

        //ExitTriggerEvent += WrapAround;
    }

    void Start()
    {
        screenBoxCollider = GetComponent<BoxCollider2D>(); //Code Reference because public static
        cameraHeight = Camera.main.orthographicSize *  2;
        var boxColliderSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight); //Adding a Box collider to the screen border
        screenBoxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Log("Has Exit the Screen Boundaries");
        //ExitTriggerEvent?.Invoke();
        if (other.transform.position.x <= -screenBoxCollider.size.x)
            Log("Pos triggered");
            other.transform.position = new Vector2(screenBoxCollider.size.x + offset, transform.position.y);
    }

    void Log(object message)
    {
        Debug.Log(message);
    }

}
