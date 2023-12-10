using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    public static MenuInput instance;
    public event Action OnEscapePressed;
    public event Action OnEnterPressed;

    [HideInInspector] public bool hasPressedEscape;
    [HideInInspector] bool PressedEscape
    {
        get { return hasPressedEscape; }
        set
        {
            if (hasPressedEscape != value)
            {
                hasPressedEscape = value;
                OnEscapePressed?.Invoke();
            }
        }
    }
    [HideInInspector] public bool hasPressedEnter;
    [HideInInspector] bool PressedEnter
    {
        get { return hasPressedEnter; }
        set
        {
            if (hasPressedEnter != value)
            {
                hasPressedEnter = value;
                OnEnterPressed?.Invoke();
            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

    }

    void Update() => ReadInput();

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PressedEscape = !PressedEscape;
        PressedEnter = Input.GetKeyDown(KeyCode.KeypadEnter);
    }
}
