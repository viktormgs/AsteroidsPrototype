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
    private const string  savedScore = "Highest Score";

    public static int HighestRecord { get; private set; } = 0;

    private void Awake()
    {
        LoadRecordScore();
    }

    private void Start() 
    {
        GameManagerEvents.OnStartPlay += ResetCurrentScore;
        GameplayEvents.OnEnemyIsDestroyed += AddScore;
        GameplayEvents.OnEnemyIsDestroyed += CheckForNewRecord;
        GameManagerEvents.OnGameOver += CheckForNewRecord;


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
        HighestRecord = PlayerPrefs.GetInt(savedScore);
    }

    private void ResetCurrentScore()
    {
        UpdateScoreToUI(currentScore = 0);
    }

    private void AddScore() => UpdateScoreToUI(currentScore += addScore);

    private void UpdateScoreToUI(int currentScore)
    {
        textScore.text = currentScore.ToString("000");
    }

    private void CheckForNewRecord()
    {
        if (currentScore < HighestRecord) return;

        HighestRecord = currentScore;
        PlayerPrefs.SetInt(savedScore, HighestRecord);
        PlayerPrefs.Save();

        GameplayEvents.InvokeNewRecord();
    }
}
