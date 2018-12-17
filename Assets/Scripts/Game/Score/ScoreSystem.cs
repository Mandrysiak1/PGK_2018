using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {

    public MonoWinStreakSource winStreakSource;
    public WinGameWhenTimeIsOver winGameWhenTimeIsOver;
    public int accumulatedWinStreak;
    public int accumulatedTip;
    public int coinScoreMultiplier = 3;
    private int winStreakScore = 0;

    public int Score
    {
        get
        {
            return winStreakScore + accumulatedTip * coinScoreMultiplier;
        }
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
        UpgradeClass.OnTipChanged.AddListener(TipChanged);
	}

    private void TipChanged(int newTip, int oldTip)
    {
        if (newTip > oldTip)
            accumulatedTip = newTip;
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
        this.winStreakScore = 0;
        this.accumulatedTip = 0;
    }
}
