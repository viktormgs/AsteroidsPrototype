using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    int currentScore;
    const int addScore = 1;
    [SerializeField] TextMeshProUGUI text;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start() => ResetScore();

    public void ResetScore() => UpdateScoreToUI(currentScore = 0);

    public void AddScore() => UpdateScoreToUI(currentScore += addScore);

    public void CheckForNewRecord()
    {
        //Check for new highest score to set a new Record
    }

    void UpdateScoreToUI(int currentScore)
    {
        text.text = currentScore.ToString("0000");
    }

}
