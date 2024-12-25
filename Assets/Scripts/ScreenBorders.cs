using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorders : MonoBehaviour
{
    public static float CameraHalfHeight { get; private set; }
    public static Vector2 ScreenSize { get; private set; }
    public static Vector2 ScreenLimits { get; private set; }


    private const int screenBoundsLayer = 6;

    private Vector2 borderPos;
    private readonly Vector2 borderOffset = new(1.2f, 1.2f); // Because the screen wrap should happen outside of the visible screen
    private const float scaleOffset = 1.2f; // Make walls size bigger to cover the corners
    private const float boundaryWidth = 0.5f; // How thick the borders are


    private void Start()
    {
        InitializeScreenBorders();
        SetupBorders();
    }

    private void InitializeScreenBorders()
    {
        CameraHalfHeight = Camera.main.orthographicSize;
        float cameraHeight = CameraHalfHeight * 2;

        // Calculate screen limits based on camera height and aspect ratio
        ScreenSize = new(cameraHeight * Camera.main.aspect, cameraHeight);
        ScreenLimits = ScreenSize / 2;
        borderPos = ScreenLimits + borderOffset;
    }

    private void SetupBorders()
    {
        // Create boundary walls
        GameObject borderLeft = CreateBorder("Border_Left");
        GameObject borderRight = CreateBorder("Border_Right");
        GameObject borderTop = CreateBorder("Border_Top");
        GameObject borderBottom = CreateBorder("Border_Bottom");

        // Place boundary walls at appropriate positions base on device screen size
        PlaceBorder(borderLeft, new Vector2(-borderPos.x, 0), new Vector2(boundaryWidth, ScreenSize.y * scaleOffset));
        PlaceBorder(borderRight, new Vector2(borderPos.x, 0), new Vector2(boundaryWidth, ScreenSize.y * scaleOffset));
        PlaceBorder(borderTop, new Vector2(0, borderPos.y), new Vector2(ScreenSize.x * scaleOffset, boundaryWidth));
        PlaceBorder(borderBottom, new Vector2(0, -borderPos.y), new Vector2(ScreenSize.x * scaleOffset, boundaryWidth));
    }

    private GameObject CreateBorder(string name)
    {
        GameObject boundary = new(name)
        {
            layer = screenBoundsLayer
        };
        boundary.AddComponent<BoxCollider2D>();
        boundary.AddComponent<ScreenWrap>();
        return boundary;
    }

    private void PlaceBorder(GameObject wall, Vector2 position, Vector2 scale)
    {
        wall.transform.position = position;
        wall.transform.localScale = scale;
    }
}
