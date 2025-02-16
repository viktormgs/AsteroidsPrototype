using System;

public static class GameplayEvents
{
    public static Action OnPlayerShoot;
    public static Action OnPlayerIsDamaged;
    public static Action<Enemy> OnEnemyToDestroy;
    public static Action OnEnemyIsDestroyed;
    public static Action OnNewRecord;


    public static void InvokePlayerShooting() => OnPlayerShoot?.Invoke();
    public static void InvokePlayerIsDamaged() => OnPlayerIsDamaged?.Invoke();
    public static void InvokeEnemyToDestroy(Enemy enemy) => OnEnemyToDestroy?.Invoke(enemy);
    public static void InvokeEnemyIsDestroyed() => OnEnemyIsDestroyed?.Invoke();
    public static void InvokeNewRecord() => OnNewRecord?.Invoke();

}
