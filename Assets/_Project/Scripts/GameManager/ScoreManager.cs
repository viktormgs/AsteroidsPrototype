using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore;





    public static int AddScore(int scoreToAdd)
    {
        return currentScore += scoreToAdd;
    }

}
