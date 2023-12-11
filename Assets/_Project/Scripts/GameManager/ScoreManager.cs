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
    bool isNewRecord;

    //encapsulation to call once for new record during game, should reset after game over
    bool _isNewRecord
    {
        get { return isNewRecord; }
        set
        {
            if (isNewRecord == true)
            {
                isNewRecord = value;
                NewRecordIngame();
            }
        }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start() 
    {
        OnEnemyDestroyed += AddScore;
        OnEnemyDestroyed += CheckForNewRecord;

        ResetIngameScore();
    }

    public void EnemyIsDestroyedEvent()
    {
        OnEnemyDestroyed?.Invoke();
    }

    public void ResetIngameScore()
    {
        UpdateScoreToUI(currentScore = 0);
        _isNewRecord = false;
    }

    void AddScore() => UpdateScoreToUI(currentScore += addScore);

    void NewRecordIngame()
    {
       
    }

    void CheckForNewRecord()
    {
        //Check for new highest score to set a new Records
        if (currentRecord < currentScore)
        {
            currentRecord = currentScore;
            PlayerPrefs.SetInt("Highest Score",currentRecord);
            PlayerPrefs.Save();
            isNewRecord = true;
        }
    } 

    void UpdateScoreToUI(int currentScore)
    {
        textScore.text = currentScore.ToString();
    }

    void ShowNewRecord()
    {

    }
}
