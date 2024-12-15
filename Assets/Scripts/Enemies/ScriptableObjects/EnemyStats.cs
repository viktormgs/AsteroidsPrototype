using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTypeStats", menuName = "ScriptableObjects/Create New Enemy Type")]
public class EnemyStats : ScriptableObject
{
    public int minMovementSpeed;
    public int maxMovementSpeed;

    public float minScale;
    public float maxScale;

    public float spawnInterval;
    public int maxLives;
}
