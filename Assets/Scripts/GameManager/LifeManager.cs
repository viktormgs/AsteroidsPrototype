using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private GameObject life;
    private readonly GameObject[] lives;

    private int CurrentLives => GameManager.Player.CurrentLives;

    private void Start()
    {
        GameManagerEvents.OnStartPlay += Initialize;
        GameManagerEvents.OnStartPlay += ResetLives;
        GameplayEvents.OnPlayerIsDamaged += RemoveLife;
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnStartPlay -= Initialize;
        GameManagerEvents.OnStartPlay -= ResetLives;
        GameplayEvents.OnPlayerIsDamaged -= RemoveLife;
    }

    private void Initialize()
    {
        for (int i = 0; i < CurrentLives - 1; i++)
        {
            lives[i] = Instantiate(life, gameObject.transform);
            ResetLives(); // This ensures all active lives are enabled
        }
    }

    private void RemoveLife()
    {
        // Can be changed to trigger VFX if needed fancier visual representation
        lives[CurrentLives - 1].SetActive(false); 
    }

    private void ResetLives()
    {
        for (int i = 0; i < CurrentLives; i++)
        {
            if (!lives[i].activeSelf) lives[i].SetActive(true);
        }
    }
}
