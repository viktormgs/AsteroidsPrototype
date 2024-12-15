using System;

public static class GameplayEvents
{
    public static Action OnPlayerShoot;
    public static Action OnPlayerIsDamaged;
    public static Action<Enemy> OnEnemyToDestroy;
    public static Action OnEnemyIsDestroyed;


    public static void InvokePlayerShooting() => OnPlayerShoot?.Invoke();
    public static void InvokePlayerIsDamaged() => OnPlayerIsDamaged?.Invoke();
    public static void InvokeOnEnemyToDestroy(Enemy enemy) => OnEnemyToDestroy?.Invoke(enemy);
    public static void InvokeOnEnemyIsDestroyed() => OnEnemyIsDestroyed?.Invoke();
}
