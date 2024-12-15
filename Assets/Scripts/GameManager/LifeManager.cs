using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public event Action OnDeath;

    int currentLife;
    const int damage = 1;
    [SerializeField] GameObject[] lifeSprite;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        ResetLives();
        OnDeath += RemoveLife;
    }

    void RemoveLife()
    {
        if (currentLife > -1)
        {
            currentLife -= damage;
            lifeSprite[currentLife].SetActive(false);
        }
        if (currentLife == 0)
        {
            GameManager.instance.CallAction(GameManager.instance.OnGameOver);
        }
    }

    public void ResetLives() 
    {
        currentLife = lifeSprite.Length;
        for (int i = 0; i < lifeSprite.Length; i++)
        {
            lifeSprite[i].SetActive(true);
        }
    }
}
