using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreManager : Events
{
    public static ScoreManager instance;

    public int currentScore;
    readonly int addScore = 1;
    readonly TextMeshProUGUI text;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start() => ResetScore();

    public void ResetScore() => currentScore = 0;

    public void AddScore() => currentScore += addScore;

    public void CheckForNewRecord()
    {
        //Check for new highest score to set a new Record


    }

}
