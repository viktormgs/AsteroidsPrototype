using System;

public static class GameManagerEvents
{
    public static Action OnStartPlay;
    public static Action OnExitPlay;
    public static Action OnResetPlay;
    public static Action OnPause;


    public static void InvokeOnStartPlay() => OnStartPlay?.Invoke();
    public static void InvokeOnExitPlay() => OnExitPlay?.Invoke();
    public static void InvokeOnResetPlay() => OnResetPlay?.Invoke();
    public static void InvokeOnPause() => OnPause?.Invoke();
}
