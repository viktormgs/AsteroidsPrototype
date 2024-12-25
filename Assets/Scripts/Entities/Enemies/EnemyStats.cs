using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/Create New Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int minMovementSpeed;
    public int maxMovementSpeed;

    public float minScale;
    public float maxScale;

    public float spawnInterval;
    public int maxLives;
}
