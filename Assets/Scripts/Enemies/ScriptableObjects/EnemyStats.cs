using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsLvlx", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int minSpeed;
    public int maxSpeed;
    public float minScale;
    public float maxScale;
    public float spawnInterval;
}
