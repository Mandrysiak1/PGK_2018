using Assets.PGKScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.ThirdPerson;

public static class UpgradeClass {
    public class TipEvent : UnityEvent<int, int> { }
    public static TipEvent OnTipChanged = new TipEvent();
    public static int preGameTip;
    public static int tip = 0;

    public static int Tip
    {
        get
        {
            return tip;
        }
        set
        {
            var old = tip;
            tip = value;
            OnTipChanged.Invoke(tip, old);
        }
    }

    public static float SpeedModif = 0f;
    public static int BeerModif = 0;
    public static int BeerTimes = 1;
    public static int SpeedTimes = 1;
    public static bool nextlvlcanvas = false;
    public static bool endgamecanvas = false;
    public static bool exited = false;
    public static bool Vulnerable = true;
    public static bool invulnerabilityPurchased = false;

    public static void ChangeSpeedModif()
    {
        int cost = 5*SpeedTimes;
        if(Tip >= cost)
        {
            Tip -= cost;
            SpeedModif += 0.05f;
            SpeedTimes++;
        }
    }
    public static void LoadTips(int value)
    {
        tip = value;
    }
    public static void ChangeMaxBeer()
    {
        int cost = 10 * BeerTimes;
        if(Tip >= cost)
        {
            Tip -= cost;
            BeerModif += 1;
            BeerTimes++;
        }
    }

    public static void MarioStar()
    {
        int cost = 1000;
        if (Tip >= cost && invulnerabilityPurchased == false)
        {
            Tip -= cost;
            Vulnerable = false;
            invulnerabilityPurchased = true;
        }
    }

}
