using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public static float horizontalInput { get; private set; } = 0f;
    public static float verticalInput { get; private set; } = 0f;
    private static bool action = false;
    public static bool Action
    {
        get => action;
        set 
        {
            if(action == value) return;
            
            action = value;
            if(action == true) GameplayEvents.InvokePlayerShooting();
        }
    }

    private void Update() => ReadInput();
    private void ReadInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Action = Input.GetKey(KeyCode.Space);
    }
}
