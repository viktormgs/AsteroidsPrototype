using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Player Stats", menuName = "Custom/PlayerStats")]
public class PlayerStats : ScriptableObject
{

    public int movementSpeed;
    public int rotateSpeed;
    public int maxAmmo;
}
