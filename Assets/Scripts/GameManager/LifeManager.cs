using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private GameObject life;
    private GameObject[] lives;


    private void Start()
    {
        GameManagerEvents.OnStartPlay += Initialize;
        GameplayEvents.OnPlayerIsDamaged += RemoveLife;

        Initialize();
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnStartPlay -= Initialize;
        GameplayEvents.OnPlayerIsDamaged -= RemoveLife;
    }

    private void Initialize()
    {
        lives = new GameObject[GameManager.Player.CurrentLives];

        for (int i = 0; i < GameManager.Player.CurrentLives; i++)
        {
            lives[i] = Instantiate(life, gameObject.transform);
        }

        RestoreLives(); // This ensures all active lives are enabled
    }

    private void RestoreLives()
    {
        for (int i = 0; i < GameManager.Player.CurrentLives; i++)
        {
            if (!lives[i].activeSelf) lives[i].SetActive(true);
        }
    }

    private void RemoveLife()
    {
        // Can be changed to trigger VFX if needed fancier visual representation
        lives[GameManager.Player.CurrentLives - 1].SetActive(false);
    }
}
