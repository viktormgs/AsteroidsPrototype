using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighestRecordUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highestRecord;

    void Start()
    {
        GameManagerEvents.OnGameOver += UpdateNewRecord;
        UpdateNewRecord();
    }

    private void OnDestroy()
    {
        GameplayEvents.OnNewRecord -= UpdateNewRecord;
    }

    private void UpdateNewRecord() => highestRecord.text = ScoreManager.HighestRecord.ToString("000");
}
