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
        //screenBoxCollider = GetComponent<BoxCollider2D>(); //Referenced here because public static
        cameraHeight = Camera.main.orthographicSize *  2;
        var boxColliderSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight); //Adding a Box collider to the screen border

        bounds = boxColliderSize / 2;
        //screenBoxCollider.size = boxColliderSize;

        GameObject wall_left = CreateBoundary("wall_left");
        GameObject wall_top = CreateBoundary("wall_top");
        GameObject wall_right = CreateBoundary("wall_right");
        GameObject wall_bottom = CreateBoundary("wall_bottom");

        float offset = 1f;
        ScreenBoundPlacer(wall_left, -bounds.x - offset, transform.position.y, transform.localScale.x, boxColliderSize.y);
        ScreenBoundPlacer(wall_right, bounds.x + offset, transform.position.y, transform.localScale.x, boxColliderSize.y);
        ScreenBoundPlacer(wall_top, transform.position.x, bounds.y + offset, boxColliderSize.x, transform.localScale.y);
        ScreenBoundPlacer(wall_bottom, transform.position.x, -bounds.y - offset, boxColliderSize.x, transform.localScale.y);
    }

    GameObject CreateBoundary(string name)
    {
        GameObject boundary = new GameObject(name);
        boundary.AddComponent<BoxCollider2D>();
        boundary.AddComponent<BoundaryTrigger>();
        return boundary;
    }

    void ScreenBoundPlacer(GameObject wallBound, float posX, float posY, float scaleX, float scaleY)
    {
        
        wallBound.transform.localScale = new Vector2(scaleX, scaleY);
        wallBound.transform.position = new Vector2(posX, posY);
    }



    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    Vector2 newPosition = other.transform.position;

    //    if (Mathf.Abs(newPosition.x) >= bounds.x)
    //        newPosition.x = -Mathf.Sign(newPosition.x) * bounds.x;

    //    if (Mathf.Abs(newPosition.y) >= bounds.y)
    //        newPosition.y = -Mathf.Sign(newPosition.y) * bounds.y;

    //    other.transform.position = newPosition;
    //}
}
