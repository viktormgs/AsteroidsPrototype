using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuRecordText : MonoBehaviour
{
    readonly ScoreManager scoreManager = ScoreManager.instance;
    TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = scoreManager.DisplayNewRecordOnMainMenu().ToString("0000");
    }




}
