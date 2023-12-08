using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public event Action OnEnemyDestroyed;

    int currentScore;
    const int addScore = 1;
    int currentRecord;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textRecord;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start() 
    {
        OnEnemyDestroyed += AddScore;
        OnEnemyDestroyed += CheckForNewRecord;

        ResetScore();
    }

    public void EnemyIsDestroyedEvent()
    {
        OnEnemyDestroyed?.Invoke();
    }

    public void ResetScore()
    {
        UpdateScoreToUI(currentScore = 0);
    }

    void AddScore() => UpdateScoreToUI(currentScore += addScore);

    void CheckForNewRecord()
    {
        //Check for new highest score to set a new Records
        if (currentRecord < currentScore)
        {
            currentRecord = currentScore;
            PlayerPrefs.SetInt("Highest Score",currentRecord);
            PlayerPrefs.Save();
        }
    } 

    void UpdateScoreToUI(int currentScore)
    {
        textScore.text = currentScore.ToString();
    }

}
