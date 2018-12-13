using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {

    public MonoWinStreakSource winStreakSource;
    public WinGameWhenTimeIsOver winGameWhenTimeIsOver;
    public int accumulatedWinStreak;
    public int coinScoreMultiplier = 3;
    public float timeBonusCap = 100;
    private int winStreakScore = 0;

    public int Score
    {
        get
        {
            return winStreakScore + UpgradeClass.Tip * coinScoreMultiplier +
               timeValue(winGameWhenTimeIsOver.Timer, winGameWhenTimeIsOver.Limit, timeBonusCap) ;
        }
    }

    private int timeValue(float currTime, float timeLimit, float cap)
    {
        return (int)(  Mathf.Pow(1.25f, (currTime/6) * 124 / timeLimit) ); 
    }

    private int ExponentWinStreakMultiplier()
    {
        return (int)(0.33 * Mathf.Exp(accumulatedWinStreak * 1f / 4) + 2);
    }

    // Use this for initialization
    void Start () {
        if (winStreakSource == null)
            winStreakSource = FindObjectOfType<MonoWinStreakSource>();
        winStreakSource.WinStreakChanged.AddListener(WinStreakCounter);
	}

    private void WinStreakCounter(int arg0, int arg1)
    {
        if (arg1 > arg0)
        {
            this.winStreakScore += (arg1 - arg0) * ExponentWinStreakMultiplier();
            accumulatedWinStreak += (arg1 - arg0);
        }
        if (arg1 == 0)
            accumulatedWinStreak = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}

    internal void ResetScore()
    {
        this.accumulatedWinStreak = 0;
    }
}
