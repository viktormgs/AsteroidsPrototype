using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
        OnDeath += PlayerManager.instance.Respawn;
    }

    public void LifeLostEvent() => OnDeath?.Invoke();

    void RemoveLife()
    {
        if (currentLife > -1)
        {
            currentLife -= damage;
            lifeSprite[currentLife].SetActive(false);
            Debug.Log(currentLife);
        }
        if (currentLife == 0)
        {
            Debug.Log("Calling it lost");
            GameManager.instance.LostAllLivesEvent();
        }
    }

    public void ResetLives() 
    {
        currentLife = lifeSprite.Length;
        Debug.Log(currentLife + " lives after reset");
        for (int i = 0; i < lifeSprite.Length; i++)
        {
            lifeSprite[i].SetActive(true);
        }
    }
}
