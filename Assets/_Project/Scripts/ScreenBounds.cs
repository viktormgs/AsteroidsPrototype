using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public static float cameraHalfHeight;
    float cameraHeight;
    public static BoxCollider2D screenBoxCollider;
    public static Vector2 screenLimit;
    public static Vector2 bounds;

    void Start()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHeight = Camera.main.orthographicSize *  2;
        var boxColliderSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight); //Storing full screen device size into var
        screenLimit = boxColliderSize / 2;


        GameObject wall_left = CreateBoundary("wall_left");
        GameObject wall_top = CreateBoundary("wall_top");
        GameObject wall_right = CreateBoundary("wall_right");
        GameObject wall_bottom = CreateBoundary("wall_bottom");

        Vector2 offset = Vector2.one;  // Adjust screen boundaries here
        bounds = screenLimit + offset; //Also related to BoundaryTrigger.cs
        float scaleOffset = 1.15f;
        float width = .5f;
        ScreenBoundPlacer(wall_left, -bounds.x, transform.position.y, width, boxColliderSize.y * scaleOffset);
        ScreenBoundPlacer(wall_right, bounds.x, transform.position.y, width, boxColliderSize.y * scaleOffset);
        ScreenBoundPlacer(wall_top, transform.position.x, bounds.y, boxColliderSize.x * scaleOffset, width);
        ScreenBoundPlacer(wall_bottom, transform.position.x, -bounds.y, boxColliderSize.x * scaleOffset, width);
    }

    GameObject CreateBoundary(string name)
    {
        GameObject boundary = new(name);
        boundary.AddComponent<BoxCollider2D>();
        boundary.AddComponent<BoundaryTrigger>();
        return boundary;
    }

    void ScreenBoundPlacer(GameObject wallBound, float posX, float posY, float scaleX, float scaleY)
    {
        
        wallBound.transform.localScale = new Vector2(scaleX, scaleY);
        wallBound.transform.position = new Vector2(posX, posY);
    }
}
