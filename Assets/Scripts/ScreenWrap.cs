using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D element)
    {
        Vector2 elementPos = element.transform.position;

        if (Mathf.Abs(elementPos.x) >= ScreenBorders.ScreenLimits.x)
        {
            elementPos.x = -Mathf.Sign(elementPos.x) * ScreenBorders.ScreenLimits.x;
        }

        if (Mathf.Abs(elementPos.y) >= ScreenBorders.ScreenLimits.y)
        {
            elementPos.y = -Mathf.Sign(elementPos.y) * ScreenBorders.ScreenLimits.y;
        }

        element.transform.position = elementPos;
    }
}
