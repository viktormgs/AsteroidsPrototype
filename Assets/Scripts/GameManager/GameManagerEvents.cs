using System;

public static class GameManagerEvents
{
    public static Action OnGameStart;

    public static Action OnStartPlay;
    public static Action OnExitPlay;
    public static Action OnGameOver;
    public static Action OnPause;
    public static Action OnResume;
    public static Action OnExitGame;


    public static void InvokeOnGameStart() => OnGameStart?.Invoke();

    public static void InvokeOnStartPlay() => OnStartPlay?.Invoke();
    public static void InvokeOnExitPlay() => OnExitPlay?.Invoke();
    public static void InvokeOnGameOver() => OnGameOver?.Invoke();
    public static void InvokeOnPause() => OnPause?.Invoke();
    public static void InvokeOnResume() => OnResume?.Invoke();
    public static void InvokeOnExitGame() => OnExitGame?.Invoke();
}
