using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Events
{
    public static GameManager instance;
    public event Action OnGameOver;
    public event Action OnReset;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        //OnGameOver += ScoreManager.instance.CheckForNewRecord;
        OnGameOver += LifeManager.instance.ResetLives;
        OnGameOver += ScoreManager.instance.ResetScore;
        //add ui screens to the events

        OnReset += LifeManager.instance.ResetLives;
        OnReset += ScoreManager.instance.ResetScore;

    }

    public void Retry()
    {
        OnReset?.Invoke();
    }

    public void LostAllLives()
    {
        OnGameOver?.Invoke();
    }
}
