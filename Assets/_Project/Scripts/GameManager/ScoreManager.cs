using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public event Action OnEnemyDestroyed;
    public Action OnIngameNewRecord;

    int currentScore = 0;
    const int addScore = 1;
    int newRecord;
    public TextMeshProUGUI textScore;
    bool isNewRecord;


    //encapsulation to call once for new record during game, should reset after game over
    bool _isNewRecord
    {
        get { return isNewRecord; }
        set
        {
            if (value == true)
            {
                isNewRecord = value;
                OnIngameNewRecord?.Invoke();
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
        PlayerPrefs.GetInt("Highest Score", newRecord);
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


    void CheckForNewRecord()
    {
        if (currentScore > newRecord)
        {
            newRecord = currentScore;
            PlayerPrefs.SetInt("Highest Score",newRecord);
            PlayerPrefs.Save();
            if(!isNewRecord) _isNewRecord = true;
        }
    } 

    public int DisplayNewRecordOnMainMenu()
    {
        return newRecord;
    }

    void UpdateScoreToUI(int currentScore) => textScore.text = currentScore.ToString("000");

    public void DebugRecordScore()
    {
        Debug.Log("Current High Record: " + newRecord.ToString());



    }
}
