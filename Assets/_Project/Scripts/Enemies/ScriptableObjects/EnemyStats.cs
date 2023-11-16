using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsLvlx", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float minSpeed;
    public float maxSpeed;
    public float minScale;
    public float maxScale;
}
