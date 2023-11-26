using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHitEvent : MonoBehaviour
{
    public static Action EnemyInteraction;

    public static void Main()
    {
        var enemyState = new EnemyState();
        var enemySpawner = new EnemySpawner();
        //var scoreManager = new ScoreManager();


        //EnemyInteraction += enemyState.DestroyEnemy;

    }


}
