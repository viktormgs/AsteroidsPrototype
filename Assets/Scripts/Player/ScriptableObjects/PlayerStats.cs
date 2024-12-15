using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Custom/Create Player Stats")]
public class PlayerStats : ScriptableObject
{
    // Player-as-an entity stats
    public int movementSpeed;
    public int rotateSpeed;
    public int maxAmmo;
    public int maxLives;

    // Weapon Stats
    public float projectileSpeed;
    public float lifeTime;
    public float shootingSpeed;
}
