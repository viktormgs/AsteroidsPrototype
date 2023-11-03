using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenBounds : MonoBehaviour
{
    //public UnityEvent<Collider2D> ExitTriggerEvent;
    
    float cameraHeight;
    [SerializeField] BoxCollider2D boxCollider;

    private void Awake()
    {
        //ExitTriggerEvent.AddListener();
    }

    void Start()
    {
        cameraHeight = Camera.main.orthographicSize *  2;
        var boxColliderSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight); //Adding a Box collider to the screen border
        boxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        WrapAround();
        //ExitTriggerEvent?.Invoke(collision);
    }
    //this is for the player
    void WrapAround()
    {
        if(transform.position.x <= -boxCollider.size.x)
        transform.position = new Vector2(boxCollider.size.x, transform.position.y);
    }

}
