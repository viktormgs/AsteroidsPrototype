using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static Action LifeEvent;

    const int maxLives = 3;
    int currentLife;
    const int damage = 1;
    [SerializeField] GameObject[] lifeSprite;

    private void Start() => ResetLives();

    public void ResetLives()
    {
        currentLife = maxLives;
        for (int i = 0; i < lifeSprite.Length; i++)
        {
            lifeSprite[i].SetActive(true);
        }
    }

    public void LoseLife()
    {
        lifeSprite[currentLife].SetActive(false);
        currentLife -= damage;
        if (currentLife == 0) GameManager.LostGame();
    }
}
