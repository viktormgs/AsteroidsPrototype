using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore;
    int addScore = 1;
    TextMeshProUGUI text;


    private void Start() => ResetScore();

    public void ResetScore() => currentScore = 0;

    public void AddScore() => currentScore += addScore;

}
