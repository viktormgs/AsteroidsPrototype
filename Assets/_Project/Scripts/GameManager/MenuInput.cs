using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    public static bool hasPressedEscape;
    public static bool hasPressedEnter;

    void Update() => ReadInput();

    private void ReadInput()
    {
        hasPressedEscape = Input.GetKeyDown(KeyCode.Escape);
        hasPressedEnter = Input.GetKeyDown(KeyCode.KeypadEnter);
    }
}
