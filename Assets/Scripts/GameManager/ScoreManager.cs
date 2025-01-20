using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;

    private int currentScore = 0;
    private const int addScore = 1;

    public static int HighestRecord { get; private set; } = 0;

    private void Start() 
    {
        GameManagerEvents.OnStartPlay += ResetCurrentScore;
        GameplayEvents.OnEnemyIsDestroyed += AddScore;
        GameplayEvents.OnEnemyIsDestroyed += CheckForNewRecord;
        GameManagerEvents.OnGameOver += CheckForNewRecord;


        LoadRecordScore();
        ResetCurrentScore();
    }

    private void OnDestroy()
    {
        GameManagerEvents.OnStartPlay -= ResetCurrentScore;
        GameplayEvents.OnEnemyIsDestroyed -= AddScore;
        GameplayEvents.OnEnemyIsDestroyed -= CheckForNewRecord;
        GameManagerEvents.OnGameOver -= CheckForNewRecord;
    }

    private void LoadRecordScore()
    {
        PlayerPrefs.GetInt("Highest Score", HighestRecord);
    }

    private void ResetCurrentScore()
    {
        UpdateScoreToUI(currentScore = 0);
    }

    private void AddScore() => UpdateScoreToUI(currentScore += addScore);

    private void UpdateScoreToUI(int currentScore)
    {
        Debug.Log($"Current Score: {currentScore}");
        textScore.text = currentScore.ToString("000");
    }

    private void CheckForNewRecord()
    {
        if (currentScore < HighestRecord) return;

        HighestRecord = currentScore;
        PlayerPrefs.SetInt("Highest Score", HighestRecord);
        PlayerPrefs.Save();
    }
}
