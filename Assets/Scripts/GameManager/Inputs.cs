using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public static float HorizontalInput { get; private set; } = 0f;
    public static float VerticalInput { get; private set; }
    private static bool action = false;
    public static bool Action
    {
        get => action;
        set 
        {            
            action = value;
            if(action == true) GameplayEvents.InvokePlayerShooting();
        }
    }

    private static bool pressedEscape;
    private static bool PressedEscape
    {
        get => pressedEscape;
        set
        {
            pressedEscape = value;

            if(ScreenManager.CanPauseOrResumeGame() == false) return;

            if (pressedEscape) GameManagerEvents.InvokeOnPause();
            else GameManagerEvents.InvokeOnResume();
        }
    }

    private static bool pressedEnter;
    private static bool PressedEnter
    {
        get => pressedEnter;
        set
        {
            if (pressedEnter != value)
            {
                pressedEnter = value;
                //GameManagerEvents.OnEnterPressed?.Invoke();
            }
        }
    }

    private void Update() => ReadInput();
    private void ReadInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        Action = Input.GetKey(KeyCode.Space); // Player must press multiple times to shoot multiple projectiles

        if (Input.GetKeyDown(KeyCode.Escape)) PressedEscape = !PressedEscape; // Toggle functionality to handle Pause State
        PressedEnter = Input.GetKeyDown(KeyCode.KeypadEnter);
    }
}
