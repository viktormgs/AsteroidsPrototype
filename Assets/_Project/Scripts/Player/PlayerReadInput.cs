using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadInput : MonoBehaviour
{
    public static float horizontalInput = 0f;
    public static float verticalInput = 0f;

    void Update()
    {
        ReadInput();
    }

    void ReadInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}
